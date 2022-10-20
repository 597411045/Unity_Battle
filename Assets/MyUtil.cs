using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MyUtil

{
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

    public static void SaveClassToBinary<T>(string path,T tmp)
    {
        FileStream fs = File.Open(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, tmp);
        fs.Close();
    }
}
