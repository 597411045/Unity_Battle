using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionControlScript : MonoBehaviour
{
    Rigidbody rigidbody;
    Animator animator;
    bool ifMoveAble;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        animator.SetBool("isBattleStatus", true);
        ifMoveAble = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifMoveAble)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector3(-1, 0, 0);
                animator.SetBool("isMoveBack", true);
            }
            else
            {
                animator.SetBool("isMoveBack", false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector3(1, 0, 0);
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
        }
        else
        {
            animator.SetBool("isMove", false);
            animator.SetBool("isMoveBack", false);
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

    public void SetIfMoveAble(bool flag)
    {
        ifMoveAble = flag;
    }
}
