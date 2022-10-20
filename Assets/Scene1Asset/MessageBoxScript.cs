using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxScript : MonoBehaviour
{
    GameObject targetGO;
    Text Content_Id;
    Text Content_Des;
    Text Content_Price;

    Text Content_Name;
    Text Content_Count;
    public Text Content_TotalPrice;

    // Start is called before the first frame update
    void Awake()
    {
        if (this.gameObject.name.Contains("Buy"))
        {
            Content_Name = MyUtil.FindTransformInChildren(this.transform, "Content_Name").GetComponentInChildren<Text>(); ;
            Content_Count = MyUtil.FindTransformInChildren(this.transform, "Content_Count").GetComponentInChildren<Text>(); ;
            Content_TotalPrice = MyUtil.FindTransformInChildren(this.transform, "Content_TotalPrice").GetComponentInChildren<Text>(); ;
        }
        else
        {
            Content_Des = MyUtil.FindTransformInChildren(this.transform, "Content_Des").GetComponentInChildren<Text>(); ;
        }
        Content_Id = MyUtil.FindTransformInChildren(this.transform, "Content_Id").GetComponentInChildren<Text>();
        Content_Price = MyUtil.FindTransformInChildren(this.transform, "Content_Price").GetComponentInChildren<Text>(); ;
    }

    public void SetDesText(GameObject go, int id, string des, int price)
    {
        Content_Id.text = id.ToString();
        Content_Des.text = des.ToString();
        Content_Price.text = price.ToString();
        targetGO = go;
    }

    public void SetBuyText(GameObject go, int id, string name, int price, int count)
    {
        Content_Id.text = id.ToString();
        Content_Name.text = name.ToString();
        Content_Price.text = price.ToString();
        Content_Count.text = count.ToString();
        Content_TotalPrice.text = (price * count).ToString();
        targetGO = go;
    }
}
