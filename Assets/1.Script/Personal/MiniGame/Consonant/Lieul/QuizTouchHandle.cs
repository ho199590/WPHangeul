using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    
    private void Start()
    {
        FindObjectOfType<QuizManager>().ActiveObject = gameObject;
    }
    private void OnMouseUp()
    {
        print("����Ŭ��O");
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        FindObjectOfType<QuizManager>().MouseUpCheck = gameObject;
    }
}
