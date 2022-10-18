using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BS_SGM_Script : SingleTon<BS_SGM_Script>
{
    public GameData gameData;


    GameObject PlayerPosition;
    Animator PlayerAnimator;

    GameObject RobotPosition;
    Animator RobotAnimator;


    GameObject InfoBar;
    Image PlayerHPBar;
    Image RobotHPBar;
    Text Timer;

    GameObject tmpGO;
    bool ifTimerOn;

    private void Awake()
    {
        base.InitInstance(this);
        gameData = AssetDatabase.LoadAssetAtPath<GameData>("Assets/GameData/gameData.asset");
        InfoBar = GameObject.Find("InfoBar");

        PlayerHPBar = MyUtil.FindTransformInChildren(GameObject.Find("PlayerHPBar").transform, "HPValue").GetComponent<Image>();
        RobotHPBar = MyUtil.FindTransformInChildren(GameObject.Find("verseHPBar").transform, "HPValue").GetComponent<Image>();
        Timer = GameObject.Find("Timer").GetComponentInChildren<Text>();

        BuildPlayer();
        BuildRobot();

        InfoBar.SetActive(false);
        ifTimerOn = true;

    }

    private void Update()
    {
        if (ifTimerOn)
        {
            Timer.text = Time.time.ToString("00");
        }
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
        if (gameData.WeaponType.Contains("Hand"))
        {
            tmpGO.AddComponent<AniControlHandScript>();
            PlayerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHuman");
            MyUtil.FindTransformInChildren(PlayerPosition.transform, "Sword").gameObject.SetActive(false);
        }
        if (gameData.WeaponType.Contains("Sword"))
        {
            tmpGO.AddComponent<AniControlSwordScript>();
            PlayerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanSword");
        }
        PlayerPosition.AddComponent<PlayerActionControlScript>();
        PlayerPosition.AddComponent<GoundDetectScript>();

    }

    public void EndDuel()
    {
        InfoBar.SetActive(true);
        ifTimerOn = false;

        if (PlayerHPBar.fillAmount > 0)
        {
            InfoBar.GetComponentInChildren<Text>().text = "YOU WIN";
        }
        else
        {
            InfoBar.GetComponentInChildren<Text>().text = "YOU LOSE";
        }
        PlayerPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;
        RobotPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;
    }
}
