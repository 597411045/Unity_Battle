using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PB_SGM_Script : SingleTon<PB_SGM_Script>
{
    public GameObject Character;
    GameObject Character_Box;
    GameObject Bag_Box;
    GameObject Store_Box;
    GameObject Equip_Box;

    public Image C1_Button;
    public Image C2_Button;
    public Image Bag_Button;
    public Image Store_Button;


    private void Awake()
    {
        base.InitInstance(this);
    }

    void Start()
    {
        Character_Box = GameObject.Find("Box");
        Bag_Box = GameObject.Find("Bag_Box");
        Store_Box = GameObject.Find("Store_Box");
        Equip_Box = GameObject.Find("Equip_Box");

        C1_Button = GameObject.Find("C1_Button").GetComponent<Image>();
        C2_Button = GameObject.Find("C2_Button").GetComponent<Image>();
        Bag_Button = GameObject.Find("Bag_Button").GetComponent<Image>();
        Store_Button = GameObject.Find("Store_Button").GetComponent<Image>();

        C1_On();
        BuildStoreItem();
        All_Off();

    }

    public void C1_On()
    {
        if (Character != null) Destroy(Character);
        C1_Button.color = Color.blue;
        C2_Button.color = Color.white;
        Character = Instantiate(Resources.Load<GameObject>("Prefabs/C1"), Character_Box.transform.position, Quaternion.Euler(0, 180, 0), Character_Box.transform);
        Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C1_Show");

        MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
    }

    public void C2_On()
    {
        if (Character != null) Destroy(Character);
        C1_Button.color = Color.white;
        C2_Button.color = Color.blue;
        Character = Instantiate(Resources.Load<GameObject>("Prefabs/C2"), Character_Box.transform.position, Quaternion.Euler(0, 180, 0), Character_Box.transform);
        Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C2_Show");
        MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
    }

    public void Equip_Switch()
    {
        if (Equip_Box.activeSelf == true)
        {
            All_Off();
        }
        else
        {
            All_Off();
            Bag_Box.SetActive(true);
            Equip_Box.SetActive(true);
            Bag_Button.color = Color.blue;
        }

    }

    public void Store_Switch()
    {
        if (Store_Box.activeSelf == true)
        {
            All_Off();
        }
        else
        {
            All_Off();
            Store_Box.SetActive(true);
            Bag_Box.SetActive(true);
            Store_Button.color = Color.blue;
        }
    }

    public void All_Off()
    {
        Bag_Box.SetActive(false);
        Equip_Box.SetActive(false);
        Store_Box.SetActive(false);
        Bag_Button.color = Color.white;
        Store_Button.color = Color.white;
    }

    void BuildStoreItem()
    {
        int count = 3;
        Transform[] tmpTf = Store_Box.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tmpTf.Length; i++)
        {
            if (tmpTf[i].name == "Slot" && count == 3)
            {
                Instantiate(Resources.Load("Prefabs/Weapon_Hand"), tmpTf[i]);
                count--;
                continue;
            }
            if (tmpTf[i].name == "Slot" && count == 2)
            {
                Instantiate(Resources.Load("Prefabs/Weapon_Sword"), tmpTf[i]);
                count--;
                continue;
            }
            if (tmpTf[i].name == "Slot" && count == 1)
            {
                GameObject tmpGO = Instantiate(Resources.Load("Prefabs/Item_Coin"), tmpTf[i]) as GameObject;
                tmpGO.GetComponent<ItemCountAbleScript>().SetCount(50);
                count--;
                continue;
            }
        }
    }

    public void ChangeScene()
    {
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
