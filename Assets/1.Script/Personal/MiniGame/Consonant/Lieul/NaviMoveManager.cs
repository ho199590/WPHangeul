using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//����Ű��� ��(Mover)�� �ڵ� �������� ������ִ� NavMesh���  
public class NaviMoveManager : MonoBehaviour
{
    [Tooltip ("������� ������ ���� ��ġ(NavMesh�� Ÿ��)�� �־��ּ���")]
    [SerializeField]
    Transform[] target; //������� NavMesh�� �������� �־��ֱ�
    [SerializeField]
    GameObject[] speechBubble; //ù��° ������ ��ǳ����
    [SerializeField]
    Collider[] planes; //�ٴڿ� �����ִ� ��� �浹ó���� plane
    [SerializeField]
    Collider drop; //�ݶ��̴��� �����ִ� ������ ���� 
    [SerializeField]
    GameObject center; //������ ������ ������ ��� ��ġ�� ����ٱ���
    [SerializeField]
    GameObject[] basket;

    Vector3 destination;
    NavMeshAgent agent;

    //��� ȸ����
    int index;
    //���̴� ���� ����ó�� ī��Ʈ��
    int invokeCount = 0;
    //����ó�� ī��Ʈ��
    int answerCount = 0;
    //���� ��������ó�� ī��Ʈ ���� �����
    int preQuizNum = 1;
    public event System.Action QuizCheck; //���� ������ �� �߻��� �̺�Ʈ

    //�ݶ��̴��� OnTrigger�� �������� ���� NavMesh�� Ÿ��(��������)�� ���� Ÿ������ �ٲ��ִ� ������Ƽ 
    public int DestinationIndex
    {
        get => index; //Ÿ��(Transform)���� �迭 �ε��� 
        set
        {
            index = value;
            destination = target[index].position; //NavMesh�� ���������� Ÿ���� ��ġ�� ����
        }
    }
    //���� ���� ����...?
    public int QuizNum
    {
        //get => preQuizNum; 
        set 
        {
            invokeCount++;
            if(preQuizNum == value) preQuizNum = value; //"���� ����"�� ����ó�� ī��Ʈ ���� �����
            print("���������� numüũ:" + preQuizNum);
            if (invokeCount < value + preQuizNum) 
            {
                answerCount++;
                print("answerCount�� üũ :" + answerCount);
                if (answerCount == value) 
                {
                    QuizCheck?.Invoke();
                    agent.isStopped = false;
                    preQuizNum = value; //���� ������� �� ���� ����� ����
                }
            }
            else 
            { 
                QuizCheck?.Invoke();
                agent.isStopped = false;
            }
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = target[0].position; //ó���� �ѹ��� 1�� Ÿ��(��������)���� NavMesh�� ����ϵ��� ����
    }
    void Update()
    {
        agent.SetDestination(destination); //NavMesh�� Ÿ��(��������) ����
    }
    //���� ����� �������鼭 ����ؼ� ���� �������� �� �浹ó��
    public void OnTriggerEnter(Collider other) //Collider�� Is Trigger üũ�Ǿ��־�� �浹 //�������(��������X)
    {
        if (other != null)
        {
            print("��Ʈ���ſ���" + other);
            agent.isStopped = true; //�׺�޽� ��ž
        }
    }
    //�ڳʿ��� ���� collider�� �����Ǿ� �ִ� ȸ�� �������� ���� �Ȱ��� ȸ��
    private void OnCollisionEnter(Collision collision) //Collider�� Is Trigger üũ�ȵǾ� �־�� �浹 //����ȵ�(��������O)
    {
        if (collision != null)
        {
            print("���ݸ�������" + collision.gameObject.name);
            if (index < 4)
            {
                DestinationIndex = ++index; //���� Ÿ������ �ε����� +1�ؼ� �Ѱ��ֱ�
            }
        }
    }
    //ù��° ����� �̺�Ʈ�� ���� ���� �Լ�
    //void Quiz1Right()
    //{
    //    print("�̺�Ʈ ���� �׽�Ʈ1");
    //    speechBubble[1].SetActive(false);
    //    drop.isTrigger = true;
    //    planes[0].isTrigger = true;
    //    center.SetActive(false);
    //    agent.isStopped = false; //�׺�޽� ��ž ��
    //}
    //�ι�° ����� �̺�Ʈ�� ���� ���� �Լ�
    //void Quiz2Right(bool check, GameObject goChild)
    //{
    //    print("�̺�Ʈ ���� �׽�Ʈ2");
    //    if (check)
    //    {
    //        count++;
    //        print("count=" + count);
    //        if(goChild.gameObject.name.Contains("Aubergine")) goChild.transform.SetParent(basket[0].transform);
    //        else goChild.transform.SetParent(basket[1].transform);
    //        if (count == 5)
    //        {
    //            for (int i = 0; i < basket.Length; i++) basket[i].SetActive(false);
    //            for(int i=1; i<8;i++) planes[i].isTrigger = true;
    //            agent.isStopped = false; //�׺�޽� ��ž ��
    //        }
    //    }
    //}
    
}
