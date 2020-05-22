using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Log 
{
    public static string file = "/applicationLogs.txt";
    public static string path = Application.persistentDataPath + file;

    public static void Save(string message) 
    {
        try 
        {
            File.AppendAllText(path, GetLog(message)); 
        } 
        catch (Exception ex) {
            Debug.Log("Save error | " + ex.Message);
        }
    }

    private static string GetLog(string message) {
        string dateTime = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        return dateTime + "\r\n" + message +"\r\n\n";
    }
}