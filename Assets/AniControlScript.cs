using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AniControlScript : MonoBehaviour
{
    Animator animator;
    AnimatorController ac;
    Quaternion originRotation;
    bool ifNeedChangeRotation;
    bool ifNeedResoreRotation;
   
    float angle;
    GameObject Bone;
    BaseActionControlScript actionControlScript;

    // Start is called before the first frame update
    void Start()
    {
        Bone = GameObject.Find("mixamorig1:Hips");
        animator = this.GetComponent<Animator>();
        originRotation = this.transform.rotation;
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
            this.transform.rotation = originRotation;
            ifNeedResoreRotation = false;
        }
    }

    public void BeginPunch()
    {
        actionControlScript.isAttacker = true;

        GeneralBeginAnimation();
    }

    public void EndPunch()
    {
        actionControlScript.isAttacker = false;
        actionControlScript.HadMadeDamageInThisRound = false;

        GeneralEndAnimation("isPunch");

    }
    public void BeginMartelo()
    {
        angle = 30;
        ifNeedChangeRotation = true;
        actionControlScript.isAttacker = true;

        GeneralBeginAnimation();
    }

    public void EndMartelo()
    {
        ifNeedResoreRotation = true;
        actionControlScript.isAttacker = false;
        actionControlScript.HadMadeDamageInThisRound = false;

        GeneralEndAnimation("isMartelo");

    }
    public void BeginInjured()
    {
        actionControlScript.isAttacker = false;

        GeneralBeginAnimation();
    }

    public void EndInjured()
    {
        ifNeedResoreRotation = true;

        GeneralEndAnimation("isInjured", "isMartelo", "isPunch");
    }

    public void BeginBlock()
    {
        angle = 30;
        ifNeedChangeRotation = true;

        GeneralBeginAnimation();
    }


    public void EndBlock()
    {
        ifNeedResoreRotation = true;
        GeneralEndAnimation("isBlock");
    }

    public void EndBlockSuccess()
    {
        ifNeedResoreRotation = true;
        GeneralEndAnimation("isBlockSuccess","isBlock");
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

    private void GeneralBeginAnimation()
    {
        actionControlScript.SetIfMoveAble(false);
    }
    private void GeneralEndAnimation(params string[] aniNames)
    {
        actionControlScript.SetIfMoveAble(true);
        foreach(string item in aniNames)
        {
            animator.SetBool(item, false);
        }
    }
}
