using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PS_TEST_Enemy : MonoBehaviour
{

    public void OnClick_UserBlock(object sender, EventArgs e)
    {
        if (LOGIN_SGM_Script.Instance.WelcomeStage)
        {
            PS_TEST_PlayerAction.UserActAble = false;
            Camera.main.GetComponent<Animation>().Play("CameraAni_2");
            GameObject.Find("GameTitle").gameObject.GetComponent<Animation>().Play("GameTitle");
            GameObject.Find("GameTitle").gameObject.GetComponent<UI_BlinkScript>().enabled = true;
            GameObject.Find("GameTitle").gameObject.GetComponent<UI_BlinkScript>().FadeModeOn();
            GameObject.Find("PressHint").gameObject.GetComponent<UI_BlinkScript>().FadeModeOn();

            StartCoroutine(CR_OnClick_UserBlock(((MouseLeftClickEventArgs)e).clickedGameObject));
            ((MouseLeftClickEventArgs)e).clickedGameObject.GetComponent<Image>().raycastTarget = false;

            return;
        }
        if (LOGIN_SGM_Script.Instance.LoginStage)
        {
            PS_TEST_PlayerAction.UserActAble = false;
            LOGIN_SGM_Script.Instance.DialogBox.SetActive(false);
            if (LOGIN_SGM_Script.Instance.dialogHint == DialogHint.LoginSuccess)
            {
                LOGIN_SGM_Script.Instance.ShowLoadingUI();
            }
            ((MouseLeftClickEventArgs)e).clickedGameObject.GetComponent<Image>().raycastTarget = false;
            return;
        }
    }
    IEnumerator CR_OnClick_UserBlock(GameObject ub)
    {
        yield return new WaitForSeconds(2);
        LOGIN_SGM_Script.Instance.Login_Panel.SetActive(true);
        LOGIN_SGM_Script.Instance.Welcome_Panel.SetActive(false);
        LOGIN_SGM_Script.Instance.LoginTitle.gameObject.GetComponent<UI_BlinkScript>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        LOGIN_SGM_Script.Instance.LoginTitle.gameObject.GetComponent<UI_BlinkScript>().AscendModeOn();
        LOGIN_SGM_Script.Instance.LoginBackground.gameObject.GetComponent<UI_BlinkScript>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        LOGIN_SGM_Script.Instance.LoginBackground.gameObject.GetComponent<UI_BlinkScript>().AscendModeOn();
        LOGIN_SGM_Script.Instance.isTiming = true;
        LOGIN_SGM_Script.Instance.DialogBox.SetActive(true);
        MVC_Test_View.Instance.ModifyMsg("首先，要完成登录环节。点击<color=red>注册按钮</color>，跳转网页端进行注册。注册成功后，输入信息，进行登录。跳过此环节，可以点击游客登录。");
        PS_TEST_PlayerAction.UserActAble = true;
        LOGIN_SGM_Script.Instance.WelcomeStage = false;
        LOGIN_SGM_Script.Instance.LoginStage = true;

        ub.GetComponent<Image>().raycastTarget = true;
    }

    public void OnClick_RegisterButton(object sender, EventArgs e)
    {
        Application.OpenURL("http://81.68.87.60/app.publish/Register");
        LOGIN_SGM_Script.Instance.Register_Button.GetComponentInChildren<Text>().enabled = false;
        LOGIN_SGM_Script.Instance.Register_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        LOGIN_SGM_Script.Instance.Register_Button.GetComponent<Image>().raycastTarget = false;
        StartCoroutine(CR_OnClick_RegisterButton());
    }
    IEnumerator CR_OnClick_RegisterButton()
    {
        yield return new WaitForSeconds(1);
        LOGIN_SGM_Script.Instance.Register_Button.GetComponentInChildren<Text>().enabled = true;
        LOGIN_SGM_Script.Instance.Register_Button.GetComponentInChildren<SpriteRenderer>().enabled = false;
        LOGIN_SGM_Script.Instance.Register_Button.GetComponent<Image>().raycastTarget = true;
    }

    public void OnClick_LoginButton(object sender, EventArgs e)
    {
        LOGIN_SGM_Script.Instance.Login_Button.GetComponentInChildren<Text>().enabled = false;
        LOGIN_SGM_Script.Instance.Login_Button.GetComponentInChildren<SpriteRenderer>().enabled = true;
        LOGIN_SGM_Script.Instance.Login_Button.GetComponent<Image>().raycastTarget = false;


        LOGIN_SGM_Script.Instance.thread_Login = new Thread(GetDataAndSaveData);
        LOGIN_SGM_Script.Instance.thread_Login.Start();
    }

    void GetDataAndSaveData()
    {
        LOGIN_SGM_Script.Instance.ds = new DataSet();
        LOGIN_SGM_Script.Instance.cmdStr = "SELECT * FROM `unity`.`tbl_user` WHERE `name`='" + LOGIN_SGM_Script.Instance.Name_Text.text + "' AND `password`='" + LOGIN_SGM_Script.Instance.Password_Text.text + "';";
        int result = MyUtil.GetDataFromMySQL(LOGIN_SGM_Script.Instance.cmdStr, LOGIN_SGM_Script.Instance.ds);
        if (result == 1)
        {
            LOGIN_SGM_Script.Instance.dt = LOGIN_SGM_Script.Instance.ds.Tables[0];
            if (LOGIN_SGM_Script.Instance.dt.Rows.Count == 1)
            {
                LOGIN_SGM_Script.Instance.isLoginSuccess = true;
            }
        }
        LOGIN_SGM_Script.Instance.isLoginDone = true;

        LOGIN_SGM_Script.Instance.cmdStr = "UPDATE `unity`.`tbl_user` SET `lastLoginTime` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE(`id` = '1');";
        MyUtil.SaveDataToMySQL(LOGIN_SGM_Script.Instance.cmdStr);

    }

    private void Start()
    {
        LOGIN_SGM_Script.Instance.AddObserver(EventName.MouseLeftClick, OnClick_UserBlock);
        LOGIN_SGM_Script.Instance.AddObserver(EventName.RegisterButton_Click, OnClick_RegisterButton);
        LOGIN_SGM_Script.Instance.AddObserver(EventName.LoginButton_Click, OnClick_LoginButton);
    }

}



