using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    public GameObject x;
    public GameObject invisible;
    public NavMeshAgent ball;

    [SerializeField]
    GameObject plane;
    
    private void OnMouseUp()
    {
        print("클릭 후 마우스 땠음");
        if (gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            print("정답클릭O");
            gameObject.SetActive(false);
            x.SetActive(false);
            invisible.SetActive(false);
            ball.isStopped = false;
            plane.SetActive(false);
        }
        else
        {
            print("오답클릭X");
        }
    }
}
