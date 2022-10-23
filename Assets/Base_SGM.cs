using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Base_SGM : MonoBehaviour
{
    public static Base_SGM instance;
    public static AnimationCurve RecycleCurve;
    public static AnimationCurve FadeCurve;
    public static AnimationCurve AscendCurve;

    protected GameObject Info_Panel;
    protected GameObject MainCamera;
    public GameObject DialogBox;
    protected GameObject LoadingBox;
    protected TextMeshPro DialogTMP;
    public DialogHint dialogHint;

    protected void Base_Awake()
    {
        Info_Panel = GameObject.Find("System_Panel");
        MainCamera = GameObject.Find("MainCamera");

        DialogBox = MyUtil.FindTransformInChildren(Info_Panel.transform, "DialogBox").gameObject;
        DialogTMP = MyUtil.FindTransformInChildren(Info_Panel.transform, "DialogTMP").gameObject.GetComponent<TextMeshPro>();
        LoadingBox = MyUtil.FindTransformInChildren(Info_Panel.transform, "LoadingBox").gameObject;
    }

    public void ShowDialogBox(string s, DialogHint dialogHint)
    {
        this.dialogHint = dialogHint;
        DialogBox.SetActive(true);
        DialogTMP.text = s;
    }
}
