using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV_BodyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.Contains("UAVHand")) return;
        Transform UVAHand = other.transform.GetChild(0);

        if (Vector3.Angle(UVAHand.forward, this.transform.forward) > 10) return;
        if (Vector3.Angle(UVAHand.up, -this.transform.right) > 10) return;
        if (Vector3.Angle(UVAHand.right, this.transform.up) > 10) return;

        UVAHand.SetParent(this.transform.parent);
        UVAHand.localPosition = Vector3.zero;
    }
}
