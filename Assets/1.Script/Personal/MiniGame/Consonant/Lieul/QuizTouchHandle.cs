using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    private void OnMouseUp()
    {
        print("����Ŭ��O");
        if (GetComponent<Rigidbody>()) //3��° ����Ʈ��
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        //Ŭ���� ������Ʈ�� �׵θ��� ��¦�̴� ���������� �����Բ� �ϴ� if�� �߰� ���� //4��° ����Ʈ��
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        Destroy(GetComponent<QuizTouchHandle>()); //���� ������Ʈ�� �ι��̻� ������ ��� �Ǵٽ� ����ó�� ������Ʈ ����Ʈ�� �߰��Ǵ� ���� �����ϱ� ���� ������Ʈ ����
    }
}
