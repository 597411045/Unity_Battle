using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseAniControlScript : MonoBehaviour
{
    public Animator animator;
    //Quaternion originRotation;
    public bool ifNeedChangeRotation;
    public bool ifNeedResoreRotation;
    public MakeDamageScript makeDamageScript;
    public Transform makeDamageTr;

    public float angle;
    public GameObject Bone;
    public BaseActionControlScript actionControlScript;

    public string[] actions;


    public void BaseStart()
    {
        Bone = GameObject.Find("mixamorig1:Hips");
        animator = this.GetComponent<Animator>();
        //originRotation = this.transform.rotation;
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();
        actions = new string[] { "is_H", "is_J", "isInjured" };
    }

    public void BaseUpdate()
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
        if (animator.GetBool("isVectory")) return;

        if (ifNeedResoreRotation)
        {
            this.transform.rotation = this.transform.parent.rotation;
            ifNeedResoreRotation = false;
        }
    }

    public virtual void Begin_H()
    {
        //makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand");
        //if (makeDamageTr != null)
        //{
        //    makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
        //    makeDamageScript.damageMultipler = 1;
        //    makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
        //    makeDamageScript.enabled = true;
        //    actionControlScript.SetIfMoveAble(false);
        //    return;
        //}

        //makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_Hand");
        //if (makeDamageTr != null)
        //{
        //    makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
        //    makeDamageScript.damageMultipler = BattleStageScript.instance.gameData.HandDamageMultipler;
        //    makeDamageScript.damageForce = new Vector3(0.5f * actionControlScript.XAxis, 0, 0); ;
        //    makeDamageScript.enabled = true;
        //    actionControlScript.SetIfMoveAble(false);
        //    return;
        //}
    }

    public virtual void End_H()
    {
        //animator.SetBool("is_H", false);
    }
    public virtual void Begin_J()
    {
        //angle = 120;
        //ifNeedChangeRotation = true;
        //makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase");
        //if (makeDamageTr != null)
        //{
        //    makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
        //    makeDamageScript.damageMultipler = 2;
        //    makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        //    makeDamageScript.enabled = true;
        //    actionControlScript.SetIfMoveAble(false);
        //    return;
        //}

        //makeDamageTr = MyUtil.FindTransformInChildren(this.transform, "J_Bip_L_ToeBase");
        //if (makeDamageTr != null)
        //{
        //    makeDamageScript = makeDamageTr.gameObject.GetComponent<MakeDamageScript>();
        //    makeDamageScript.damageMultipler = BattleStageScript.instance.gameData.FootDamageMultipler;
        //    makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        //    makeDamageScript.enabled = true;
        //    actionControlScript.SetIfMoveAble(false);
        //    return;
        //}
    }

    public virtual void End_J()
    {
        //ifNeedResoreRotation = true;
        //animator.SetBool("is_J", false);
    }

    public virtual void Begin_SDH()
    {
        //angle = 120;
        //ifNeedChangeRotation = true;
        //makeDamageScript = MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").GetComponent<MakeDamageScript>();
        //makeDamageScript.damageMultipler = 2;
        //makeDamageScript.damageForce = new Vector3(1 * actionControlScript.XAxis, 1, 0); ;
        //makeDamageScript.enabled = true;
        //actionControlScript.SetIfMoveAble(false);
    }

    public virtual void End_SDH()
    {
        //ifNeedResoreRotation = true;
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

    public void EndBlockSuccess()
    {
        ifNeedResoreRotation = true;
    }

    public void BeginBlock()
    {

    }
    public void EndIBlock()
    {
        //ifNeedResoreRotation = true;
    }

    public void BeginDeath()
    {
        angle = 120;
        ifNeedChangeRotation = true;
        actionControlScript.SetIfMoveAble(false);
        EndAllAction();

    }

    public void EndDeath()
    {
        BS_SGM_Script.instance.EndDuel();
    }

    public void BeginVectory()
    {
        angle = actionControlScript.XAxis > 0 ? 180 : 0;
        ifNeedChangeRotation = true;
        actionControlScript.SetIfMoveAble(false);
        EndAllAction();

    }

    public void EndVectory()
    {
    }

    public virtual void BeginBattleStatus()
    {
        actionControlScript.SetIfMoveAble(true);
        if (makeDamageScript != null)
        {
            makeDamageScript.End();
            makeDamageScript = null;
        }
        EndAllAction();
    }



    public void EndAllAction()
    {
        foreach (string item in actions)
        {
            animator.SetBool(item, false);
        }
    }
}
