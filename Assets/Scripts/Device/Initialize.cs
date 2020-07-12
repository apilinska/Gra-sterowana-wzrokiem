using System;
using UnityEngine;
using QuickLink2DotNet;
using UnityEngine.UI;

namespace QuickStart
{
    public static class Initialize
    {
        public static int QL2Initialize(string path)
        {
            QLError qlerror = QLError.QL_ERROR_OK;
            int deviceId = 0;
            QLDeviceInfo deviceInfo;
            //int settingsId;
            int numDevices = 1;
            int[] deviceIds = new int[numDevices];
            qlerror = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);

            QuickLink2API.QLDevice_GetInfo(deviceIds[0], out deviceInfo);
            if (qlerror != QLError.QL_ERROR_OK)
            {
                Debug.Log("QLDevice_Enumerate() failed with error code " + qlerror);
                return 0;
            }
            
            else if (numDevices == 0)
            {
                Debug.Log("No devices present.");
                return 0;
            }
            else if (numDevices == 1)
            {
                deviceId = deviceIds[0];
            }
            else if (numDevices > 1)
            {
                Debug.Log("QLDevice_Enumerate() found " + numDevices + " devices");
                return 0;
            }

            // QuickLink2API.QLSettings_Create(0, out settingsId);

            // QuickLink2API.QLSettings_Load(path, ref settingsId);

            // QuickLink2API.QLDevice_GetInfo(deviceId, out deviceInfo);

            // string serialNumberName = "SN_";
            // serialNumberName += deviceInfo.serialNumber;

            // qlerror = QuickLink2API.QLDevice_SetPassword(deviceId, "CTR6JLYD");

            // if (qlerror != QLError.QL_ERROR_OK)
            // {
            //     System.Console.WriteLine("What is the password for the device? ");
            //     string userPassword = System.Console.ReadLine();

            //     qlerror = QuickLink2API.QLDevice_SetPassword(deviceId, userPassword);
            //     if (qlerror != QLError.QL_ERROR_OK)
            //     {
            //         System.Console.WriteLine("Invalid password. Error = {0}", qlerror);
            //         return 0;
            //     }

            //     QuickLink2API.QLSettings_SetValueString(settingsId, serialNumberName, userPassword);
            //     QuickLink2API.QLSettings_Save(path, settingsId);
            // }

            return deviceId;
        }
    }
}
