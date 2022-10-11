using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionControlScript : BaseActionControlScript
{
    public float xPosition;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        player = GameObject.Find("P1");
    }

    // Update is called once per frame
    void Update()
    {
        xPosition = player.transform.position.x - this.transform.position.x;
        if (ifMoveAble)
        {
            if (xPosition < 0 && Mathf.Abs(xPosition) > 1.5f)
            {
                _rigidbody.velocity = new Vector3(-1, 0, 0);
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
            if (xPosition < 0 && Mathf.Abs(xPosition) < 1.0f)
            {
                _rigidbody.velocity = new Vector3(1, 0, 0);
                animator.SetBool("isMoveBack", true);
            }
            else
            {
                animator.SetBool("isMoveBack", false);
            }

            //if (xPosition > 0 && Mathf.Abs(xPosition) > 1.5f)
            //{
            //    _rigidbody.velocity = new Vector3(1, 0, 0);
            //    animator.SetBool("isMoveBack", true);
            //}
            //else
            //{
            //    animator.SetBool("isMoveBack", false);
            //}
        }
        else
        {
            animator.SetBool("isMove", false);
            animator.SetBool("isMoveBack", false);
        }
        if (Mathf.Abs(xPosition) > 1.1f && Mathf.Abs(xPosition) < 1.4f)
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
