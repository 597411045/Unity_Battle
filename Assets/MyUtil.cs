using System.Collections;
using System.Collections.Generic;
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

}
