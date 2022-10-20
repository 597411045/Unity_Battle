﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
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
        if (this.gameObject.name.Contains("Bag_Button"))
        {
            PB_SGM_Script.instance.Equip_Switch();
            return;
        }
        if (this.gameObject.name.Contains("Store_Button"))
        {
            PB_SGM_Script.instance.Store_Switch();
            return;
        }
        if (this.gameObject.name.Contains("Start_Button"))
        {
            string path = Application.dataPath + "/Resources/gameData";
            GameData gameData = new GameData();

            //gameData.CharacterName = PB_SGM_Script.instance.CharacterName;
            //gameData.WeaponType = PB_SGM_Script.instance.WeaponType;
            if (gameData.WeaponType.Contains("Hand"))
            {
                gameData.HandDamageMultipler = 1;
                gameData.FootDamageMultipler = 2;
            }
            if (gameData.WeaponType.Contains("Sword"))
            {
                gameData.HandDamageMultipler = 5;
                gameData.FootDamageMultipler = 10;
            }

            FileStream fs = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, gameData);
            fs.Close();

            PB_SGM_Script.instance.ChangeScene();
        }
        if (this.gameObject.name.Contains("CancelBuy_Button"))
        {
            PB_SGM_Script.instance.MessageBox_Buy.active = false;
        }
        if (this.gameObject.name.Contains("Buy_Button"))
        {
            int playerMoney;
            Transform[] tmpTf = PB_SGM_Script.instance.Bag_Box.GetComponentsInChildren<Transform>();
            for (int i = 0; i < tmpTf.Length; i++)
            {
                if (tmpTf[i].name.Contains("Item_Coin"))
                {
                    playerMoney = tmpTf[i].gameObject.GetComponent<ItemCountAbleScript>().GetCountText();
                    break;
                }
            }

            int TotalSellMoney;
            TotalSellMoney = int.Parse(PB_SGM_Script.instance.MessageBox_Buy.GetComponent<MessageBoxScript>().Content_TotalPrice.text);


        }
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