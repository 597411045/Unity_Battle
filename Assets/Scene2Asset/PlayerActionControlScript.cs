using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionControlScript : BaseActionControlScript
{
    bool isNeedClearKey;
    Text KeyCodeBar;
    Dictionary<string, string> StyleDic;
    public StringBuilder keyCodes;
    string keyCodesUI;

    private void Awake()
    {
        base.InitAwake();
        isNeedClearKey = true;
        keyCodes = new StringBuilder();
        StyleDic = new Dictionary<string, string>();
        StyleDic.Add("is_SDH", "SDH");
        StyleDic.Add("isCTPunch", "SASDH");
        StyleDic.Add("is_H", "H");
        StyleDic.Add("is_J", "J");
    }
    // Start is called before the first frame update
    void Start()
    {
        base.InitStart();
        verser = GameObject.Find("Enemy");
        HPBar = MyUtil.FindTransformInChildren(GameObject.Find("PlayerHPBar").transform, "HPValue").GetComponent<Image>();
        KeyCodeBar = GameObject.Find("KeyCodeBar").GetComponentInChildren<Text>();
        HPText = GameObject.Find("PlayerHPBar").GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXAxis();
        this.transform.eulerAngles = new Vector3(0, 90 * XAxis, 0);

        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);

        if (ifMoveAble)
        {
            if (KeyDetect()) return;

            if (isOutOfGround == false)
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
                    if (Mathf.Abs(_rigidbody.velocity.x) < 2)
                    {
                        _rigidbody.velocity += new Vector3(-2 + Mathf.Abs(_rigidbody.velocity.x), 0, 0);
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
                    if (Mathf.Abs(_rigidbody.velocity.x) < 2)
                    {
                        _rigidbody.velocity += new Vector3(2 - Mathf.Abs(_rigidbody.velocity.x), 0, 0);
                    }
                }
                if (!isOutOfGround && Input.GetKeyDown(KeyCode.Space))
                {
                    _rigidbody.velocity += new Vector3(0, 5, 0);
                }
                if (Input.GetKey(KeyCode.K))
                {
                    animator.SetBool("isBlock", true);
                    baseAniControl.EndAllAction();
                }
                else
                {
                    animator.SetBool("isBlock", false);
                }
            }
        }


    }

    bool KeyDetect()
    {
        if (Input.GetKeyDown(KeyCode.A)) keyCodes.Append("A");
        if (Input.GetKeyDown(KeyCode.S)) keyCodes.Append("S");
        if (Input.GetKeyDown(KeyCode.D)) keyCodes.Append("D");
        if (Input.GetKeyDown(KeyCode.H)) keyCodes.Append("H");
        if (Input.GetKeyDown(KeyCode.J)) keyCodes.Append("J");

        keyCodesUI = keyCodes.ToString();
        KeyCodeBar.text = keyCodesUI.Replace('A', '←').Replace('S', '↓').Replace('D', '→').Replace('H', 'a').Replace('j', 'B');

        if (keyCodes.Length > 0)
        {
            foreach (KeyValuePair<string, string> item in StyleDic)
            {
                if (item.Value.Equals(keyCodes.ToString()))
                {
                    animator.SetTrigger(item.Key);
                    keyCodes.Clear();
                    return true;
                }
            }

            foreach (KeyValuePair<string, string> item in StyleDic)
            {
                int tmpInt = item.Value.IndexOf(keyCodes.ToString());
                if (tmpInt == 0)
                {
                    return false;
                }
            }

            keyCodes.Clear();
        }
        return false;
        //foreach (KeyCode item in keyCodes)
        //{
        //    if (item == KeyCode.A) KeyCodeBar.text += "←";
        //    if (item == KeyCode.S) KeyCodeBar.text += "↓";
        //    if (item == KeyCode.D) KeyCodeBar.text += "→";
        //    if (item == KeyCode.H) KeyCodeBar.text += "A";
        //    if (item == KeyCode.J) KeyCodeBar.text += "B";
        //}

    }


}
