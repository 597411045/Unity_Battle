using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIActionControlScript : BaseActionControlScript
{
    float DistanceFromVerse;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        verser = GameObject.Find("P1");
        HPBar = MyUtil.FindTransformInChildren(GameObject.Find("verseHPBar").transform, "HPValue").GetComponent<Image>();
        HPText = GameObject.Find("verseHPBar").GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXAxis();
        this.transform.eulerAngles = new Vector3(0, 90 * XAxis, 0);
        DistanceFromVerse = Mathf.Abs(verser.transform.position.x - this.transform.position.x);

        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);

        if (DistanceFromVerse < 1.1f)
        {
            animator.SetBool("isPunch", true);

        }
        if (DistanceFromVerse > 1.1f && DistanceFromVerse < 1.2f)
        {
            animator.SetBool("isMartelo", true);
        }

        if (ifMoveAble && isOutOfGround == false)
        {

            if (DistanceFromVerse > 1.5f)
            {
                animator.SetBool("isMove", true);

                if (Mathf.Abs(rigidbody.velocity.x) < 2)
                {
                    rigidbody.velocity += new Vector3((2 - Mathf.Abs(rigidbody.velocity.x)) * XAxis, 0, 0);
                }
                //rigidbody.velocity = new Vector3(1 * XAxis, 0, 0);

            }
            if (DistanceFromVerse < 1.0f)
            {
                animator.SetBool("isMoveBack", true);
                if (Mathf.Abs(rigidbody.velocity.x) < 2)
                {
                    rigidbody.velocity += new Vector3((-2 + Mathf.Abs(rigidbody.velocity.x)) * XAxis, 0, 0);
                }
                //rigidbody.velocity = new Vector3(-1 * XAxis, 0, 0);
            }
            if (animator.GetBool("isPunch") || animator.GetBool("isMartelo"))
            {
                animator.SetBool("isBlock", false);
            }
            else
            {
                animator.SetBool("isBlock", true);
            }
        }





        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    animator.SetBool("isPunch", true);
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    animator.SetBool("isBlock", true);
        //}
    }

}
