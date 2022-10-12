using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    public GameObject x;
    public GameObject invisible;
    public NavMeshAgent ball;

    [SerializeField]
    GameObject plane;
    
    private void OnMouseUp()
    {
        print("Ŭ�� �� ���콺 ����");
        if (gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            print("����Ŭ��O");
            gameObject.SetActive(false);
            x.SetActive(false);
            invisible.SetActive(false);
            ball.isStopped = false;
            plane.SetActive(false);
        }
        else
        {
            print("����Ŭ��X");
        }
    }
}
