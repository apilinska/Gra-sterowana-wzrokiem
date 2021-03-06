﻿using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DeviceData : MonoBehaviour
{
    public Image cursor;

    private string path;
    private Vector2 cursor_pos;
    private Vector3 cursor_world_pos;
    private float distance; 

    [Serializable]
    public class EyeData
    {
        public string category;
        public string request;
        public int statuscode;
        public Values values;
    }

    [Serializable]
    public class Values
    {
        public Frame frame;
    }

    [Serializable]
    public class Frame
    {
        public Coordinates avg;
        public bool fix;
        public Eye lefteye;
        public Eye righteye;
        public Coordinates raw;
        public int state;
        public long time;
        public string timestamp;
    }

    [Serializable]
    public class Eye
    {
        public Coordinates avg;
        public Coordinates pcenter;
        public float psize;
        public Coordinates raw;
    }

    [Serializable]
    public class Coordinates
    {
        public float x;
        public float y;
    }

    void Start()
    {
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\eyeTribeData.txt";
        this.distance = (-1) * Camera.main.transform.position.z;
        SetCursor();
    }

    private Frame GetFrame(EyeData data) 
    {
        return data?.values?.frame;
    }

    public Vector3? CursorPosition() 
    {
        if(this.cursor_pos != null) 
        {
            return new Vector3(this.cursor_pos.x, this.cursor_pos.y, 0);
        } 
        return null;
    }

    private void SetCursor() 
    {
        if(GameData.IsDeviceMode()) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        this.cursor.gameObject.SetActive(GameData.IsDeviceMode());
    }

    void Update()
    {
        if(GameData.IsDeviceMode()) 
        {
            ReadPositionFromFile(); 
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Initialize");
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    private void ReadPositionFromFile()
    {
        float? x = null, y = null;
        if(File.Exists(path)) 
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader fileReader = new StreamReader(fileStream);
            EyeData data = new EyeData();
            Frame frame = null;
            string line = "";

            while (!fileReader.EndOfStream)
            {
                line = fileReader.ReadLine();
            }

            fileReader.Close();
            fileStream.Close();

            if(!String.IsNullOrEmpty(line)) 
            {
                data = JsonUtility.FromJson<EyeData>(line);
                if(data.category == "tracker") 
                {
                    frame = GetFrame(data);
                    if(frame != null) 
                    {
                        x = frame.avg.x;
                        y = frame.avg.y;

                        if(x != null && y != null && x != 0 && y != 0) 
                        {
                            this.cursor_pos = new Vector2((float)x, (float)y);
                            this.cursor_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(cursor_pos.x, Camera.main.pixelHeight - cursor_pos.y, distance));
                            this.cursor_world_pos *= distance;
                            Vector3 newPos = new Vector3(cursor_world_pos.x, cursor_world_pos.y, cursor.transform.position.z);
                            cursor.transform.position = newPos;
                        } 
                    }
                }
            }
        }
    }
}
