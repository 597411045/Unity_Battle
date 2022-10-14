using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamageScript : MonoBehaviour
{
    Canvas UICanvas;
    Camera UICamera;
    int bodyDamage;
    int totalDamage;
    Animator animator;
    Rigidbody rigidbody;
    BaseActionControlScript actionControlScript;
    string DebugText;
    string UIText;


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



        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Block")
        {
            totalDamage = bodyDamage * damageMultipler / 2;
            DebugText = this.gameObject.name + "格挡了来自于【" + HiterName + "】的伤害:" + totalDamage;
            UIText = "格挡部位：" + this.gameObject.name.Replace("mixamorig1:", "")
                + "伤害值：" + totalDamage;
            actionControlScript.SetIfMoveAble(false);
            rigidbody.velocity = damageForce * totalDamage * 0.1f;
            actionControlScript.HPBar.fillAmount -= totalDamage / 100f;
        }
        else
        {
            totalDamage = bodyDamage * damageMultipler;
            DebugText = this.gameObject.name + "受到了来自于【" + HiterName + "】的伤害:" + totalDamage;
            UIText = "受伤部位：" + this.gameObject.name.Replace("mixamorig1:", "")
                + "伤害值：" + totalDamage;
            animator.SetTrigger("isInjured");
            actionControlScript.SetIfMoveAble(false);
            rigidbody.velocity = damageForce * totalDamage * 0.1f;
            actionControlScript.HPBar.fillAmount -= totalDamage / 100f;
        }
        Debug.Log(DebugText);
        go.GetComponent<Text>().text = UIText;
        actionControlScript.HPText.text = (actionControlScript.HPBar.fillAmount * 100).ToString();
        if (actionControlScript.HPBar.fillAmount <= 0)
        {
            animator.SetBool("isDeath", true);
        }


    }
}
