using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SB_SGM_Script : SingleTon<SB_SGM_Script>
{
    public GameObject Character;
    GameObject CharacterBox;

    public GameObject Bag;
    public GameObject Equipment;

    public Image Continue_Button;
    public Image Store_Button;
    public Image Bag_Button;


    public string CharacterName;
    public string WeaponType;

    private void Awake()
    {
        base.InitInstance(this);
    }

    void Start()
    {
        CharacterBox = GameObject.Find("Box");
        Bag = GameObject.Find("Bag"); ;
        Equipment = GameObject.Find("Equipment");

        Continue_Button = GameObject.Find("Continue_Button").GetComponent<Image>();
        Store_Button = GameObject.Find("Store_Button").GetComponent<Image>();
        Bag_Button = GameObject.Find("Bag_Button").GetComponent<Image>();

        C1_On();
        Bag_Off();
    }

    public void C1_On()
    {
        //if (Character != null) Destroy(Character);
        //C1_Button.color = Color.blue;
        //C2_Button.color = Color.white;
        //Character = Instantiate(Resources.Load<GameObject>("Prefabs/C1"), CharacterBox.transform.position, Quaternion.Euler(0, 180, 0), CharacterBox.transform);
        //Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C1_Show");

        //MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
        //CharacterName = "C1";
        //WeaponType = "Hand";
    }

    public void C2_On()
    {
        //if (Character != null) Destroy(Character);
        //C1_Button.color = Color.white;
        //C2_Button.color = Color.blue;
        //Character = Instantiate(Resources.Load<GameObject>("Prefabs/C2"), CharacterBox.transform.position, Quaternion.Euler(0, 180, 0), CharacterBox.transform);
        //Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C2_Show");
        //MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
        //CharacterName = "C2";
    }

    public void Bag_On()
    {
        Bag.SetActive(true);
        Equipment.SetActive(true);
        Bag_Button.color = Color.blue;
    }

    public void Bag_Off()
    {
        Bag.SetActive(false);
        Equipment.SetActive(false);
        Bag_Button.color = Color.white;
    }

    public void Bag_Switch()
    {
        Bag.SetActive(!Bag.activeSelf);
        Equipment.SetActive(!Equipment.activeSelf);
        Bag_Button.color = Bag.activeSelf ? Color.blue : Color.white;
    }

    public void ChangeScene()
    {
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("BattleState");
    }
}
