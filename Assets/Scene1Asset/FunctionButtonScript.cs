using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FunctionButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name.Contains("C1"))
        {
            CC_SGM_Script.instance.C1_On();
            return;
        }
        if (this.gameObject.name.Contains("C2"))
        {
            CC_SGM_Script.instance.C2_On();
            return;
        }
        if (this.gameObject.name.Contains("Bag_Button"))
        {
            CC_SGM_Script.instance.Bag_Switch();
            return;
        }
        if (this.gameObject.name.Contains("Start_Button"))
        {
            string path = "D:\\1102\\Github\\GameData\\gameData";
            GameData gameData = new GameData();

            gameData.CharacterName = CC_SGM_Script.instance.CharacterName;
            gameData.WeaponType = CC_SGM_Script.instance.WeaponType;
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

            CC_SGM_Script.instance.ChangeScene();
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