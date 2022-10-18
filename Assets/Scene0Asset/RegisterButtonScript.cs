using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;

public class RegisterButtonScript : MonoBehaviour, IPointerClickHandler
{
    string connStr = "Server=81.68.87.60;Port=3307;Database=unity;Uid=sa;Pwd=P@ss1234;Charset=utf8";
    MySqlConnection conn;
    MySqlCommand cmd;
    string cmdStr;
    int result;

    Text nameField;
    Text pwdField;
    Text resultField;

    private void Start()
    {
        nameField = GameObject.Find("nameField").GetComponent<Text>();
        pwdField = GameObject.Find("pwdField").GetComponent<Text>();
        resultField = GameObject.Find("resultField").GetComponent<Text>();
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
                cmdStr = "INSERT INTO `unity`.`tbl_user` (`name`, `password`) VALUES ('" + nameField.text + "', '" + pwdField.text + "');";
                cmd = new MySqlCommand(cmdStr, conn);
                result = cmd.ExecuteNonQuery();

                if (result == 1)
                {
                    resultField.text = "注册成功，即将自动登录";
                    this.gameObject.GetComponent<Image>().raycastTarget = false;
                    this.gameObject.GetComponent<Image>().color = Color.green;
                    LOGIN_SGM_Script.instance.ChangeScene();
                }
                else
                {
                    resultField.text = "注册失败，请联系管理员";
                    this.gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
        catch (Exception e)
        {
            resultField.text = "注册异常失败，请联系管理员";
            this.gameObject.GetComponent<Image>().color = Color.white;
        }
        finally
        {
            conn.Close();
        }
    }
}
