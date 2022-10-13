using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundDetectScript : MonoBehaviour
{
    BaseActionControlScript actionControlScript;
    RaycastHit raycastHit;
    public float DistanceFromGround;
    void Start()
    {
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
        raycastHit = new RaycastHit();
    }

    private void Update()
    {
        if (!actionControlScript.isOutOfGround) return;
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1, 0), out raycastHit, 100))
        {
            DistanceFromGround = this.transform.position.y - raycastHit.point.y;
            if (actionControlScript._rigidbody.velocity.y <= 0 && DistanceFromGround < 0.05f)
            {
                actionControlScript.isOutOfGround = false;
                this.gameObject.GetComponentInChildren<AniControlScript>().GeneralEndAnimation("isInjured");
            }
        }
    }
    // Update is called once per frame
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (actionControlScript.isOutOfGround && collision.gameObject.layer == 10)
    //    {
    //        actionControlScript.isOutOfGround = false;
    //        this.gameObject.GetComponentInChildren<AniControlScript>().GeneralEndAnimation("isInjured");
    //    }
    //}
}
