using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void OnValueChanged<V>(V value);

public class MVC_Base_Model<T, D> : SingleTon<T> where T : new() where D : new()
{
    protected virtual string GetLocalDataKey()
    {
        return "";
    }

    protected D LocalData = new D();

    private void GetLocalData()
    {
        if (GetLocalDataKey() == "")
        {
            return;
        }

        //string _localDataStr = PlayerPrefs.GetString(GetLocalDataKey(), "");
        //if (_localDataStr == null || _localDataStr == "")
        //{
        //    return;
        //}

        //LocalData=
    }

    public void Init()
    {
        GetLocalData();
    }

    protected virtual void InitData() { }

    protected void SaveLocalData()
    {
        if (GetLocalDataKey() == "")
        {
            return;
        }

        if (LocalData == null)
        {

            return;
        }
    }

}
