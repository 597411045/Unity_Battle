using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamageScript : MonoBehaviour
{
    Canvas UICanvas;
    Camera UICamera;
    int bodyDamage;
    Animator animator;
    Rigidbody rigidbody;
    BaseActionControlScript actionControlScript;


    private void Start()
    {
        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        animator = this.GetComponentInParent<Animator>();
        rigidbody = this.GetComponentInParent<Rigidbody>();
        actionControlScript = this.GetComponentInParent<BaseActionControlScript>();

    }

    public void takeDamage(Vector3 HiterPosition, string HiterName, int damageMultipler, Vector3 damageForce)
    {
        Vector3 v = Camera.main.WorldToViewportPoint(HiterPosition);
        GameObject go = Instantiate(Resources.Load("Prefabs\\Hint"), UICamera.ViewportToWorldPoint(new Vector3(v.x, v.y, 5)), Quaternion.identity, UICanvas.transform) as GameObject;

        bodyDamage = 2;
        if (this.gameObject.name.Contains("Head"))
        {
            bodyDamage = 10;
        }
        if (this.gameObject.name.Contains("Hips"))
        {
            bodyDamage = 5;
        }
        Debug.Log(this.gameObject.name + "受到了来自于【" + HiterName + "】的伤害:" + bodyDamage * damageMultipler);

        go.GetComponent<Text>().text = "受伤部位：" + this.gameObject.name.Replace("mixamorig1:", "")
            + "伤害值：" + bodyDamage * damageMultipler;

        animator.SetBool("isInjured", true);
        actionControlScript.SetIfMoveAble(false);
        rigidbody.velocity = damageForce * bodyDamage * damageMultipler * 0.1f;
        this.GetComponentInParent<BaseActionControlScript>().HPBar.fillAmount -= 0.1f;

    }
}
