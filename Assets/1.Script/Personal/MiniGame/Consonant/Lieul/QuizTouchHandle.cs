using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ʈ���� ����ó���� OnMouse���� ���
public class QuizTouchHandle : MonoBehaviour
{
    [Tooltip ("����Ʈ�� ����� �߷����� ����߷������ ������Ʈ�� ��� �־��ּ���")]
    [SerializeField]
    Rigidbody[] drop; //����Ʈ�� ���� ���۽� �߷����� ����߷���� �ϴ� ������Ʈ
    [Tooltip ("����Ʈ�� ����� Ȱ��ȭ ����� �� ������Ʈ ��� �־��ּ���")]
    [SerializeField]
    GameObject[] active; //����Ʈ�� ���� ���۽� Ȱ��ȭ������� ������Ʈ
    [Tooltip("����Ʈ�� ���� ������ ���ο� ���ص� ��ֹ��� ��� �־��ּ���")]
    [SerializeField]
    GameObject[] obstacles; //������߸� ��Ȱ��ȭ�� ���ι��ع�
    
    [Tooltip ("����Ʈ�� ������ ����ó�� ������ �Է����ּ���")]
    [SerializeField]
    int num;
    private void Start()
    {
        //���� ����ÿ� �ʿ��� ������Ʈ ����߷��ֱ�
        if (drop != null)
        {
            for (int j = 0; j < drop.Length; j++)
                drop[j].useGravity = true;
        }
        //���� ����ÿ� �ʿ��� ������Ʈ Ȱ��ȭ���ֱ� 
        if (active != null)
        {
            for (int k = 0; k < active.Length; k++)
                active[k].SetActive(true);
        }
        FindObjectOfType<NaviMoveManager>().QuizCheck += RemoveObstacles; //�� ��ũ��Ʈ�� ����ִ� ������Ʈ�� Ȱ��ȭ ���ڸ��� �̺�Ʈ�� �Լ� �߰�

    }
    void RemoveObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++) obstacles[i].gameObject.SetActive(false);
    }
    //���������Ʈ�� Ŭ���� �̺�Ʈ ȣ���ؼ� ����ǰԲ� �Ķ���Ͱ��� ����
    private void OnMouseUp()
    {
        print("����Ŭ��O");
        if (GetComponent<Rigidbody>()) 
        {
            print("�ڱ��ڽ� üũ");
            GetComponent<Rigidbody>().useGravity = true; //�ι�° ��� Ŭ������ ����߷����ϴ� ������Ʈ��
        }

        //gameObject.SetActive(false); //�ڱ��ڽŵ� ���ι����� ��ֹ������� ��Ȱ��ȭ
        FindObjectOfType<NaviMoveManager>().QuizNum = num; //�Ķ���Ͱ��� ���������ν� �̺�Ʈ ȣ��(�Ķ���ͳ��� �ȿ� �̺�Ʈ ȣ���� ������)
    }
}
