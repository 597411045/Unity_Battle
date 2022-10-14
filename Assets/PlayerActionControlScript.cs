using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionControlScript : BaseActionControlScript
{
    public List<KeyCode> keyCodes;
    bool isNeedClearKey;
    Text KeyCodeBar;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        verser = GameObject.Find("P2");
        HPBar = MyUtil.FindTransformInChildren(GameObject.Find("PlayerHPBar").transform, "HPValue").GetComponent<Image>();
        KeyCodeBar = GameObject.Find("KeyCodeBar").GetComponentInChildren<Text>();
        HPText = GameObject.Find("PlayerHPBar").GetComponentInChildren<Text>();
        List<KeyCode> keyCodes = new List<KeyCode>();
        isNeedClearKey = true;
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
                if (Mathf.Abs(rigidbody.velocity.x) < 2)
                {
                    rigidbody.velocity += new Vector3(-2 + Mathf.Abs(rigidbody.velocity.x), 0, 0);
                }
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
                if (Mathf.Abs(rigidbody.velocity.x) < 2)
                {
                    rigidbody.velocity += new Vector3(2 - Mathf.Abs(rigidbody.velocity.x), 0, 0);
                }
            }
            if (!isOutOfGround && Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.velocity += new Vector3(0, 5, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                animator.SetBool("isBlock", true);
            }
            else
            {
                animator.SetBool("isBlock", false);
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

        KeyDetect();
    }

    void KeyDetect()
    {
        if (Input.GetKeyDown(KeyCode.A)) keyCodes.Add(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.S)) keyCodes.Add(KeyCode.S);
        if (Input.GetKeyDown(KeyCode.D)) keyCodes.Add(KeyCode.D);
        if (Input.GetKeyDown(KeyCode.H)) keyCodes.Add(KeyCode.H);
        if (Input.GetKeyDown(KeyCode.J)) keyCodes.Add(KeyCode.J);
        KeyCodeBar.text = "";
        foreach (KeyCode item in keyCodes)
        {
            if(item== KeyCode.A) KeyCodeBar.text += "←";
            if(item== KeyCode.S) KeyCodeBar.text += "↓";
            if(item== KeyCode.D) KeyCodeBar.text += "→";
            if(item== KeyCode.H) KeyCodeBar.text += "A";
            if(item== KeyCode.J) KeyCodeBar.text += "B";
        }
        if (isNeedClearKey)
        {
            StartCoroutine(DeleteFirstKeyCode());
            isNeedClearKey = false;
        }
    }

    IEnumerator DeleteFirstKeyCode()
    {
        yield return new WaitForSeconds(1);
        keyCodes.RemoveAt(0);
        isNeedClearKey = true;
    }

}
