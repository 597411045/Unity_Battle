using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeDamageScript : MonoBehaviour
{
    Canvas UICanvas;
    Camera UICamera;
    List<GameObject> affectedObjects;

    private void Start()
    {
        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        affectedObjects = new List<GameObject>();
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false) return;
        if (other.gameObject.layer != 9) return;
        if (other.gameObject.GetComponent<TakeDamageScript>() == null) return;
        //if (
        //    affectedObjects.Exists(
        //        (GameObject tmp) =>
        //        {
        //            int a = tmp.gameObject.GetHashCode();
        //            int b = other.gameObject.GetHashCode();
        //            return tmp.gameObject.GetHashCode() == other.gameObject.GetHashCode();
        //        }
        //        )
        //    ) return;
        if (affectedObjects.Count > 0) return;
        affectedObjects.Add(other.gameObject);
        Vector3 v = Camera.main.WorldToViewportPoint(this.gameObject.transform.position);
        GameObject go = Instantiate(Resources.Load("Prefabs\\Hint"), UICamera.ViewportToWorldPoint(new Vector3(v.x, v.y, 5)), Quaternion.identity, UICanvas.transform) as GameObject;
        Debug.Log(this.gameObject.name + "->" + other.gameObject.name);
        go.GetComponent<Text>().text = other.gameObject.name.Replace("mixamorig1:", "");
        other.gameObject.GetComponentInParent<Animator>().SetBool("isInjured", true);
        other.attachedRigidbody.velocity = new Vector3(10, 10, 0);
        other.gameObject.GetComponentInParent<BaseActionControlScript>().isOutOfGround = true;
    }

    public void End()
    {
        this.enabled = false;
        affectedObjects.Clear();
    }
}
