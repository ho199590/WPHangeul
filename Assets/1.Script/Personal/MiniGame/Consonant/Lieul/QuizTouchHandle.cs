using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    public event System.Action QuizCheck1; //���� ������ �� �߻��� �̺�Ʈ

    private void OnMouseUp()
    {
        if (gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            print("����Ŭ��O");
            QuizCheck1?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
