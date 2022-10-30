using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MVC_Test_View : SingleTon<MVC_Test_View>
{

    public TextMeshPro tmp;

    private void UpdateMsg(string str)
    {
        tmp.text = str;
    }

    private void Start()
    {
        tmp = LOGIN_SGM_Script.Instance.DialogTMP.GetComponent<TextMeshPro>();

        MVC_Test_Model.Instance.OnMessageChanged += UpdateMsg;
    }

    public MVC_Test_View()
    {
        Start();
    }

    public void ModifyMsg(string str)
    {
        MVC_Test_Controller.Instance.ChangeMsg(str);
    }
}



