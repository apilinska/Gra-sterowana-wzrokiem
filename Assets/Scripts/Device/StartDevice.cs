using UnityEngine;
using System;
using QuickLink2DotNet;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartDevice : MonoBehaviour
{
    public Text deviceInfoText;

    private static string dirname = "D:\\# praca magisterska\\Gra sterowana wzrokiem\\Gra sterowana wzrokiem - aplikacja\\Gra sterowana wzrokiem";

    // private static string path = Application.persistentDataPath;
    // private static string settingsFile = path + "settings.txt";
    // private static string calibrationFile = path + "calibration.cal";

    private static string calibrationFilename = System.IO.Path.Combine(dirname, "QLCalibration.cal");
    private static string filename_settings = System.IO.Path.Combine(dirname, "QLSettings.txt");

    private int deviceId = 0;
    private int calibrationId = 0;

    private System.Single leftRadius;
    private System.Single rightRadius;
    private int distance = 53;

    private QLError error;
    private QLFrameData frameData;

    void Start()
    {
        deviceId = QuickStart.Initialize.QL2Initialize(filename_settings);
        Log.Save("device id: " + deviceId);

        // try 
        // {
        //     QuickLink2API.QLDevice_Stop(deviceId);
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log("try to stop: " + ex.Message);
        //     Log.Save("try to stop: " + ex.Message);
        // }

        if (QuickLink2API.QLDevice_Start(deviceId) != QLError.QL_ERROR_OK) {
            Log.Save("Device not started successfully!");
            Debug.Log("Device not started successfully!");
            return;
        } else {
            Debug.Log("Device started successfully!");
        }

        // if(error != QLError.QL_ERROR_OK) {
        //     Log.Save("QLDevice_Start: " + error.ToString());
        // }
        QLDeviceInfo deviceInfo;
        if (QuickLink2API.QLDevice_GetInfo(deviceId, out deviceInfo) == QLError.QL_ERROR_OK) {
            string info = "";
            info = "model: " + deviceInfo.modelName + "\n\n" + "sensor height: " + deviceInfo.sensorHeight + 
                "\n\n"  + "sensor width: " + deviceInfo.sensorWidth  + "\n\n" + "serial number: " +  deviceInfo.serialNumber;
            // Log.Save("info: " + info);
            deviceInfoText.text = info;
        }
        // if(error != QLError.QL_ERROR_OK) {
        //     Log.Save("QLDevice_GetInfo: " + error.ToString());
        // }

        // QuickLink2API.QLCalibration_Create(0, out calibrationId);
        // QuickLink2API.QLCalibration_Load(calibrationFilename, ref calibrationId);
        // QuickLink2API.QLDevice_ApplyCalibration(deviceId, calibrationId);

        frameData = new QLFrameData();
        error = QLError.QL_ERROR_OK;
        try
        {
            if((error = QuickLink2API.QLDevice_GetFrame(deviceId, 1000, ref frameData)) != QLError.QL_ERROR_OK)
            {
                Debug.Log("Left eye found: " + frameData.LeftEye.Found.ToString());
            }
            else
            {
                Debug.Log("Error: " + error);
            }
        }
        catch (Exception ex)
        {
            //Log.Save("QLDevice_GetFrame error | " + ex.Message);
            Debug.Log("QLDevice_GetFrame error | " + ex.Message);
        }
    }

    private void SelectUser() {
        SceneManager.LoadScene("SelectUser");
    }

    void Update()
    {
        if(Input.GetKeyDown("space")) 
        {
            SelectUser();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Device stopped");
            QuickLink2API.QLDevice_Stop(deviceId);
            Application.Quit();
        }
        //QuickLink2API.QLDevice_CalibrateEyeRadius(deviceId, distance, out leftRadius, out rightRadius);
        //Debug.Log("leftRadius " + leftRadius);
        //Debug.Log("rightRadius " + rightRadius);

        //QuickLink2API.QLDevice_GetFrame(deviceId, 1000, ref frameData);
        //Debug.Log("WeightedGazePoint: " + frameData.WeightedGazePoint);
        //if (frameData.WeightedGazePoint.Valid)
        //{
        //    string log_line = frameData.WeightedGazePoint.x.ToString("0.0") + ", " + frameData.WeightedGazePoint.y.ToString("0.0");
        //    Debug.Log(log_line);
        //}

        //QLFrameData frameData = new QLFrameData();
        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log("deviceId: " + deviceId);
        //    Debug.Log("i: " + i);
        //    error = QuickLink2API.QLDevice_GetFrame(deviceId, 10000, ref frameData);
        //    if (error == QLError.QL_ERROR_OK)
        //    {
        //        Debug.Log("Left eye found: " + frameData.LeftEye.Found.ToString());
        //    }
        //    else
        //    {
        //        Debug.Log("Error: " + error);
        //    }
        //}
    }
}
