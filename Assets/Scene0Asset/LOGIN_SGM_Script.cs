using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOGIN_SGM_Script : Base_SGM
{
    public static LOGIN_SGM_Script Instance;
    public static GameObject UserBlock;

    public GameObject Welcome_Panel;
    public GameObject Login_Panel;
    public ParticleSystem.MainModule particleSystem;
    public GameObject windZone;
    public GameObject LoginTitle;
    public GameObject LoginBackground;
    public GameObject DialogTMP;
    public GameObject Register_Button;


    public AnimationCurve recycleCurve;
    public AnimationCurve fadeCurve;
    public AnimationCurve ascendCurve;

    public bool WelcomeStage;
    public bool LoginStage;
    public bool ExitStage;

    private void Awake()
    {
        Instance = this;
        instance = this;
        base.Base_Awake();
        Welcome_Panel = GameObject.Find("Welcome_Panel");
        Login_Panel = GameObject.Find("Login_Panel");
        particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>().main;
        windZone = GameObject.Find("WindZone");
        UserBlock = GameObject.Find("UserBlock");
        LoginTitle = GameObject.Find("LoginTitle");
        LoginBackground = GameObject.Find("LoginBackground");
        DialogTMP = GameObject.Find("DialogTMP");

        LoginForm = MyUtil.FindTransformInChildren(Login_Panel.transform, "LoginForm").gameObject;
        Register_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Register_Button").gameObject;
        Login_Button = MyUtil.FindTransformInChildren(Login_Panel.transform, "Login_Button").gameObject;
        Name_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Name_Text").gameObject.GetComponent<Text>();
        Password_Text = MyUtil.FindTransformInChildren(Login_Panel.transform, "Password_Text").gameObject.GetComponent<Text>();
        Login_Panel.SetActive(false);

        DialogBox.SetActive(false);
        LoadingBox.SetActive(false);


        MainCamera.GetComponent<Animation>().Play("CameraAni_1");
        WelcomeStage = true;
    }
    private void Start()
    {
        Base_SGM.RecycleCurve = recycleCurve;
        Base_SGM.FadeCurve = fadeCurve;
        Base_SGM.AscendCurve = ascendCurve;
    }

    public float timer;
    public bool isTiming;
    GameObject LoginForm;

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
        if (isLoginDone)
        {
            ResetLoginButton(isLoginSuccess);
            isLoginDone = false;
        }
    }

    public GameObject Login_Button;
    public Text Name_Text;
    public Text Password_Text;
    public UserInfo userInfo;
    public bool isLoginDone;
    public bool isLoginSuccess;
    public Thread thread_Login;
    public DataSet ds;
    public DataTable dt;
    public string cmdStr;
    public void OnLoginButtonClick()
    {
        //Login_Button.GetComponentInChildren<Text>().enabled = false;
        //Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        //Login_Button.GetComponent<Image>().raycastTarget = false;


        //thread_Login = new Thread(GetDataAndSaveData);
        //thread_Login.Start();

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
            int _coin = 0;
            try
            {
                _coin = int.Parse(dt.Rows[0]["coin"].ToString());
            }
            catch (Exception e)
            {

            }
            int _vipLevel = 0;
            try
            {
                _vipLevel = int.Parse(dt.Rows[0]["vipLevel"].ToString());
            }
            catch (Exception e)
            {

            }
            DateTime _lastLoginTime = DateTime.Parse("1999-01-01 00:00:00");
            try
            {
                _lastLoginTime = DateTime.Parse(dt.Rows[0]["lastLoginTime"].ToString());
            }
            catch (Exception e)
            {

            }
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

    public void ShowLoadingUI()
    {
        Login_Panel.SetActive(false);
        LoadingBox.SetActive(true);
        particleSystem.loop = false;
        //StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

    #region PS_TEXT
    private static Dictionary<string, EventHandler> obs = new Dictionary<string, EventHandler>();

    public void AddObserver(string name, EventHandler h)
    {
        if (obs.ContainsKey(name))
        {
            obs[name] += (h);
        }
        else
        {
            obs.Add(name, h);
        }
    }
    public void RemoveObserver(string name, EventHandler h)
    {
        if (obs.ContainsKey(name))
        {
            obs[name] -= h;
        }
    }

    public void Notify(string name, object sender)
    {
        if (obs.ContainsKey(name))
        {
            obs[name]?.Invoke(sender, EventArgs.Empty);
        }
    }

    public void Notify(string name, object sender, EventArgs e)
    {
        if (obs.ContainsKey(name))
        {
            obs[name]?.Invoke(sender, e);
        }
    }


    #endregion

    #region Event
    private event Action OnExit;
    public void AddListener(Action a)
    {
        OnExit += a;
    }
    public void RemoveListener(Action a)
    {
        OnExit -= a;
    }
    public void TriggerEvent()
    {
        OnExit();
    }
    #endregion
}


