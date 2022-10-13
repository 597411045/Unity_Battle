using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionControlScript : BaseActionControlScript
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        verser = GameObject.Find("P2");
        HPBar = MyUtil.FindTransformInChildren(GameObject.Find("PlayerHPBar").transform, "HPValue").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXAxis();
        this.transform.eulerAngles = new Vector3(0, 90 * XAxis, 0);

        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);
        if (ifMoveAble && isOutOfGround == false)
        {

            if (Input.GetKey(KeyCode.A))
            {
                if (XAxis > 0)
                {
                    animator.SetBool("isMoveBack", true);
                }
                else
                {
                    animator.SetBool("isMove", true);
                }
                _rigidbody.velocity = new Vector3(-1, 0, 0);

            }
            if (Input.GetKey(KeyCode.D))
            {
                if (XAxis > 0)
                {
                    animator.SetBool("isMove", true);
                }
                else
                {
                    animator.SetBool("isMoveBack", true);
                }
                _rigidbody.velocity = new Vector3(1, 0, 0);

            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetBool("isPunch", true);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("isMartelo", true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("isBlock", true);
        }
    }

}
