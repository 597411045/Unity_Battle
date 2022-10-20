using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BS_SGM_Script : SingleTon<BS_SGM_Script>
{
    public GameData gameData;
    public CurLevelData curLevelData;

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

    CinemachineTargetGroup cinemachineTargetGroup;

    private void Awake()
    {
        instance = this;

        string pathGD = Application.dataPath + "/Resources/gameData";
        gameData = MyUtil.GetClassFromBinary<GameData>(pathGD);

        string pathLD = Application.dataPath + "/Resources/curLevelData";
        if (!File.Exists(pathLD))
        {
            curLevelData = new CurLevelData();
            curLevelData.curLevel = 1;
            curLevelData.curDifficult = 1;
        }
        else
        {
            curLevelData = MyUtil.GetClassFromBinary<CurLevelData>(pathLD);
        }


        cinemachineTargetGroup = GameObject.Find("TargetGroup1").GetComponent<CinemachineTargetGroup>();

        PlayerHPBar = MyUtil.FindTransformInChildren(GameObject.Find("PlayerHPBar").transform, "HPValue").GetComponent<Image>();
        RobotHPBar = MyUtil.FindTransformInChildren(GameObject.Find("verseHPBar").transform, "HPValue").GetComponent<Image>();
        Timer = GameObject.Find("Timer").GetComponentInChildren<Text>();

        BuildPlayer();
        BuildRobot();

        ifTimerOn = true;
    }

    private void Start()
    {
        InfoBar = GameObject.Find("InfoBar");
        InfoBar.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            EndDuel();
        }
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
        if (curLevelData.curLevel == 1)
        {
            tmpGO.AddComponent<AniControlHandScript>();
            RobotAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanEnemy");
            MyUtil.FindTransformInChildren(RobotPosition.transform, "Sword").gameObject.SetActive(false);

        }
        else if (curLevelData.curLevel == 2)
        {
            tmpGO.AddComponent<AniControlSwordScript>();
            RobotAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC/ACHumanSwordEnemy");
        }
        RobotPosition.AddComponent<AIActionControlScript>();
        RobotPosition.AddComponent<GoundDetectScript>();

        cinemachineTargetGroup.AddMember(MyUtil.FindTransformInChildren(RobotPosition.transform, "mixamorig1:Hips"), 1, 0.3f);
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

        if (gameData.CharacterName == "C1")
            cinemachineTargetGroup.AddMember(MyUtil.FindTransformInChildren(PlayerPosition.transform, "J_Bip_C_Hips"), 1, 0.3f);
        if (gameData.CharacterName == "C2")
            cinemachineTargetGroup.AddMember(MyUtil.FindTransformInChildren(PlayerPosition.transform, "mixamorig1:Hips"), 1, 0.3f);

    }

    public void EndDuel()
    {
        InfoBar.SetActive(true);
        ifTimerOn = false;

        if (PlayerHPBar.fillAmount > 0)
        {
            InfoBar.GetComponentInChildren<Text>().text = "YOU WIN";
            StartCoroutine(CR_ChangeSceneWhenWin());
            curLevelData.isPassed = true;
        }
        else
        {
            InfoBar.GetComponentInChildren<Text>().text = "YOU LOSE";
            RobotAnimator.SetBool("isVectory", true);
            PlayerPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;
            StartCoroutine(CR_ChangeSceneWhenLose());
            curLevelData.isPassed = false;
        }
        RobotPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;

        string path = Application.dataPath + "/Resources/curLevelData";
        MyUtil.SaveClassToBinary<CurLevelData>(path, curLevelData);

    }

    IEnumerator CR_ChangeSceneWhenWin()
    {
        yield return new WaitForSeconds(5);
        PlayerAnimator.SetBool("isVectory", true);
        PlayerPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);
    }

    IEnumerator CR_ChangeSceneWhenLose()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);
    }
}
