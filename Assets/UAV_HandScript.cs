using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV_HandScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.name.Contains("UAVFan")) return;
        Transform UVAFan = other.transform.GetChild(0);

        if (Vector3.Angle(UVAFan.forward, this.transform.GetChild(0).forward) > 10) return;
        if (Vector3.Angle(UVAFan.up, this.transform.GetChild(0).up) > 10) return;
        if (Vector3.Angle(UVAFan.right, this.transform.GetChild(0).right) > 10) return;

        UVAFan.SetParent(this.transform.GetChild(0));
        UVAFan.localPosition = Vector3.zero;
    }
}
