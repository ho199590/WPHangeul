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
            // �� �̸��� Re�� �Է��� ��� ���� Ȱ��ȭ�� ������ �̵� = ���ΰ�ħ
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
    //HJ_�̴ϰ��� �������� Ʃ�丮��� ������ִ� �Լ�
    public void PlayIntroduction()
    {
        LoadingIntroductionManager.LoadScene(SceneName);
        FindObjectOfType<SceneChanger>().IntroductionAction -= PlayIntroduction;
    }
}
