using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonScript : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        ScriptableObject so = ScriptableObject.CreateInstance<GameData>();
        GameData gameData = so as GameData;
        if (ChooseCharacterScript.instance.CharacterName == "C1")
        {
            gameData.CharacterName = "C1";
            gameData.FootDamageMultipler = 4;
            gameData.HandDamageMultipler = 2;
        }
        if (ChooseCharacterScript.instance.CharacterName == "C2")
        {
            gameData.CharacterName = "C2";
            gameData.FootDamageMultipler = 2;
            gameData.HandDamageMultipler = 1;
        }
        string path = Application.dataPath + "/GameData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = "Assets/GameData/gameData.asset";
        AssetDatabase.CreateAsset(so, path);

        ChooseCharacterScript.instance.ChangeScene();
    }
}
