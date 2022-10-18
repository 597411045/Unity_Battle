using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AniControlSwordScript : BaseAniControlScript
{
    void Start()
    {
        base.BaseStart();
        //Time.timeScale = 0.25f;
        //Time.fixedDeltaTime = 0.02f * 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
    }

    public override void Begin_H()
    {
        angle = 135;
        ifNeedChangeRotation = true;

        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "Sword");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = 3;
            makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
            makeDamageScript.enabled = true;
            actionControlScript.SetIfMoveAble(false);
            return;
        }
    }

    public override void End_H()
    {
        ifNeedResoreRotation = true;
        animator.SetBool("is_H", false);
    }
    public override void Begin_J()
    {
        angle = 135;
        ifNeedChangeRotation = true;

        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "Sword");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = 5;
            makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
            makeDamageScript.enabled = true;
            actionControlScript.SetIfMoveAble(false);
            return;
        }

    }

    public override void End_J()
    {
        ifNeedResoreRotation = true;
        animator.SetBool("is_J", false);
    }

    public override void Begin_SDH()
    {
        angle = 135;
        ifNeedChangeRotation = true;
        //makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").GetComponent<MakeDamageScript>();
        //makeDamageScript.damageMultipler = 2;
        //makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        //makeDamageScript.enabled = true;

        actionControlScript.SetIfMoveAble(false);

    }

    public override void End_SDH()
    {
        ifNeedResoreRotation = true;
    }

}
