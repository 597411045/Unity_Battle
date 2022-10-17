using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AniControlHandScript : BaseAniControlScript
{

    void Start()
    {
        base.BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
    }

    public override void Begin_H()
    {
        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = 1;
            makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
            makeDamageScript.enabled = true;
            actionControlScript.SetIfMoveAble(false);
            return;
        }

        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_Hand");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = BattleStageScript.instance.gameData.HandDamageMultipler;
            makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
            makeDamageScript.enabled = true;
            actionControlScript.SetIfMoveAble(false);
            return;
        }
    }

    public override void End_H()
    {
        animator.SetBool("is_H", false);
    }
    public override void Begin_J()
    {
        angle = 120;
        ifNeedChangeRotation = true;
        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = 2;
            makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
            makeDamageScript.enabled = true;
            actionControlScript.SetIfMoveAble(false);
            return;
        }

        makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_ToeBase");
        if (makeDamageTr != null)
        {
            makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
            makeDamageScript.damageMultipler = BattleStageScript.instance.gameData.FootDamageMultipler;
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
        angle = 120;
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
