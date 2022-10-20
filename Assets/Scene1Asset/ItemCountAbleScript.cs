using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountAbleScript : MonoBehaviour
{
    Text CountText;
    int count;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.GetComponent<ItemDetailScript>().Id = 1003;
        this.gameObject.GetComponent<ItemDetailScript>().Description = "金钱堆";
        this.gameObject.GetComponent<ItemDetailScript>().Price = 1;
        CountText = this.gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    public void SetCount(int count)
    {
        this.count = count;
        CountText.text = this.count.ToString();
    }

    public int GetCountText()
    {
        return this.count;
    }
    public void CountS(int i)
    {
        this.count -= i;
        CountText.text = this.count.ToString();
        if (count == 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void CountP(int i)
    {
        this.count += i;
        CountText.text = this.count.ToString();
    }
}
