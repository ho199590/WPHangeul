using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    int completedQuizOrder;
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    //����Ʈ�� ������ ������Ʈ���� ���° ����Ʈ���� �˷��ִ� ������Ƽ
    public int CompletedQuizOrder
    {
        set
        {
            completedQuizOrder = value;
        }
    }
    private void OnMouseUp()
    {
        print("����Ŭ��!");
        if (GetComponent<Rigidbody>()) //2��° ����Ʈ���� ������Ʈ ����߸����
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (completedQuizOrder == 2 || completedQuizOrder == 4) //�ι�°, �׹�° ����Ʈ�� ��� ����Ʈ �Ϸ� ����
        {
            print("������ �Ѱ����� �Ϸ� �����ϴ� ����Ʈ");
            StartCoroutine(DelayComplete());
        }
        else //������ ����Ʈ���� "�ٷ�" �Ϸ�ó��
        {
            //Ŭ���� ������Ʈ�� �׵θ��� ��¦�̴� ���������� �����Բ� �ϴ� if�� �߰� ���� //4��° ����Ʈ��
            FindObjectOfType<QuizManager>().AddNRemove = gameObject;
            Destroy(GetComponent<QuizTouchHandle>()); //���� ������Ʈ�� �ι��̻� ������ ��� �Ǵٽ� ����ó�� ������Ʈ ����Ʈ�� �߰��Ǵ� ���� �����ϱ� ���� ������Ʈ ����
        }
        if (GetComponent<Outline>() && GetComponent<Animator>())
        {
            GetComponent<Outline>().enabled = true; //4��° ����Ʈ���� ����Ŭ���� �ƿ����� ����Ʈ ���ֱ�
            GetComponent<Animator>().enabled = false;
        }
    }
    //2�� ����Ʈ���� ������ ������Ʈ�� �ٱ��Ͽ� �������� ������� �������� ����Ʈ �Ϸ�ǵ��� ���������ִ� �Լ�
    IEnumerator DelayComplete()
    {
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        Destroy(GetComponent<QuizTouchHandle>());
    }
}
