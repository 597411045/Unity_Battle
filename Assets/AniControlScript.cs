using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AniControlScript : MonoBehaviour
{
    Animator animator;
    AnimatorController ac;
    Quaternion oldRotation;
    bool ifChangeRotation;
    bool isSetRotationBack;
    float angle;
    GameObject Bone;
    BaseActionControlScript actionControlScript;
    // Start is called before the first frame update
    void Start()
    {
        Bone = GameObject.Find("mixamorig1:Hips");
        animator = this.GetComponent<Animator>();
        ac = animator.runtimeAnimatorController as AnimatorController;
        oldRotation = this.transform.rotation;
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ifChangeRotation)
        {
            this.transform.rotation = this.transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
            ifChangeRotation = false;
        }
        if (isSetRotationBack)
        {
            this.transform.rotation = oldRotation;
            isSetRotationBack = false;
        }
    }

    public void BeginPunch()
    {
        actionControlScript.SetIfMoveAble(false);
        animator.SetBool("isPunch", false);
        actionControlScript.SetDamageBody(true);
    }

    public void EndPunch()
    {
        actionControlScript.SetIfMoveAble(true);
        actionControlScript.SetDamageBody(false);
        animator.SetBool("isPunch", false);

    }
    public void BeginMartelo()
    {
        angle = 30;
        ifChangeRotation = true;
        animator.SetBool("isMartelo", false);
        actionControlScript.SetIfMoveAble(false);
        actionControlScript.SetDamageBody(true);

    }

    public void EndMartelo()
    {
        actionControlScript.SetIfMoveAble(true);
        isSetRotationBack = true;
        actionControlScript.SetDamageBody(false);
        animator.SetBool("isMartelo", false);
    }
    public void BeginInjured()
    {
        actionControlScript.SetIfMoveAble(false);
        animator.SetBool("isInjured", false);
        actionControlScript.SetDamageBody(false);

    }

    public void EndInjured()
    {
        actionControlScript.SetIfMoveAble(true);
        animator.SetBool("isInjured", false);
        isSetRotationBack = true;
    }

    public void BeginBlock()
    {
        angle = 30;
        ifChangeRotation = true;
        actionControlScript.SetIfMoveAble(false);
        animator.SetBool("isBlock", false);
    }

    public void EndBlock()
    {
        actionControlScript.SetIfMoveAble(true);
    }

    public void ReserRotation()
    {
        isSetRotationBack = true;
        actionControlScript.SetIfMoveAble(true);

    }
}
