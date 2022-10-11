using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionControlScript : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected Animator animator;
    protected bool ifMoveAble;
    protected List<Collider> damageBodys;
    public bool isAttacker;

    protected void Init()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        animator.SetBool("isBattleStatus", true);
        ifMoveAble = true;
        damageBodys = new List<Collider>();
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightToeBase").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightHand").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:Hips").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftUpLeg").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftLeg").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightUpLeg").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightLeg").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftArm").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftForeArm").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightArm").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightForeArm").gameObject.GetComponent<Collider>());
        damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:Head").gameObject.GetComponent<Collider>());
    }

    public void SetIfMoveAble(bool flag)
    {
        ifMoveAble = flag;
    }

    public void SetDamageBody(bool flag)
    {
        foreach (Collider item in damageBodys)
        {
            item.enabled = flag;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            if (isAttacker)
            {
                Debug.Log(collision.gameObject.name + "+" + collision.collider.name);
                collision.rigidbody.AddForce(collision.contacts[0].normal * -250);
                collision.gameObject.GetComponent<BaseActionControlScript>().SetDamageBody(false);
                collision.gameObject.GetComponent<BaseActionControlScript>().animator.SetBool("isInjured",true);
            }
        }
    }
}
