using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PS_TEST_PlayerAction : MonoBehaviour
{

    RaycastHit raycastHit;
    Camera uiCamera;

    PointerEventData ped;
    List<RaycastResult> raycastResults;

    public static bool UserActAble;


    private void Start()
    {
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();

        ped = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
        UserActAble = true;
    }

    private void Update()
    {
        if (UserActAble)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LOGIN_SGM_Script.Instance.Notify(EventName.MouseLeftClick, this, new MouseLeftClickEventArgs { });
            }
            if (Input.GetMouseButton(0))
            {
                ped.position = Input.mousePosition;
                EventSystem.current.RaycastAll(ped, raycastResults);
                if (raycastResults.Count > 0)
                {
                    LOGIN_SGM_Script.Instance.Notify(EventName.MouseLeftClick, this, new MouseLeftClickEventArgs() { clickedGameObject = raycastResults[0].gameObject });
                }
            }
        }
    }
}

public class MouseLeftClickEventArgs : EventArgs
{
    public GameObject clickedGameObject;
}
public static class EventName
{
    public const string MouseLeftClick = "MouseLeftClick";
    public const string DialogueUB_Click = "DialogueUB_Click";
    public const string RegisterButton_Click = "RegisterButton_Click";
    public const string LoginButton_Click = "LoginButton_Click";
}