using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MVC_Test_Model : MVC_Base_Model<MVC_Test_Model, Object>
{

    public OnValueChanged<string> OnMessageChanged;

    private string msg = "";

    public string MsgIndex
    {
        get
        {
            return msg;
        }
        set
        {
            msg = value;
            if (OnMessageChanged != null)
            {
                OnMessageChanged(msg);
            }
        }
    }
}
