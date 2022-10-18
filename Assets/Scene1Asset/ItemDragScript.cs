using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject DragGO;
    GameObject DragGOParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name != "Slot") return;
        if (eventData.pointerEnter.transform.childCount == 0) return;

        DragGO = eventData.pointerEnter.transform.GetChild(0).gameObject;
        DragGOParent = eventData.pointerEnter;
        DragGOParent.transform.DetachChildren();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragGO == null) return;
        DragGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DragGO.transform.position = new Vector3(DragGO.transform.position.x,
        DragGO.transform.position.y, 5);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragGO == null) return;
        if (eventData.pointerEnter != null &&
            eventData.pointerEnter.name == "Slot" &&
            eventData.pointerEnter.transform.childCount == 0)
        {
            DragGOParent = eventData.pointerEnter;
            CC_SGM_Script.instance.WeaponType = DragGO.name;
        }
        DragGO.transform.SetParent(DragGOParent.transform);
        DragGO.transform.localPosition = new Vector3(0, 0, 0);
        DragGO = null;
        DragGOParent = null;
    }
}
