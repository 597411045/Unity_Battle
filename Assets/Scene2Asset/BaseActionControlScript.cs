using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseActionControlScript : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Animator animator;
    //obsolete
    //protected List<Collider> damageBodys;
    public GameObject verser;
    public BaseAniControlScript baseAniControl;

    public int XAxis;
    public bool isOutOfGround;
    public bool ifMoveAble;

    public Image HPBar;
    public Text HPText;

    protected void InitAwake()
    {
        ifMoveAble = true;
    }

    protected void InitStart()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        baseAniControl = this.GetComponentInChildren<BaseAniControlScript>();
        animator.SetBool("isBattleStatus", true);

        //obsolete
        //damageBodys = new List<Collider>();
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftToeBase").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightToeBase").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftHand").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightHand").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:Hips").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftUpLeg").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftLeg").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightUpLeg").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightLeg").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftArm").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:LeftForeArm").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightArm").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:RightForeArm").gameObject.GetComponent<Collider>());
        //damageBodys.Add(MyUtil.FindTransformInChildren(this.transform, "mixamorig1:Head").gameObject.GetComponent<Collider>());

    }

    public void SetIfMoveAble(bool flag)
    {
        ifMoveAble = flag;
    }

    public void UpdateXAxis()
    {
        XAxis = this.transform.position.x - verser.transform.position.x > 0 ? -1 : 1;
    }

}
