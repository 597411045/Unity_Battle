using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragScript : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
