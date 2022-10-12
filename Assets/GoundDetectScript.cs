using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundDetectScript : MonoBehaviour
{
    BaseActionControlScript actionControlScript;
    void Start()
    {
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (actionControlScript.isOutOfGround && collision.gameObject.layer == 10)
        {
            actionControlScript.isOutOfGround = false;
            actionControlScript.animator.SetBool("isInjured", false);
        }
    }
}
