using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        if (animator.gameObject.transform.parent.gameObject.name == "CharacterBox")
        {

            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.gameData.ATK_H;
                makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
                makeDamageScript.enabled = true;
                actionControlScript.SetIfMoveAble(false);
                return;
            }

            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_Hand");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.gameData.ATK_H;
                makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
                makeDamageScript.enabled = true;
                actionControlScript.SetIfMoveAble(false);
                return;
            }
        }

        if (animator.gameObject.transform.parent.gameObject.name == "RobotBox")
        {

            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.curLevelData.curDifficult;
                makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
                makeDamageScript.enabled = true;
                actionControlScript.SetIfMoveAble(false);
                return;
            }
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
        actionControlScript.SetIfMoveAble(false);

    }

    public void Begin_J_Damage()
    {
        if (animator.gameObject.transform.parent.gameObject.name == "CharacterBox")
        {
            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.gameData.ATK_J;
                makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
                makeDamageScript.enabled = true;
                return;
            }

            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_ToeBase");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.gameData.ATK_J;
                makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
                makeDamageScript.enabled = true;
                actionControlScript.SetIfMoveAble(false);
                return;
            }
        }
        if (animator.gameObject.transform.parent.gameObject.name == "RobotBox")
        {
            makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase");
            if (makeDamageTr != null)
            {
                makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
                makeDamageScript.damageMultipler = BS_SGM_Script.instance.curLevelData.curDifficult * 2;
                makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
                makeDamageScript.enabled = true;
                return;
            }
        }
    }

    public void End_J_Damage()
    {
        if (makeDamageScript != null)
        {
            makeDamageScript.End();
            makeDamageScript = null;
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
