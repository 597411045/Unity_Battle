using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseActionControlScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Animator animator;
    protected bool ifMoveAble;
    protected List<Collider> damageBodys;
    public int XAxis;
    public GameObject verser;

    public bool isOutOfGround;

    public Image HPBar;
    public AniControlScript aniControlScript;


    protected void Init()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        aniControlScript = this.GetComponentInChildren<AniControlScript>();
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

    public void UpdateXAxis()
    {
        XAxis = this.transform.position.x - verser.transform.position.x > 0 ? -1 : 1;
    }

}
