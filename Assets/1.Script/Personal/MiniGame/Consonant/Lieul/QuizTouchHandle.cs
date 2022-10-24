using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    public event System.Action<GameObject> QuizCheck; //���� ������ �� �߻��� �̺�Ʈ
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
        QuizCheck?.Invoke(gameObject); //�����Լ� ������ ���� �̺�Ʈ ȣ��
    }
}
