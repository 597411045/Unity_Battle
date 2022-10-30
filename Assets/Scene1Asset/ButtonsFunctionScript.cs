using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunctionScript : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name.Contains("C1"))
        {
            Obsolete_PB_SGM_Script.instance.C1_On();
            return;
        }
        if (this.gameObject.name.Contains("C2"))
        {
            Obsolete_PB_SGM_Script.instance.C2_On();
            return;
        }
        if (this.gameObject.name.Equals("Bag_Button"))
        {
            Obsolete_PB_SGM_Script.instance.Equip_Switch();
            return;
        }
        if (this.gameObject.name.Equals("Store_Button"))
        {
            Obsolete_PB_SGM_Script.instance.Store_Switch();
            return;
        }
        if (this.gameObject.name.Equals("Start_Button"))
        {
            //if (Obsolete_PB_SGM_Script.instance.gameData.CharacterName != "C1" && Obsolete_PB_SGM_Script.instance.gameData.CharacterName != "C2") return;
            //if (Obsolete_PB_SGM_Script.instance.gameData.WeaponType != "Sword" && Obsolete_PB_SGM_Script.instance.gameData.WeaponType != "Hand") return;

            //string path = Application.dataPath + "/Resources/gameData";
            //MyUtil.SaveClassToBinary<GameData>(path, Obsolete_PB_SGM_Script.instance.gameData);

            //Obsolete_PB_SGM_Script.instance.ChangeScene();
            PB_SGM_Script._Instance.ShowLoadingUI();

        }
        if (this.gameObject.name.Equals("CancelBuy_Button"))
        {
            Obsolete_PB_SGM_Script.instance.MessageBox_Buy.active = false;
        }
        if (this.gameObject.name.Equals("Buy_Button"))
        {


            int TotalSellMoney;
            TotalSellMoney = int.Parse(Obsolete_PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().Content_TotalPrice.text);

            int playerMoney = 0;
            Transform[] tmpTf = Obsolete_PB_SGM_Script.instance.Bag_Box.GetComponentsInChildren<Transform>();
            for (int i = 0; i < tmpTf.Length; i++)
            {
                if (tmpTf[i].name.Contains("Item_Coin"))
                {
                    playerMoney = tmpTf[i].gameObject.GetComponent<ItemCountAbleScript>().GetCountText();

                    if (playerMoney >= TotalSellMoney)
                    {
                        Obsolete_PB_SGM_Script.instance.Hint_Buy.active = true;
                        Obsolete_PB_SGM_Script.instance.Hint_Buy.GetComponentInChildren<Text>().text = "购买成功";

                        Transform[] tmpTf2 = Obsolete_PB_SGM_Script.instance.Bag_Box.GetComponentsInChildren<Transform>();
                        for (int j = 0; j < tmpTf.Length; j++)
                        {
                            if (tmpTf[j].name == "Slot" && tmpTf[j].childCount == 0)
                            {
                                Obsolete_PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().targetGO.transform.SetParent(tmpTf[j]);
                                Obsolete_PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().targetGO.transform.localPosition = new Vector3(0, 0, 0);

                                tmpTf[i].gameObject.GetComponent<ItemCountAbleScript>().CountS(TotalSellMoney);

                                StartCoroutine(CR_EndBuyButton());
                                return;
                            }
                        }
                    }
                }
            }

            Obsolete_PB_SGM_Script.instance.Hint_Buy.active = true;
            Obsolete_PB_SGM_Script.instance.Hint_Buy.GetComponentInChildren<Text>().text = "购买失败";
            StartCoroutine(CR_EndBuyButton());

        }
        if (this.gameObject.name.Equals("Retry_Button"))
        {
            SceneManager.LoadScene(2);
        }
        if (this.gameObject.name.Equals("Continue_Button"))
        {
            if (SB_SGM_Script.instance.curLevelData.curLevel == 1)
            {
                SB_SGM_Script.instance.curLevelData.curLevel = 2;
            }
            else
            {
                SB_SGM_Script.instance.curLevelData.curDifficult++;
            }
            string path = Application.dataPath + "/Resources/curLevelData";
            SB_SGM_Script.instance.curLevelData.BK_Count = 0;
            SB_SGM_Script.instance.curLevelData.CD_Count = 0;
            SB_SGM_Script.instance.curLevelData.ID_Count = 0;
            MyUtil.SaveClassToBinary<CurLevelData>(path, SB_SGM_Script.instance.curLevelData);
            SceneManager.LoadScene(2);
        }
        if (this.gameObject.name.Equals("Register_Button"))
        {
            LOGIN_SGM_Script.Instance.Notify(EventName.RegisterButton_Click, this);
        }
        if (this.gameObject.name.Equals("Login_Button"))
        {
            LOGIN_SGM_Script.Instance.Notify(EventName.LoginButton_Click, this);
            
            //LOGIN_SGM_Script.Instance.OnLoginButtonClick();
        }
        if (this.gameObject.name.Equals("Guest_Button"))
        {

        }
        if (this.gameObject.name.Equals("SystemButton_1"))
        {
            //LOGIN_SGM_Script._Instance.ShowLoadingUI();
        }
        if (this.gameObject.name.Equals("ReChoose_Button"))
        {
            PB_SGM_Script._Instance.BackToChooseStage();
        }
        if (this.gameObject.name.Equals("SystemButton_3"))
        {

        }
    }

    IEnumerator CR_EndBuyButton()
    {
        yield return new WaitForSeconds(1);
        Obsolete_PB_SGM_Script.instance.Hint_Buy.active = false;
        Obsolete_PB_SGM_Script.instance.MessageBox_Buy.active = false;
    }
}

//{

//    ScriptableObject so = ScriptableObject.CreateInstance<GameData>();
//    GameData gameData = so as GameData;

//    gameData.CharacterName = CC_SGM_Script.instance.CharacterName;
//    gameData.WeaponType = CC_SGM_Script.instance.WeaponType;
//    if (gameData.WeaponType.Contains("Hand"))
//    {
//        gameData.HandDamageMultipler = 1;
//        gameData.FootDamageMultipler = 2;
//    }
//    if (gameData.WeaponType.Contains("Sword"))
//    {
//        gameData.HandDamageMultipler = 5;
//        gameData.FootDamageMultipler = 10;
//    }
//    string path = Application.dataPath + "/GameData";
//    if (!Directory.Exists(path))
//    {
//        Directory.CreateDirectory(path);
//    }
//    path = "Assets/GameData/gameData.asset";
//#if UNITY_EDITOR
//    AssetDatabase.CreateAsset(so, path);
//#endif
//    CC_SGM_Script.instance.ChangeScene();
//}