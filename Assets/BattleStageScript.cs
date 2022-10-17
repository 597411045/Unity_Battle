using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleStageScript : SingleTon<BattleStageScript>
{
    private void Awake()
    {
        base.InitInstance(this);
        gameData = AssetDatabase.LoadAssetAtPath<GameData>("Assets/GameData/gameData.asset");

        if (gameData.CharacterName == "C1")
        {
            GameObject.Find("C2").SetActive(false);
        }
        if (gameData.CharacterName == "C2")
        {
            GameObject.Find("C1").SetActive(false);
        }
    }

    public GameData gameData;

    private void Start()
    {
    }
}
