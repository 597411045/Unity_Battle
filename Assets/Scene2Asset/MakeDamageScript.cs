using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MakeDamageScript : MonoBehaviour
{
    bool isActive;
    List<GameObject> affectedObjects;
    public int damageMultipler;
    public Vector3 damageForce;
    public int affectedObjectsCounts;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;
        isActive = true;

    }

    private void Start()
    {
        if (!isActive) return;
        affectedObjects = new List<GameObject>();
        this.enabled = false;
    }

    private void Update()
    {
        if (!isActive) return;

        affectedObjectsCounts = affectedObjects.Count;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (this.enabled == false) return;
        if (affectedObjects.Count > 0) return;
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
        affectedObjects.Add(other.gameObject);
        Debug.Log("Hit");

        Vector3 damageForce = new Vector3(10 * this.gameObject.GetComponentInParent<BaseActionControlScript>().XAxis, 10, 0);
        other.gameObject.GetComponent<TakeDamageScript>().takeDamage(
            this.gameObject.transform.position,
            this.gameObject.name,
            damageMultipler,
            damageForce);


    }

    public void End()
    {
        if (!isActive) return;

        affectedObjects.Clear();
        this.enabled = false;
        affectedObjectsCounts = affectedObjects.Count;
    }
}
