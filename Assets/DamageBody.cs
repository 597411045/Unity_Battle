using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBody : MonoBehaviour
{
    public bool active;

    private void Start()
    {
        active = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (active && collision.gameObject.GetComponent<InjuredBody>() != null)
        {
            Debug.Log(this.gameObject.name + "|Attack:" + collision.gameObject.name);

            collision.gameObject.GetComponent<InjuredBody>().Injured(10);
            //other.attachedRigidbody.AddForce((other.gameObject.transform.position - this.transform.position).normalized * 250);
            collision.gameObject.GetComponentInParent<Animator>().SetBool("isInjured", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.gameObject.GetComponent<InjuredBody>() != null)
        {
            Debug.Log(this.gameObject.name + "|Attack:" + other.gameObject.name);

            other.gameObject.GetComponent<InjuredBody>().Injured(10);
            //other.attachedRigidbody.AddForce((other.gameObject.transform.position - this.transform.position).normalized * 250);
            other.gameObject.GetComponentInParent<Animator>().SetBool("isInjured", true);
        }
    }

}
