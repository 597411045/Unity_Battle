using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV_HandScript : MonoBehaviour
{
    Transform UVAFan;

    public bool isAttached;
    public bool isAttachAble;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isAttachAble)
        {
            UVAFan.GetComponent<Rigidbody>().useGravity = false;
            UVAFan.SetParent(this.transform);
            UVAFan.localPosition = Vector3.zero;
            UVAFan.rotation = this.transform.rotation;
            UVAFan.SetParent(null);
            FixedJoint tmp = UVAFan.gameObject.AddComponent<FixedJoint>();
            tmp.connectedBody = this.transform.GetComponent<Rigidbody>();
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
            isAttached = true;
            isAttachAble = false;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (isAttached) return;
        if (!other.gameObject.name.Contains("UAVFan")) return;
        UVAFan = other.transform;

        if (Vector3.Distance(this.transform.position, other.transform.position) > 0.1f) return;
        if (Vector3.Angle(UVAFan.forward, this.transform.forward) > 10) return;
        if (Vector3.Angle(UVAFan.up, this.transform.up) > 10) return;
        if (Vector3.Angle(UVAFan.right, this.transform.right) > 10) return;

        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1, 0, 0, 0));
        isAttachAble = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
    }
}
