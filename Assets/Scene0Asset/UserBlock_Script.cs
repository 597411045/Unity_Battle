using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserBlock_Script : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name == "Welcome_UserBlock")
        {
            LOGIN_SGM_Script._Instance.PassToLoginStage();
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "Dialog_UserBlock")
        {
            LOGIN_SGM_Script.instance.DialogBox.SetActive(false);
            if (LOGIN_SGM_Script.instance.dialogHint == DialogHint.LoginSuccess)
            {
                LOGIN_SGM_Script._Instance.ShowLoadingUI();
            }
            if (UI_BlinkScript.defaultImageMaterial != null)
            {
                UI_BlinkScript.defaultImageMaterial.SetColor("_Color", new Color(1, 1, 1, 1));
            }
        }
    }


}
