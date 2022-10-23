using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SampleInfoScript : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    GameObject target;
    public void OnBeginDrag(PointerEventData eventData)
    {
        target = eventData.pointerEnter.gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (true)
        {
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + eventData.scrollDelta.y, target.transform.position.z);
        }
    }
}
