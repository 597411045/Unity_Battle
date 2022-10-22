using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOGIN_SGM_Script : SingleTon<LOGIN_SGM_Script>
{
    GameObject Welcome_Panel;
    GameObject Login_Panel;
    GameObject Info_Panel;
    GameObject particleSystem;
    GameObject windZone;
    GameObject MainCamera;

    public AnimationCurve recycleCurve;
    public AnimationCurve fadeCurve;
    public AnimationCurve ascendCurve;

    private void Awake()
    {
        base.InitInstance(this);

        Welcome_Panel = GameObject.Find("Welcome_Panel");
        Login_Panel = GameObject.Find("Login_Panel");
        Info_Panel = GameObject.Find("System_Panel");
        particleSystem = GameObject.Find("particleSystem");
        windZone = GameObject.Find("WindZone"); ;
        MainCamera = GameObject.Find("MainCamera");

        LoginForm = MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginForm").gameObject;
        Register_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Register_Button").gameObject;
        Login_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Login_Button").gameObject;
        Name_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Name_Text").gameObject.GetComponentInChildren<Text>();
        Password_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Password_Text").gameObject.GetComponentInChildren<Text>();
        Login_Panel.SetActive(false);


        DialogBox = MyUtil.FindTransformInChildren(Info_Panel.transform, "DialogBox").gameObject;
        DialogTMP = MyUtil.FindTransformInChildren(Info_Panel.transform, "DialogTMP").gameObject.GetComponent<TextMeshPro>();
        MyUtil.FindTransformInChildren(Info_Panel.transform, "LoadingBox").gameObject.SetActive(false);
        DialogBox.SetActive(false);


        MainCamera.GetComponent<Animation>().Play("CameraAni_1");
    }



    public float timer;
    bool isTiming;
    bool isCheckingLogin;
    GameObject LoginForm;
    UserInfo userInfo;

    private void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            LoginForm.GetComponent<CanvasGroup>().alpha = ascendCurve.Evaluate(timer);

            if (timer >= 1)
            {
                isTiming = false;
            }
        }
        if (isCheckingLogin)
        {
            userInfo = MyUtil.GetClassFromBinary<UserInfo>(MyUtil.mainPath + "/userInfo");
            if (userInfo != null)
            {
                ResetLoginButton();
                isCheckingLogin = false;
            }
        }
    }


    public void PassToLoginStage()
    {
        MainCamera.GetComponent<Animation>().Play("CameraAni_2");
        MyUtil.FindTransformInChildren(Welcome_Panel.transform, "GameTitle").gameObject.GetComponent<Animation>().Play("GameNameAnimation");
        MyUtil.FindTransformInChildren(Welcome_Panel.transform, "GameTitle").gameObject.GetComponent<UI_BlinkScript>().enabled = true;
        MyUtil.FindTransformInChildren(Welcome_Panel.transform, "GameTitle").gameObject.GetComponent<UI_BlinkScript>().FadeModeOn();
        MyUtil.FindTransformInChildren(Welcome_Panel.transform, "PressHint").gameObject.GetComponent<UI_BlinkScript>().FadeModeOn();
    }

    public void ShowLoginUI()
    {
        Login_Panel.SetActive(true);
        Welcome_Panel.SetActive(false);
        MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginTitle").gameObject.GetComponent<UI_BlinkScript>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginTitle").gameObject.GetComponent<UI_BlinkScript>().AscendModeOn();
        MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginBackground").gameObject.GetComponent<UI_BlinkScript>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginBackground").gameObject.GetComponent<UI_BlinkScript>().AscendModeOn();
        isTiming = true;
        StartCoroutine(CR_ShowLoginDialog());
    }

    IEnumerator CR_ShowLoginDialog()
    {
        yield return new WaitForSeconds(1);
        ShowDialogBox("首先，要完成登录环节。点击<color=red>注册按钮</color>，跳转网页端进行注册。注册成功后，输入信息，进行登录。跳过此环节，可以点击游客登录。");
    }

    GameObject Register_Button;
    public void OnRegisterButtonClick()
    {
        Register_Button.GetComponentInChildren<Text>().enabled = false;
        Register_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Register_Button.GetComponent<Image>().raycastTarget = false;
        StartCoroutine(CR_ResetRegisterButton());
    }

    IEnumerator CR_ResetRegisterButton()
    {
        yield return new WaitForSeconds(1);
        Register_Button.GetComponentInChildren<Text>().enabled = true;
        Register_Button.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Register_Button.GetComponent<Image>().raycastTarget = true;
    }

    GameObject Login_Button;
    public Text Name_Text;
    public Text Password_Text;

    public void OnLoginButtonClick()
    {
        Login_Button.GetComponentInChildren<Text>().enabled = false;
        Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Login_Button.GetComponent<Image>().raycastTarget = false;
        isCheckingLogin = true;
    }

    public void ResetLoginButton()
    {
        Login_Button.GetComponentInChildren<Text>().enabled = true;
        Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Login_Button.GetComponent<Image>().raycastTarget = true;
    }



    public GameObject DialogBox;
    TextMeshPro DialogTMP;

    public void ShowDialogBox(string s)
    {
        DialogBox.SetActive(true);
        DialogTMP.text = s;
    }


    public void ChangeScene()
    {
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
