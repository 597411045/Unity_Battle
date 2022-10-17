using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterScript : SingleTon<ChooseCharacterScript>
{
    public GameObject C1;
    public GameObject C2;
    public Image C1_Button;
    public Image C2_Button;
    public string CharacterName;

    private void Awake()
    {
        base.InitInstance(this);
    }

    void Start()
    {
        C1 = GameObject.Find("C1");
        C2 = GameObject.Find("C2");
        C2.SetActive(false);

        C1_Button = GameObject.Find("C1_Button").GetComponent<Image>();
        C2_Button = GameObject.Find("C2_Button").GetComponent<Image>();
        C1_On();
    }

    public void C1_On()
    {
        C1_Button.color = Color.blue;
        C2_Button.color = Color.white;
        C1.SetActive(true);
        C2.SetActive(false);
        CharacterName = "C1";
    }

    public void C2_On()
    {
        C1_Button.color = Color.white;
        C2_Button.color = Color.blue;
        C1.SetActive(false);
        C2.SetActive(true);
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
