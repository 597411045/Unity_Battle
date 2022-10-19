using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOGIN_SGM_Script : SingleTon<LOGIN_SGM_Script>
{

    private void Awake()
    {
        base.InitInstance(this);
    }

    public void ChangeScene()
    {
        StartCoroutine(CR_ChangeScene());
    }

    IEnumerator CR_ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
