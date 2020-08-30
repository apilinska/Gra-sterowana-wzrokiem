using UnityEngine;
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
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\eye_data.txt";
        this.distance = (-1) * Camera.main.transform.position.z;
        Cursor.lockState = GameData.IsDeviceMode() ? CursorLockMode.Locked : CursorLockMode.None;
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
            SceneManager.LoadScene("Initialize");
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    void ReadPositionFromFile()
    {
        float? x = null, y = null;
        FileStream logFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader logFileReader = new StreamReader(logFileStream);
        EyeData data = new EyeData();

        while (!logFileReader.EndOfStream)
        {
            string line = logFileReader.ReadLine();
            data = JsonUtility.FromJson<EyeData>(line);
            x = data?.values?.frame?.avg?.x;
            y = data?.values?.frame?.avg?.y;
        }
        logFileReader.Close();
        logFileStream.Close();

        if(data.category == "tracker" && x != 0 && y != 0) 
        {
            // Cursor.lockState = CursorLockMode.Locked;
            // cursor.gameObject.SetActive(true);
            // if(x != 0 && y != 0) {
            this.cursor_pos = new Vector2((float)x, (float)y);
            this.cursor_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(cursor_pos.x, Camera.main.pixelHeight - cursor_pos.y, distance));
            this.cursor_world_pos *= 10f;
            cursor.transform.position = new Vector3(cursor_world_pos.x, cursor_world_pos.y, cursor.transform.position.z);
            // }
        } 
        // else 
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     cursor.gameObject.SetActive(false);
        // }
    }

    public Vector3? CursorPosition() 
    {
        if(this.cursor_pos != null) 
        {
            return new Vector3(this.cursor_pos.x, this.cursor_pos.y, 0);
        } else {
            return null;
        }
    }
}
