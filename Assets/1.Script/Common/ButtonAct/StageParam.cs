using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageParam : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    SceneChanger changer;

    private void Start()
    {
        changer = FindObjectOfType<SceneChanger>();
        GetComponent<ButtonAct>().FuncBasket += StageScene;
    }


    void StageScene()
    {
        changer.ChangeScene(SceneName);
    }
}
