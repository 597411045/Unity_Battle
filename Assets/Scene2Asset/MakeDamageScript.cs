using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeDamageScript : MonoBehaviour
{
    Canvas UICanvas;
    Camera UICamera;
    List<GameObject> affectedObjects;
    public int damageMultipler;
    public Vector3 damageForce;
    public int affectedObjectsCounts;

    private void Start()
    {
        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        affectedObjects = new List<GameObject>();
        this.enabled = false;
    }

    private void Update()
    {
        affectedObjectsCounts = affectedObjects.Count;
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

        Vector3 damageForce = new Vector3(10 * this.gameObject.GetComponentInParent<BaseActionControlScript>().XAxis, 10, 0);
        other.gameObject.GetComponent<TakeDamageScript>().takeDamage(
            this.gameObject.transform.position,
            this.gameObject.name,
            damageMultipler,
            damageForce);



    }

    public void End()
    {
        affectedObjects.Clear();
        this.enabled = false;
        affectedObjectsCounts = affectedObjects.Count;
    }
}
