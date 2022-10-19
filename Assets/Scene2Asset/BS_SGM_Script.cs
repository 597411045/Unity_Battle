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
        base.InitInstance(this);
        string path = "D:\\1102\\Github\\GameData\\gameData";
        gameData = new GameData();

        gameData.CharacterName = CC_SGM_Script.instance.CharacterName;
        gameData.WeaponType = CC_SGM_Script.instance.WeaponType;

        FileStream fs = File.Open(path, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        gameData = bf.Deserialize(fs) as GameData;
        fs.Close();
        InfoBar = GameObject.Find("InfoBar");
        cinemachineTargetGroup = GameObject.Find("TargetGroup1").GetComponent<CinemachineTargetGroup>();

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
        }
        else
        {
            InfoBar.GetComponentInChildren<Text>().text = "YOU LOSE";
            RobotAnimator.SetBool("isVectory", true);
            PlayerPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;
            StartCoroutine(CR_ChangeSceneWhenLose());
        }
        RobotPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;
    }

    IEnumerator CR_ChangeSceneWhenWin()
    {
        yield return new WaitForSeconds(5);
        PlayerAnimator.SetBool("isVectory", true);
        PlayerPosition.GetComponentInChildren<BaseActionControlScript>().enabled = false;

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("ScoreBoard");
    }

    IEnumerator CR_ChangeSceneWhenLose()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("ScoreBoard");
    }
}
