using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;
using System.Data;
using System.Linq;

public class LoginButtonScript : MonoBehaviour, IPointerClickHandler
{
    string connStr = "Server=81.68.87.60;Port=3307;Database=unity;Uid=sa;Pwd=P@ss1234;Charset=utf8";
    MySqlConnection conn;
    MySqlDataAdapter adapter;
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
        if (conn == null)
        {
            conn = new MySqlConnection(connStr);
        }
        conn.Open();
        try
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                cmdStr = "SELECT * FROM `unity`.`tbl_user` WHERE `name`='" + nameField.text + "' AND `password`='" + pwdField.text + "';";
                adapter = new MySqlDataAdapter(cmdStr, conn);
                adapter.Fill(ds, "tbl_user");
                dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    resultField.text = "登录成功，即将跳转";
                    this.gameObject.GetComponent<Image>().raycastTarget = false;
                    this.gameObject.GetComponent<Image>().color = Color.green;
                    LoginSceneScript.instance.ChangeScene();

                }
                else
                {
                    resultField.text = "登录失败，请联系管理员";
                    this.gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
        catch (Exception e)
        {
            resultField.text = "登录异常失败，请联系管理员";
            this.gameObject.GetComponent<Image>().color = Color.white;
        }
        finally
        {
            conn.Close();
        }
    }
}
