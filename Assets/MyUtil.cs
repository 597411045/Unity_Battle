using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MyUtil
{
    public static string mainPath = Application.dataPath + "/Resources";

    public static Transform FindTransformInChildren(Transform t, string childName)
    {
        Transform tmp = t.Find(childName);
        if (tmp == null)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                tmp = FindTransformInChildren(t.GetChild(i), childName);
                if (tmp != null)
                {
                    return tmp;
                }
            }
        }
        return tmp;
    }

    public static T GetClassFromBinary<T>(string path)
    {
        T tmp;
        FileStream fs = File.Open(path, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        tmp = (T)bf.Deserialize(fs);
        fs.Close();
        return tmp;
    }

    public static void SaveClassToBinary<T>(string path, T tmp)
    {
        FileStream fs = File.Open(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, tmp);
        fs.Close();
    }

    static string connStr = "Server=81.68.87.60;Port=3307;Database=unity;Uid=sa;Pwd=P@ss1234;Charset=utf8";
    static MySqlConnection conn;
    static MySqlDataAdapter adapter;

    public static int GetDataFromMySQL(string cmdStr, DataSet ds)
    {
        int result = 0;
        try
        {
            if (conn == null)
            {
                conn = new MySqlConnection(connStr);
            }
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                adapter = new MySqlDataAdapter(cmdStr, conn);
                adapter.Fill(ds);
                result = 1;
            }
        }
        catch (Exception e)
        {
            result = -1;
        }
        finally
        {
            conn.Close();
        }
        return result;
    }
}
