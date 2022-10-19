using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuizObjectHandle : MonoBehaviour
{
    [Tooltip ("����Ʈ�� ������۶� <QuizTouchHandle>��ũ��Ʈ�� ����ִ� Ȱ��ȭ�� �ʿ��� ������Ʈ�鸸 �� �־��ּ���")]
    [SerializeField]
    GameObject[] quizObjects; //����Ʈ�� ������۽� Ȱ��ȭ����� �� ������Ʈ
    [Tooltip("<QuizTouchHandle>��ũ��Ʈ�� ����ְ� ����Ʈ�� ������۶� ����߷���� �� ������Ʈ�鸸 �� �־��ּ���")]
    [SerializeField]
    Rigidbody[] drops; //����Ʈ�� ������۽� ����߷���� �� ������Ʈ


    private void OnTriggerEnter(Collider other)
    {
        if(quizObjects != null)
        {
            for (int i = 0; i < quizObjects.Length; i++)
            {
                quizObjects[i].SetActive(true);
            }
        }
        if(drops != null)
        {
            for(int j =0; j < drops.Length; j++)
            {
                drops[j].useGravity = true;
            }
        }
      
    }
}
