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

    // Start is called before the first frame update
    void Start()
    {
        Bone = GameObject.Find("mixamorig1:Hips");
        animator = this.GetComponent<Animator>();
        //originRotation = this.transform.rotation;
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ifNeedChangeRotation)
        {
            this.transform.rotation = this.transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
            ifNeedChangeRotation = false;
        }
        if (ifNeedResoreRotation)
        {
            this.transform.rotation = this.transform.parent.rotation;
            ifNeedResoreRotation = false;
        }
    }

    public void BeginPunch()
    {
        makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand").GetComponent<MakeDamageScript>();
        makeDamageScript.damageMultipler = 1;
        makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
        makeDamageScript.enabled = true;

        GeneralBeginAnimation();
    }

    public void EndPunch()
    {

        GeneralEndAnimation("isPunch");

    }
    public void BeginMartelo()
    {
        angle = 30;
        ifNeedChangeRotation = true;
        makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").GetComponent<MakeDamageScript>();
        makeDamageScript.damageMultipler = 2;
        makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        makeDamageScript.enabled = true;

        GeneralBeginAnimation();
    }

    public void EndMartelo()
    {
        ifNeedResoreRotation = true;

        GeneralEndAnimation("isMartelo");

    }
    public void BeginInjured()
    {
        if (makeDamageScript != null)
        {
            makeDamageScript.End();
            makeDamageScript = null;
        }
        GeneralBeginAnimation();
    }

    public void EndInjured()
    {
        ifNeedResoreRotation = true;

        GeneralEndAnimation("isInjured", "isMartelo", "isPunch");
    }

    public void BeginBlock()
    {
        GeneralBeginAnimation();
    }

    public void EndBlock()
    {

    }

    public void EndBlockSuccess()
    {
        ifNeedResoreRotation = true;
        GeneralEndAnimation("isBlockSuccess", "isBlock");
    }

    public void BeginInterrupt()
    {
        GeneralBeginAnimation();
    }
    public void EndInterrupt()
    {
        ifNeedResoreRotation = true;
        GeneralEndAnimation("isInterrupt");
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

    private void GeneralBeginAnimation()
    {
        actionControlScript.SetIfMoveAble(false);
    }

    public void GeneralEndAnimation(params string[] aniNames)
    {
        foreach (string item in aniNames)
        {
            animator.SetBool(item, false);
        }
    }
}
