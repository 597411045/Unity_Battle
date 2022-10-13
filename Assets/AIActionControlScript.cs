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

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXAxis();
        this.transform.eulerAngles = new Vector3(0, 90 * XAxis, 0);
        DistanceFromVerse = Mathf.Abs(verser.transform.position.x - this.transform.position.x);

        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);
        if (ifMoveAble && isOutOfGround == false)
        {

            if (DistanceFromVerse > 1.5f)
            {
                animator.SetBool("isMove", true);
                _rigidbody.velocity = new Vector3(1 * XAxis, 0, 0);

            }
            if (DistanceFromVerse < 1.0f)
            {
                animator.SetBool("isMoveBack", true);
                _rigidbody.velocity = new Vector3(-1 * XAxis, 0, 0);

            }
        }


        if (DistanceFromVerse > 1.1f && DistanceFromVerse < 1.4f)
        {
            animator.SetBool("isMartelo", true);
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
