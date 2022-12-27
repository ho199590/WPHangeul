using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    [Tooltip("����� ������Ʈ�̸� üũ")]
    [SerializeField]
    bool answerCheck; //������ ������Ʈ Ȯ�ο� �Ķ����
    int completedQuizOrder;
    private void OnEnable()
    {
        if(answerCheck) FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    //����Ʈ�� ������ �� ���� ������Ʈ���� ���° ����Ʈ���� �˷��ִ� ������Ƽ
    public int CompletedQuizOrder
    {
        set
        {
            completedQuizOrder = value;
        }
    }
    private void OnMouseUp()
    {
        if (GetComponent<Outline>() && GetComponent<Animator>()) //4��° ����Ʈ�� �ƿ�����
        {
            GetComponent<Outline>().enabled = true;
            GetComponent<Animator>().enabled = false;
        }
        if (answerCheck)
        {
            print("����Ŭ��!");
            if (GetComponent<Rigidbody>()) //2��° ����Ʈ�� ����߸���
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
            if (completedQuizOrder != 3) //3��° ����Ʈ�� �����ϰ� ������Ű��
            {
                StartCoroutine(DelayComplete());
            }
            else //������ ����Ʈ���� "�ٷ�" �Ϸ�ó��
            {
                FindObjectOfType<QuizManager>().AddNRemove = gameObject;
                Destroy(GetComponent<QuizTouchHandle>()); //���� ������Ʈ�� �ι��̻� ������ ��� �Ǵٽ� ����ó�� ������Ʈ ����Ʈ�� �߰��Ǵ� ���� �����ϱ� ���� ������Ʈ ����
            }
        }
        else
        {
            print("��");
            //���⿡ Ʋ���� ����� �Ҹ� �ֱ�
        }
    }
    //����Ʈ�Ϸ� ���� �Լ�
    IEnumerator DelayComplete()
    {
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        Destroy(GetComponent<QuizTouchHandle>());
    }
}
