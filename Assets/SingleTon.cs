using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : new()
{
    private static readonly object _syncLock = new object();

    private static T _instance;
    //private static Lazy<SingleTon> _instance = new Lazy<SingleTon>(() => new SingleTon());
    public SingleTon()
    {
        Console.WriteLine("Created");
    }

    //static SingleTon()
    //{
    //    _instance = new T();
    //}

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }

    }
}
