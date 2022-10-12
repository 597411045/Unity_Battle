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
        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);
        if (ifMoveAble)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isMoveBack", true);
                _rigidbody.velocity = new Vector3(-1, 0, 0);

            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isMove", true);
                _rigidbody.velocity = new Vector3(1, 0, 0);

            }
            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            //this.transform.rotation = Quaternion.Euler(0, 90, 0);
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
