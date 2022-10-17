using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleStageScript : SingleTon<BattleStageScript>
{
    public GameData gameData;


    GameObject PlayerPosition;
    Animator PlayerAnimator;

    GameObject RobotPosition;
    Animator RobotAnimator;

    GameObject tmpGO;

    private void Awake()
    {
        base.InitInstance(this);
        gameData = AssetDatabase.LoadAssetAtPath<GameData>("Assets/GameData/gameData.asset");

        BuildPlayer();

        BuildRobot();
    }

    private void BuildRobot()
    {
        RobotPosition = GameObject.Find("RobotBox");
        tmpGO = Instantiate(Resources.Load<GameObject>("Prefabs/C2"), RobotPosition.transform.position, Quaternion.Euler(0, 270, 0), RobotPosition.transform);
        tmpGO.name = "Enemy";
        RobotAnimator = tmpGO.GetComponent<Animator>();
        //if (gameData.WeaponType == "Hand")
        {
            tmpGO.AddComponent<AniControlHandScript>();
            RobotAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanEnemy");
            MyUtil.FindTransformInChildren(RobotPosition.transform, "Sword").gameObject.SetActive(false);

        }
        //if (gameData.WeaponType == "Sword")
        //{
        //    tmpGO.AddComponent<AniControlHandScript>();
        //    PlayerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanSword");
        //}
        RobotPosition.AddComponent<AIActionControlScript>();
        RobotPosition.AddComponent<GoundDetectScript>();


    }

    private void BuildPlayer()
    {
        PlayerPosition = GameObject.Find("CharacterBox");
        tmpGO = Instantiate(Resources.Load<GameObject>("Prefabs/" + gameData.CharacterName), PlayerPosition.transform.position, Quaternion.Euler(0, 90, 0), PlayerPosition.transform);
        tmpGO.name = gameData.CharacterName;
        PlayerAnimator = tmpGO.GetComponent<Animator>();
        if (gameData.WeaponType == "Hand")
        {
            tmpGO.AddComponent<AniControlHandScript>();
            PlayerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHuman");
            MyUtil.FindTransformInChildren(PlayerPosition.transform, "Sword").gameObject.SetActive(false);
        }
        if (gameData.WeaponType == "Sword")
        {
            tmpGO.AddComponent<AniControlSwordScript>();
            PlayerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanSword");
        }
        PlayerPosition.AddComponent<PlayerActionControlScript>();
        PlayerPosition.AddComponent<GoundDetectScript>();

    }


}
