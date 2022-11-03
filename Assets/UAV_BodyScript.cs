using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV_BodyScript : MonoBehaviour
{
    Transform UVAHand;
    public bool isAttached;
    public bool isAttachAble;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isAttachAble)
        {
            UVAHand.GetComponent<Rigidbody>().useGravity = false;
            UVAHand.SetParent(this.transform.parent);
            UVAHand.localPosition = Vector3.zero;
            UVAHand.rotation = Quaternion.LookRotation(this.transform.forward, -this.transform.right);
            UVAHand.SetParent(null);
            FixedJoint tmp = UVAHand.gameObject.AddComponent<FixedJoint>();
            tmp.connectedBody = this.transform.parent.GetComponent<Rigidbody>();
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
            isAttached = true;
            isAttachAble = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAttached) return;

        if (!other.gameObject.name.Contains("UAVHand")) return;
        UVAHand = other.transform;
        if (Vector3.Angle(UVAHand.forward, this.transform.forward) > 10) return;
        if (Vector3.Angle(UVAHand.up, -this.transform.right) > 10) return;
        if (Vector3.Angle(UVAHand.right, this.transform.up) > 10) return;
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1, 0, 0, 0));
        isAttachAble = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
    }
}
