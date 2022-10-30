using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MVC_Test_Controller : SingleTon<MVC_Test_Controller>
{

    private void Start()
    {
        MVC_Test_Model.Instance.Init();
    }

    public MVC_Test_Controller()
    {
        Start();
    }

    public void ChangeMsg(string str)
    {
        MVC_Test_Model.Instance.MsgIndex = str;
    }
}



