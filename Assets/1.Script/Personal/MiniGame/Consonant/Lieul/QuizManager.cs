using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������ ����ó�� �Ŵ���
public class QuizManager : MonoBehaviour
{
    public List<GameObject> quizObjects; //Ȱ��ȭ�� <QuizTouchHandle>��ũ��Ʈ�� ����ִ� ������Ʈ�� �־��� ���� �� ����Ʈ
    [SerializeField]
    List<GameObject> obstacles; //���� ���� �� �������� ��ֹ�

    //<QuizTouchHandle>��ũ��Ʈ�� ����ִ� ������Ʈ�� Ȱ��ȭ�Ǹ� �������Ե� ������Ƽ
    public GameObject ActiveObject
    {
        set
        {
            quizObjects.Add(value);
            print("���� ó�� ī��Ʈ ��" + quizObjects.Count);
            //�����Լ� �̺�Ʈ�� �߰����ֱ�
            var quiztouch = FindObjectsOfType<QuizTouchHandle>();
            foreach(var item in quiztouch)
            {
                item.QuizCheck += RemoveObject;
            }
        }
    }
    //<QuizTouchHandle>��ũ��Ʈ���� OnMouseUp�϶� ����Ʈ���� ������Ʈ �������ִ� �Լ�
    //����Ʈ�� �� ������Ʈ�� Ȱ��ȭ�ɶ� �� �Լ��� �̺�Ʈ�� �߰����ֱ�
    void RemoveObject(GameObject minus)
    {
        print("������� ������Ʈ ����");
        quizObjects.Remove(minus);
        if (quizObjects.Count == 0) 
        {
            foreach (var item in obstacles) item.SetActive(false);
            FindObjectOfType<NaviMoveManager>().Check = true;
        }
    }
}

