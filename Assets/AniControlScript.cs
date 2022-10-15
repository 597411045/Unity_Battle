using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AniControlScript : MonoBehaviour
{
    Animator animator;
    AnimatorController ac;
    //Quaternion originRotation;
    bool ifNeedChangeRotation;
    bool ifNeedResoreRotation;
    MakeDamageScript makeDamageScript;

    float angle;
    GameObject Bone;
    BaseActionControlScript actionControlScript;

    string[] actions;
    // Start is called before the first frame update
    void Start()
    {
        Bone = GameObject.Find("mixamorig1:Hips");
        animator = this.GetComponent<Animator>();
        //originRotation = this.transform.rotation;
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
        actions = new string[] { "isPunch", "isMartelo" };
    }

    // Update is called once per frame
    void Update()
    {


        if (ifNeedChangeRotation)
        {
            if (actionControlScript.XAxis > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0) * Quaternion.AngleAxis(angle, Vector3.up);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0) * Quaternion.AngleAxis(angle, Vector3.up);
            }
            ifNeedChangeRotation = false;
        }
        if (animator.GetBool("isDeath")) return;

        if (ifNeedResoreRotation)
        {
            this.transform.rotation = this.transform.parent.rotation;
            ifNeedResoreRotation = false;
        }
    }

    public void BeginPunch()
    {
        makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand").GetComponent<MakeDamageScript>();
        if (makeDamageScript != null)
        {
            makeDamageScript.damageMultipler = 1;
            makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
            makeDamageScript.enabled = true;
        }
        actionControlScript.SetIfMoveAble(false);
    }

    public void EndPunch()
    {
        animator.SetBool("isPunch", false);
    }
    public void BeginMartelo()
    {
        angle = 120;
        ifNeedChangeRotation = true;
        makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").GetComponent<MakeDamageScript>();
        makeDamageScript.damageMultipler = 2;
        makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        makeDamageScript.enabled = true;

        actionControlScript.SetIfMoveAble(false);
    }

    public void EndMartelo()
    {
        ifNeedResoreRotation = true;
        animator.SetBool("isMartelo", false);
    }

    public void BeginFireBall()
    {
        angle = 120;
        ifNeedChangeRotation = true;
        //makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").GetComponent<MakeDamageScript>();
        //makeDamageScript.damageMultipler = 2;
        //makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        //makeDamageScript.enabled = true;

        actionControlScript.SetIfMoveAble(false);

    }

    public void EndFireBall()
    {
        ifNeedResoreRotation = true;
    }
    public void BeginInjured()
    {
        if (makeDamageScript != null)
        {
            makeDamageScript.End();
            makeDamageScript = null;
        }
        actionControlScript.SetIfMoveAble(false);

    }

    public void EndInjured()
    {
        ifNeedResoreRotation = true;
        animator.SetBool("isInjured", false);
    }

    public void BeginBlock()
    {
    }

    public void EndBlock()
    {
    }

    public void EndBlockSuccess()
    {
        ifNeedResoreRotation = true;
    }

    public void BeginInterrupt()
    {

    }
    public void EndInterrupt()
    {
        ifNeedResoreRotation = true;
    }

    public void BeginDeath()
    {
        angle = 120;
        ifNeedChangeRotation = true;
        actionControlScript.SetIfMoveAble(false);
        EndAllAction();
    }

    public void BeginBattleStatus()
    {
        actionControlScript.SetIfMoveAble(true);
        if (makeDamageScript != null)
        {
            makeDamageScript.End();
            makeDamageScript = null;
        }
    }



    public void EndAllAction()
    {
        foreach (string item in actions)
        {
            animator.SetBool(item, false);
        }
    }
}
