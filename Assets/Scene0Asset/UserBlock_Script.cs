using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserBlock_Script : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        if (this.gameObject.name == "Dialog_UserBlock")
        {
            //LOGIN_SGM_Script.instance.DialogBox.SetActive(false);
            //if (LOGIN_SGM_Script.instance.dialogHint == DialogHint.LoginSuccess)
            //{
            //    LOGIN_SGM_Script._Instance.ShowLoadingUI();
            //}
        }
    }

}
