using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;
using System.Data;
using System.Linq;

public class Obsolete_LoginButtonScript : MonoBehaviour, IPointerClickHandler
{
    string cmdStr;

    DataSet ds;
    DataTable dt;

    Text nameField;
    Text pwdField;
    Text resultField;

    private void Start()
    {
        nameField = GameObject.Find("nameField").GetComponent<Text>();
        pwdField = GameObject.Find("pwdField").GetComponent<Text>();
        resultField = GameObject.Find("resultField").GetComponent<Text>();
        ds = new DataSet("unity");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = Color.red;

        cmdStr = "SELECT * FROM `unity`.`tbl_user` WHERE `name`='" + nameField.text + "' AND `password`='" + pwdField.text + "';";
        int result = MyUtil.GetDataFromMySQL(cmdStr, ds);
        if (result == 1)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count == 1)
            {
                resultField.text = "登录成功，即将跳转";
                this.gameObject.GetComponent<Image>().raycastTarget = false;
                this.gameObject.GetComponent<Image>().color = Color.green;
                LOGIN_SGM_Script._Instance.ShowLoadingUI();

            }
            else
            {
                resultField.text = "登录失败，请联系管理员";
                this.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            resultField.text = "登录失败，请联系管理员";
            this.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}

