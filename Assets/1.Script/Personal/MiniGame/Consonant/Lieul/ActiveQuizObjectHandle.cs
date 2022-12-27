using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������ �� ��� Ǯ�� ���� ������Ʈ Ȱ��ȭ��
public class ActiveQuizObjectHandle : MonoBehaviour
{
    [Tooltip("����Ʈ�� ������۶� Ȱ��ȭ�� �ʿ��� ������Ʈ�鸸 �� �־��ּ���")]
    [SerializeField]
    GameObject[] quizObjects; //����Ʈ�� ������۽� Ȱ��ȭ����� �� ������Ʈ
    [Tooltip("������۶� ����߷���� �� ������Ʈ�鸸 �� �־��ּ���")]
    [SerializeField]
    Rigidbody[] drops; //����Ʈ�� ������۽� ����߷���� �� ������Ʈ
    int count;
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<QuizManager>().QuizOrder = count; //���° �������� ī��Ʈ�� ������Ƽ ȣ��
        if (quizObjects != null)
        {
            for (int i = 0; i < quizObjects.Length; i++)
            {
                quizObjects[i].SetActive(true);
            }
        }
        if (drops != null)
        {
            for (int j = 0; j < drops.Length; j++)
            {
                drops[j].useGravity = true;
            }
        }
    }
}
