using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class PB_SGM_Script : Base_SGM
{
    public static PB_SGM_Script _Instance;

    GameObject SampleInfo_Panel;
    GameObject Login_Panel;

    public AnimationCurve recycleCurve;
    public AnimationCurve fadeCurve;
    public AnimationCurve ascendCurve;

    private void Awake()
    {
        _Instance = this;
        instance = this;
        base.Base_Awake();
        SampleInfo_Panel = GameObject.Find("SampleInfo_Panel");
        //Login_Panel = GameObject.Find("Login_Panel");
        //LoginForm = MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginForm").gameObject;
        //Register_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Register_Button").gameObject;
        //Login_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Login_Button").gameObject;
        //Name_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Name_Text").gameObject.GetComponent<Text>();
        //Password_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Password_Text").gameObject.GetComponent<Text>();
        SampleInfo_Panel.SetActive(false);
        DialogBox.SetActive(false);
        FadeLoadingUI();

        //MainCamera.GetComponent<Animation>().Play("CameraAni_1");
        exhibitionPoint = new Vector3(-50.21f, -25.06f, -13.83f);
    }

    private void Start()
    {
        Base_SGM.RecycleCurve = recycleCurve;
        Base_SGM.FadeCurve = fadeCurve;
        Base_SGM.AscendCurve = ascendCurve;
    }

    public float timer;
    bool isTiming;
    bool isTimingBack;
    GameObject LoginForm;
    RaycastHit raycastHit;
    GameObject pointTarget;
    GameObject choosenTarget;
    Vector3 exhibitionPoint;
    Vector3 oldPoint;
    private void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            SampleInfo_Panel.GetComponent<CanvasGroup>().alpha = ascendCurve.Evaluate(timer);

            if (timer >= 1)
            {
                isTiming = false;
                timer = 0;
            }
        }
        if (isTimingBack)
        {
            timer += Time.deltaTime;
            SampleInfo_Panel.GetComponent<CanvasGroup>().alpha = fadeCurve.Evaluate(timer);

            if (timer >= 1)
            {
                isTimingBack = false;
                timer = 0;
            }
        }
        if (isLoginDone)
        {
            ResetLoginButton(isLoginSuccess);
            isLoginDone = false;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
        {
            if (pointTarget == null && raycastHit.collider.gameObject.name.Contains("Sample"))
            {
                pointTarget = raycastHit.collider.gameObject;
                pointTarget.GetComponent<OutlineScript>().EnableOutline();
            }
            if (!raycastHit.collider.gameObject.name.Contains("Sample") && pointTarget != null)
            {
                pointTarget.GetComponent<OutlineScript>().DisableOutline();
                pointTarget = null;
            }
        }
        else
        {
            if (pointTarget != null)
            {
                pointTarget.GetComponent<OutlineScript>().DisableOutline();
                pointTarget = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && pointTarget != null)
        {
            oldPoint = pointTarget.transform.position;
            choosenTarget = pointTarget;
            pointTarget.GetComponent<NavMeshAgent>().SetDestination(exhibitionPoint);
            MainCamera.GetComponent<Animation>().Play("MainCameraS1_1");
            PassToInfoStage();
        }
    }

    public void PassToInfoStage()
    {
        SampleInfo_Panel.SetActive(true);
        isTiming = true;
    }

    public void BackToChooseStage()
    {
        isTimingBack = true;
        choosenTarget.GetComponent<NavMeshAgent>().SetDestination(oldPoint);
        MainCamera.GetComponent<Animation>().Play("MainCameraS1_2");
        StartCoroutine(CR_BackToChooseStage());
    }

    IEnumerator CR_BackToChooseStage()
    {
        yield return new WaitForSeconds(1);
        SampleInfo_Panel.SetActive(false);
    }

    public void ShowLoginUI()
    {
        Login_Panel.SetActive(true);
        //Welcome_Panel.SetActive(false);
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
        ShowDialogBox("首先，要完成登录环节。点击<color=red>注册按钮</color>，跳转网页端进行注册。注册成功后，输入信息，进行登录。跳过此环节，可以点击游客登录。", DialogHint.Default);
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
    public UserInfo userInfo;
    public bool isLoginDone;
    public bool isLoginSuccess;
    Thread thread_Login;
    DataSet ds;
    DataTable dt;
    string cmdStr;
    public void OnLoginButtonClick()
    {
        Login_Button.GetComponentInChildren<Text>().enabled = false;
        Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Login_Button.GetComponent<Image>().raycastTarget = false;


        thread_Login = new Thread(GetDataAndSaveData);
        thread_Login.Start();

    }
    void GetDataAndSaveData()
    {
        ds = new DataSet();
        cmdStr = "SELECT * FROM `unity`.`tbl_user` WHERE `name`='" + Name_Text.text + "' AND `password`='" + Password_Text.text + "';";
        int result = MyUtil.GetDataFromMySQL(cmdStr, ds);
        if (result == 1)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count == 1)
            {
                isLoginSuccess = true;
            }
        }
        isLoginDone = true;
    }
    public void ResetLoginButton(bool flag)
    {
        Login_Button.GetComponentInChildren<Text>().enabled = true;
        Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Login_Button.GetComponent<Image>().raycastTarget = true;
        if (flag)
        {
            int _id = int.Parse(dt.Rows[0]["id"].ToString());
            string _name = dt.Rows[0]["name"].ToString();
            int _coin = int.Parse(dt.Rows[0]["coin"].ToString());
            int _vipLevel = int.Parse(dt.Rows[0]["vipLevel"].ToString());
            DateTime _lastLoginTime = DateTime.Parse(dt.Rows[0]["lastLoginTime"].ToString());
            userInfo = new UserInfo
            {
                id = _id,
                name = _name,
                coin = _coin,
                vipLevel = _vipLevel,
                lastLoginTime = _lastLoginTime
            };
            MyUtil.SaveClassToBinary<UserInfo>(MyUtil.mainPath + "/userInfo", userInfo);
            ShowDialogBox(String.Format("欢迎，{0}。目前的vip等级是：{1}。上一次的登录时间为：{2}。登录成功，即将跳转下一场景。", userInfo.name,
            userInfo.vipLevel, userInfo.lastLoginTime), DialogHint.LoginSuccess);
        }
        else
        {
            ShowDialogBox("登录失败，请确认用户名和密码", DialogHint.Default);
        }
    }

    public void FadeLoadingUI()
    {
        StartCoroutine(CR_FadeLoadingUI());
    }

    IEnumerator CR_FadeLoadingUI()
    {
        yield return new WaitForSeconds(1);
        LoadingBox.SetActive(false);
        ShowDialogBox("现在要完成使用角色的选择，点击角色查看详细信息", DialogHint.Default);
    }

    public void ShowLoadingUI()
    {
        LoadingBox.SetActive(true);
        SampleInfo_Panel.SetActive(false);
        MyUtil.FindTransformInChildren(LoadingBox.transform, "ExitBlackScreen").gameObject.GetComponent<UI_BlinkScript>().enabled=true;
        MyUtil.FindTransformInChildren(LoadingBox.transform, "ExitBlackScreen").gameObject.GetComponent<UI_BlinkScript>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
