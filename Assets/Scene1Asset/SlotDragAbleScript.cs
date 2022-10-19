using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragAbleScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject DragGO;
    GameObject DragGOParent;
    ItemCountAbleScript Old_iCAS;
    ItemCountAbleScript New_iCAS;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!eventData.pointerEnter.name.Contains("Slot")) return;
        if (eventData.pointerEnter.transform.childCount == 0) return;

        DragGO = eventData.pointerEnter.transform.GetChild(0).gameObject;
        DragGOParent = eventData.pointerEnter;

        if (DragGO.name.Contains("Item"))
        {
            Old_iCAS = DragGO.GetComponentInChildren<ItemCountAbleScript>();
            if (Old_iCAS.GetCountText() > 1)
            {
                Old_iCAS.CountSS();
                DragGO = Instantiate(Resources.Load("Prefabs/Item_Coin"), DragGOParent.transform) as GameObject;
                New_iCAS = DragGO.GetComponentInChildren<ItemCountAbleScript>();
                New_iCAS.SetCount(1);
                DragGO.transform.SetParent(null);
            }
            else
            {
                DragGOParent.transform.DetachChildren();
            }
        }
        else
        {
            DragGOParent.transform.DetachChildren();
        }
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
        if (eventData.pointerEnter == null || (!eventData.pointerEnter.name.Contains("Slot"))) { EndIfPutToNull(); return; }
        if (eventData.pointerEnter.transform.childCount == 0)
        {
            if (eventData.pointerEnter.name == "Slot")
            {
                DragGOParent = eventData.pointerEnter;
            }
            else if (eventData.pointerEnter.name.Split('_')[1] == DragGO.name.Split('_')[0])
            {
                DragGOParent = eventData.pointerEnter;
            }
            else
            {
                if (New_iCAS != null)
                {
                    EndIfItemPutToEmpyButTypeWorng();
                    return;
                }
            }
            EndIfPutToEmpty();
        }
        else
        {
            EndIfPutToFull(eventData);
        }
    }

    void EndIfPutToNull()
    {
        if (DragGO.name.Contains("Item") && New_iCAS != null)
        {
            Destroy(DragGO);
            Old_iCAS.CountPP();
        }
        else
        {
            DragGO.transform.SetParent(DragGOParent.transform);
            DragGO.transform.localPosition = new Vector3(0, 0, 0);
        }
        DragGO = null;
        DragGOParent = null;
        Old_iCAS = null;
        New_iCAS = null;
    }
    void EndIfPutToEmpty()
    {
        DragGO.transform.SetParent(DragGOParent.transform);
        DragGO.transform.localPosition = new Vector3(0, 0, 0);
        DragGO = null;
        DragGOParent = null;
        Old_iCAS = null;
        New_iCAS = null;
    }

    void EndIfItemPutToEmpyButTypeWorng()
    {
        Destroy(DragGO);
        Old_iCAS.CountPP();
        DragGO = null;
        DragGOParent = null;
        Old_iCAS = null;
        New_iCAS = null;
    }

    void EndIfPutToFull(PointerEventData eventData)
    {
        if (DragGO.name == eventData.pointerEnter.transform.GetChild(0).gameObject.name)
        {
            eventData.pointerEnter.transform.GetChild(0).gameObject.GetComponent<ItemCountAbleScript>().CountPP();
            Destroy(DragGO);
        }
        else if (DragGO.name.Contains("Item") && New_iCAS != null)
        {
            Destroy(DragGO);
            Old_iCAS.CountPP();
        }
        else
        {
            DragGO.transform.SetParent(DragGOParent.transform);
            DragGO.transform.localPosition = new Vector3(0, 0, 0);
        }
        DragGO = null;
        DragGOParent = null;
        Old_iCAS = null;
        New_iCAS = null;
    }
}
