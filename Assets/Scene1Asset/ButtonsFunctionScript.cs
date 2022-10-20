using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
            PB_SGM_Script.instance.C1_On();
            return;
        }
        if (this.gameObject.name.Contains("C2"))
        {
            PB_SGM_Script.instance.C2_On();
            return;
        }
        if (this.gameObject.name.Equals("Bag_Button"))
        {
            PB_SGM_Script.instance.Equip_Switch();
            return;
        }
        if (this.gameObject.name.Equals("Store_Button"))
        {
            PB_SGM_Script.instance.Store_Switch();
            return;
        }
        if (this.gameObject.name.Equals("Start_Button"))
        {


            if (PB_SGM_Script.instance.gameData.CharacterName != "C1" && PB_SGM_Script.instance.gameData.CharacterName != "C2") return;
            if (PB_SGM_Script.instance.gameData.WeaponType != "Sword" && PB_SGM_Script.instance.gameData.WeaponType != "Hand") return;

            string path = Application.dataPath + "/Resources/gameData";
            MyUtil.SaveClassToBinary<GameData>(path, PB_SGM_Script.instance.gameData);

            PB_SGM_Script.instance.ChangeScene();
        }
        if (this.gameObject.name.Equals("CancelBuy_Button"))
        {
            PB_SGM_Script.instance.MessageBox_Buy.active = false;
        }
        if (this.gameObject.name.Equals("Buy_Button"))
        {


            int TotalSellMoney;
            TotalSellMoney = int.Parse(PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().Content_TotalPrice.text);

            int playerMoney = 0;
            Transform[] tmpTf = PB_SGM_Script.instance.Bag_Box.GetComponentsInChildren<Transform>();
            for (int i = 0; i < tmpTf.Length; i++)
            {
                if (tmpTf[i].name.Contains("Item_Coin"))
                {
                    playerMoney = tmpTf[i].gameObject.GetComponent<ItemCountAbleScript>().GetCountText();

                    if (playerMoney >= TotalSellMoney)
                    {
                        PB_SGM_Script.instance.Hint_Buy.active = true;
                        PB_SGM_Script.instance.Hint_Buy.GetComponentInChildren<Text>().text = "购买成功";

                        Transform[] tmpTf2 = PB_SGM_Script.instance.Bag_Box.GetComponentsInChildren<Transform>();
                        for (int j = 0; j < tmpTf.Length; j++)
                        {
                            if (tmpTf[j].name == "Slot" && tmpTf[j].GetChildCount() == 0)
                            {
                                PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().targetGO.transform.SetParent(tmpTf[j]);
                                PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().targetGO.transform.localPosition = new Vector3(0, 0, 0);

                                tmpTf[i].gameObject.GetComponent<ItemCountAbleScript>().CountS(TotalSellMoney);

                                StartCoroutine(CR_EndBuyButton());
                                return;
                            }
                        }
                    }
                }
            }

            PB_SGM_Script.instance.Hint_Buy.active = true;
            PB_SGM_Script.instance.Hint_Buy.GetComponentInChildren<Text>().text = "购买失败";
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
    }


    IEnumerator CR_EndBuyButton()
    {
        yield return new WaitForSeconds(1);
        PB_SGM_Script.instance.Hint_Buy.active = false;
        PB_SGM_Script.instance.MessageBox_Buy.active = false;
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