using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour
{
    public static T instance;

    public void InitInstance(T t)
    {
        if (instance == null)
        {
            instance = t;
        }
    }
}
