using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredBody : MonoBehaviour
{
    public void Injured(int ATK)
    {
        Debug.Log(this.gameObject.name + "|Injured:" + ATK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InjuredBody>() != null)
        {
            Debug.Log(this.gameObject.name + "|Attack:" + other.gameObject.name);

            other.gameObject.GetComponent<InjuredBody>().Injured(10);
            //other.attachedRigidbody.AddForce((other.gameObject.transform.position - this.transform.position).normalized * 250);
            other.gameObject.GetComponentInParent<Animator>().SetBool("isInjured", true);
        }
    }
}
