using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageParam_minigame : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    SceneChanger changer;

    private void Start()
    {
        if (SceneName.Equals("Re"))
        {
            // 씬 이름에 Re를 입력할 경우 현제 활성화된 씬으로 이동 = 새로고침
            SceneName = SceneManager.GetActiveScene().name;            
        }
        changer = FindObjectOfType<SceneChanger>();
        GetComponent<ButtonAct>().FuncBasket += StageScene;
    }


    void StageScene()
    {
        changer.ChangeScene("Introduction");
        FindObjectOfType<SceneChanger>().IntroductionAction += PlayIntroduction;
    }
    //HJ_미니게임 시작전에 튜토리얼씬 재생해주는 함수
    public void PlayIntroduction()
    {
        LoadingIntroductionManager.LoadScene(SceneName);
        FindObjectOfType<SceneChanger>().IntroductionAction -= PlayIntroduction;
    }
}
