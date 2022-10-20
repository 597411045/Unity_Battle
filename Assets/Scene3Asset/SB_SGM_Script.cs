using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SB_SGM_Script : SingleTon<SB_SGM_Script>
{
    public CurLevelData curLevelData;

    public Image Retry_Button;
    public Image Continue_Button;

    Text Content_CD_Count;
    Text Content_ID_Count;
    Text Content_BK_Count;
    Text Content_TotalScore;
    Text Content_LV_Score;
    Text Title_Level;

    private void Awake()
    {
        curLevelData = new CurLevelData();

        instance = this;
        string path = Application.dataPath + "/Resources/curLevelData";
        curLevelData = MyUtil.GetClassFromBinary<CurLevelData>(path);
        
    }

    void Start()
    {
        Continue_Button = GameObject.Find("Continue_Button").GetComponent<Image>();
        Retry_Button = GameObject.Find("Retry_Button").GetComponent<Image>();
        Content_CD_Count = GameObject.Find("Content_CD_Count").GetComponent<Text>();
        Content_ID_Count = GameObject.Find("Content_ID_Count").GetComponent<Text>();
        Content_BK_Count = GameObject.Find("Content_BK_Count").GetComponent<Text>();
        Content_TotalScore = GameObject.Find("Content_TotalScore").GetComponent<Text>();
        Content_LV_Score = GameObject.Find("Content_LV_Score").GetComponent<Text>();
        Title_Level = GameObject.Find("Title_Level").GetComponentInChildren<Text>();

        int LV_Score = ((curLevelData.isPassed ? 1 : 0) * curLevelData.curDifficult * 1000 + curLevelData.curLevel * 1000);
        Content_LV_Score.text = LV_Score.ToString();
        Content_CD_Count.text = curLevelData.CD_Count.ToString();
        Content_ID_Count.text = curLevelData.ID_Count.ToString();
        Content_BK_Count.text = curLevelData.BK_Count.ToString();
        Content_TotalScore.text = (curLevelData.CD_Count * 2 + curLevelData.ID_Count * 5 + curLevelData.BK_Count + LV_Score).ToString();
        Title_Level.text = "第" + curLevelData.curLevel.ToString() + "关，难度：" + curLevelData.curDifficult.ToString();

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
