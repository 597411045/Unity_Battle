using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterScript : SingleTon<ChooseCharacterScript>
{
    public GameObject Character;
    GameObject CharacterBox;

    public Image C1_Button;
    public Image C2_Button;
    public string CharacterName;

    private void Awake()
    {
        base.InitInstance(this);
    }

    void Start()
    {
        CharacterBox = GameObject.Find("Box");
        C1_Button = GameObject.Find("C1_Button").GetComponent<Image>();
        C2_Button = GameObject.Find("C2_Button").GetComponent<Image>();
        C1_On();
    }

    public void C1_On()
    {
        if (Character != null) Destroy(Character);
        C1_Button.color = Color.blue;
        C2_Button.color = Color.white;
        Character = Instantiate(Resources.Load<GameObject>("Prefabs/C1"), CharacterBox.transform.position, Quaternion.Euler(0, 180, 0), CharacterBox.transform);
        Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C1_Show");
        
        MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
        CharacterName = "C1";
    }

    public void C2_On()
    {
        if (Character != null) Destroy(Character);
        C1_Button.color = Color.white;
        C2_Button.color = Color.blue;
        Character = Instantiate(Resources.Load<GameObject>("Prefabs/C2"), CharacterBox.transform.position, Quaternion.Euler(0, 180, 0), CharacterBox.transform);
        Character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/C2_Show");
        MyUtil.FindTransformInChildren(Character.transform, "Sword").gameObject.SetActive(false);
        CharacterName = "C2";
    }

    public void ChangeScene()
    {
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
