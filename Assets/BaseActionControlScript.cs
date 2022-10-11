using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseActionControlScript : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected Animator animator;
    protected bool ifMoveAble;
    protected List<Collider> damageBodys;
    public bool isAttacker;
    Canvas UICanvas;
    Camera UICamera;
    bool isDetecting;

    public string verseColliderName;
    public string myColliderName;
    Vector3 firstContactPoint;

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

        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        isDetecting = true;
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
        if (!isDetecting) return;
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<BaseActionControlScript>().myColliderName = collision.collider.name;
            verseColliderName = collision.collider.name;
            firstContactPoint = collision.contacts[0].point;
            isDetecting = false;
            StartCoroutine(CorTest());
        }
    }

    IEnumerator CorTest()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("123");
    }

    
}
