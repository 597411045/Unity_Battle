using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIActionControlScript : BaseActionControlScript
{
    float DistanceFromVerse;
    // Start is called before the first frame update
    private void Awake()
    {
        base.InitAwake();
    }

    void Start()
    {
        base.InitStart();
        verser = GameObject.Find(BattleStageScript.instance.gameData.CharacterName);
        HPBar = MyUtil.FindTransformInChildren(GameObject.Find("verseHPBar").transform, "HPValue").GetComponent<Image>();
        HPText = GameObject.Find("verseHPBar").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isDeath")) return;

        UpdateXAxis();
        this.transform.eulerAngles = new Vector3(0, 90 * XAxis, 0);
        DistanceFromVerse = Mathf.Abs(verser.transform.position.x - this.transform.position.x);

        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMove", false);

        if (ifMoveAble)
        {
            if (KeyDetect()) return;

            if (isOutOfGround == false)
            {
                if (DistanceFromVerse > 1.5f)
                {
                    animator.SetBool("isMove", true);

                    if (Mathf.Abs(_rigidbody.velocity.x) < 2)
                    {
                        _rigidbody.velocity += new Vector3((2 - Mathf.Abs(_rigidbody.velocity.x)) * XAxis, 0, 0);
                    }
                }
                if (DistanceFromVerse < 1.0f)
                {
                    animator.SetBool("isMoveBack", true);
                    if (Mathf.Abs(_rigidbody.velocity.x) < 2)
                    {
                        _rigidbody.velocity += new Vector3((-2 + Mathf.Abs(_rigidbody.velocity.x)) * XAxis, 0, 0);
                    }
                }
                //if (!isOutOfGround && Input.GetKeyDown(KeyCode.Space))
                //{
                //    _rigidbody.velocity += new Vector3(0, 5, 0);
                //}
                animator.SetBool("isBlock", true);
                baseAniControl.EndAllAction();
                //{
                //    animator.SetBool("isBlock", false);
                //}
            }
        }
    }

    bool KeyDetect()
    {
        if (DistanceFromVerse < 1.0f)
        {
            animator.SetBool("isBlock", false);
            animator.SetTrigger("is_H");
            return true;
        }
        if (DistanceFromVerse > 1.0f && DistanceFromVerse < 1.5f)
        {
            animator.SetBool("isBlock", false);
            animator.SetTrigger("is_J");
            return true;
        }
        return false;
    }
}
