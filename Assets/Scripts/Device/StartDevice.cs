using UnityEngine;
using System;
using QuickLink2DotNet;

public class StartDevice : MonoBehaviour
{
    private static string dirname = "D:\\# praca magisterska\\Gra sterowana wzrokiem\\Gra sterowana wzrokiem - aplikacja\\Gra sterowana wzrokiem";
    private static string calibrationFilename = System.IO.Path.Combine(dirname, "QLCalibration.cal");
    private static string filename_settings = System.IO.Path.Combine(dirname, "QLSettings.txt");

    private int deviceId = 0;
    private int calibrationId = 0;

    private System.Single leftRadius;
    private System.Single rightRadius;
    private int distance = 53;


    void Start()
    {
        deviceId = QuickStart.Initialize.QL2Initialize(filename_settings);
        QLError error = QuickLink2API.QLDevice_Start(deviceId);
  
        if (error != QLError.QL_ERROR_OK)
        {
            Debug.Log("Device not started successfully!");
            return;
        }

        QuickLink2API.QLCalibration_Create(0, out calibrationId);
        QuickLink2API.QLCalibration_Load(calibrationFilename, ref calibrationId);
        QuickLink2API.QLDevice_ApplyCalibration(deviceId, calibrationId);

        QLFrameData frameData = new QLFrameData();
        try
        {
            error = QuickLink2API.QLDevice_GetFrame(deviceId, 5000, ref frameData);
            if (error == QLError.QL_ERROR_OK)
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
            Debug.Log("QLDevice_GetFrame error | " + ex.Message);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
