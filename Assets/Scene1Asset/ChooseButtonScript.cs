using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseButtonScript : MonoBehaviour,IPointerClickHandler
{
   

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name.Contains("C1"))
        {
            ChooseCharacterScript.instance.C1_On();
        }
        if (this.gameObject.name.Contains("C2"))
        {
            ChooseCharacterScript.instance.C2_On();
        }
    }

}
