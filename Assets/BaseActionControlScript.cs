using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionControlScript : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected Animator animator;
    protected bool ifMoveAble;
    protected List<DamageBody> damageBodys;

    protected void Init()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        animator.SetBool("isBattleStatus", true);
        ifMoveAble = true;
        damageBodys = new List<DamageBody>();
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").gameObject.GetComponent<DamageBody>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightToeBase").gameObject.GetComponent<DamageBody>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand").gameObject.GetComponent<DamageBody>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightHand").gameObject.GetComponent<DamageBody>());
    }

    public void SetIfMoveAble(bool flag)
    {
        ifMoveAble = flag;
    }

    public void SetDamageBody(bool flag)
    {
        foreach (DamageBody item in damageBodys)
        {
            item.active = flag;
        }
    }
}
