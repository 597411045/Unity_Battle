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
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1, 0), out raycastHit, 100,LayerMask.GetMask("Ground"),QueryTriggerInteraction.Ignore))
        {
            DistanceFromGround = this.transform.position.y - raycastHit.point.y;
            if (actionControlScript.rigidbody.velocity.y <= 0 && DistanceFromGround < 0.05f)
            {
                actionControlScript.isOutOfGround = false;
            }
            if (DistanceFromGround > 0.05f)
            {
                actionControlScript.isOutOfGround = true;
            }
        }
    }
}
