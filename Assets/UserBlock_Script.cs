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
            LOGIN_SGM_Script.instance.PassToLoginStage();
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "Dialog_UserBlock")
        {
            LOGIN_SGM_Script.instance.DialogBox.SetActive(false);
        }
    }


}
