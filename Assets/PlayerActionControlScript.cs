using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionControlScript : BaseActionControlScript
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();

    }

    // Update is called once per frame
    void Update()
    {
        if (ifMoveAble)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody.velocity = new Vector3(-1, 0, 0);
                animator.SetBool("isMoveBack", true);
            }
            else
            {
                animator.SetBool("isMoveBack", false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody.velocity = new Vector3(1, 0, 0);
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
    
}
