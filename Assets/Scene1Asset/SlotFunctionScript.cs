using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotFunctionScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject DragGO;
    GameObject DragGOParent;
    ItemCountAbleScript Old_iCAS;
    ItemCountAbleScript New_iCAS;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!eventData.pointerEnter.name.Contains("Slot")) return;
        if (eventData.pointerEnter.transform.childCount == 0) return;
        if (eventData.pointerEnter.transform.parent.name == "Store") return;

        DragGO = eventData.pointerEnter.transform.GetChild(0).gameObject;
        DragGOParent = eventData.pointerEnter;

        if (DragGO.name.Contains("Item"))
        {
            Old_iCAS = DragGO.GetComponentInChildren<ItemCountAbleScript>();
            if (Old_iCAS.GetCountText() > 1)
            {
                Old_iCAS.CountS(1);
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
                if (DragGO.name.Contains("Weapon"))
                {
                    Obsolete_PB_SGM_Script.instance.gameData.WeaponType = DragGO.name.Split('_')[1];
                    Obsolete_PB_SGM_Script.instance.gameData.ATK_H = DragGO.GetComponent<ItemDetailScript>().ATK_H;
                    Obsolete_PB_SGM_Script.instance.gameData.ATK_J = DragGO.GetComponent<ItemDetailScript>().ATK_J;
                }
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
            Old_iCAS.CountP(1);
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
        Old_iCAS.CountP(1);
        DragGO = null;
        DragGOParent = null;
        Old_iCAS = null;
        New_iCAS = null;
    }
    void EndIfPutToFull(PointerEventData eventData)
    {
        if (DragGO.name == eventData.pointerEnter.transform.GetChild(0).gameObject.name)
        {
            eventData.pointerEnter.transform.GetChild(0).gameObject.GetComponent<ItemCountAbleScript>().CountP(1);
            Destroy(DragGO);
        }
        else if (DragGO.name.Contains("Item") && New_iCAS != null)
        {
            Destroy(DragGO);
            Old_iCAS.CountP(1);
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

    GameObject PointGO;
    ItemDetailScript ITS_Des;
    ItemCountAbleScript ICAS_Des;
    GameObject MessageBox_Des;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.pointerEnter.name.Contains("Slot")) return;
        if (eventData.pointerEnter.transform.childCount == 0) return;

        PointGO = eventData.pointerEnter.transform.GetChild(0).gameObject;
        ITS_Des = PointGO.GetComponentInChildren<ItemDetailScript>();
        ICAS_Des = PointGO.GetComponentInChildren<ItemCountAbleScript>();
        if (ITS_Des == null) return;

        int tmpInt = 0;
        if (ICAS_Des == null)
        {
            tmpInt = 1;
        }
        else
        {
            tmpInt = ICAS_Des.GetCountText();
        }

        if (eventData.pointerEnter.transform.parent.name == "Store" || eventData.pointerEnter.transform.parent.name == "Equipment")
        {
            MessageBox_Des = Obsolete_PB_SGM_Script.instance.LeftMessageBox;
        }
        else if (eventData.pointerEnter.transform.parent.name == "Bag")
        {
            MessageBox_Des = Obsolete_PB_SGM_Script.instance.RightMessageBox;
        }

        MessageBox_Des.active = true;
        MessageBox_Des.GetComponent<MessageBoxScript>().SetDesText(PointGO, ITS_Des.Id, ITS_Des.Description, ITS_Des.Price * tmpInt);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (MessageBox_Des != null) MessageBox_Des.active = false;
        PointGO = null;
        ITS_Des = null;
        MessageBox_Des = null;
    }

    GameObject ClickGO;
    ItemDetailScript ITS_Buy;
    ItemCountAbleScript ICAS_Buy;
    GameObject MessageBox_Buy;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.transform.parent.name != "Store") return;

        ClickGO = eventData.pointerEnter.transform.GetChild(0).gameObject;
        ITS_Des = ClickGO.GetComponentInChildren<ItemDetailScript>();
        MessageBox_Buy = Obsolete_PB_SGM_Script.instance.MessageBox_Buy;
        MessageBox_Buy.active = true;

        ICAS_Buy = ClickGO.GetComponent<ItemCountAbleScript>();
        int tmpInt = 0;
        if (ICAS_Buy == null)
        {
            tmpInt = 1;
        }
        else
        {
            tmpInt = ICAS_Buy.GetCountText();
        }
        MessageBox_Buy.GetComponent<MessageBoxScript>().SetBuyText(ClickGO, ITS_Des.Id, ClickGO.name, ITS_Des.Price, tmpInt);

    }
}
