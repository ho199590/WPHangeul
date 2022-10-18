using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
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
    public Collider drop; //�ݶ��̴��� �����ִ� ������ ���� 
    public GameObject center; //������ ������ ������ ��� ��ġ�� ����ٱ���
    public GameObject[] basket;

    Vector3 destination;
    NavMeshAgent agent;
    int index;
    int count = 0;
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
            if (other.gameObject.name.Contains("InvisibleWall1"))
            {
                print("ù��° ����Ʈ");
                agent.isStopped = true; //�׺�޽� ��ž
                center.SetActive(true);
                drop.GetComponent<Rigidbody>().useGravity = true;
                for (int i = 0; i < speechBubble.Length; i++) speechBubble[i].SetActive(true); //��ǳ�� ��Ÿ����
                FindObjectOfType<QuizTouchHandle>().QuizCheck1 += Quiz1Right; //QuizTouchHandle��ũ��Ʈ�� �������� �� ������ �������� ��� QuizCheck�̺�Ʈ�� �� ������ ��!
            }
            else if (other.gameObject.name.Contains("InvisibleWall2"))
            {
                print("�ι�° ����Ʈ");
                agent.isStopped = true; //�׺�޽� ��ž
                for (int i = 0; i < basket.Length;i++) basket[i].SetActive(true);
                var draghandles = FindObjectsOfType<DragNDropHandle>(); //��ũ��Ʈ�� �������� �� ������ ������ ��ũ��Ʈ�� �̺�Ʈ�� �ؿ�ó�� ������ �־���� �� //FindObject"s"OfType
                foreach (var item in draghandles)
                {
                    item.QuizCheck2 += Quiz2Right;
                }
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
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
                print(index);
            }
        }
    }
    void Quiz1Right()
    {
        print("�̺�Ʈ ���� �׽�Ʈ1");
        speechBubble[1].SetActive(false);
        drop.isTrigger = true;
        planes[0].isTrigger = true;
        center.SetActive(false);
        agent.isStopped = false; //�׺�޽� ��ž ��
    }
    void Quiz2Right(bool check, GameObject goChild)
    {
        print("�̺�Ʈ ���� �׽�Ʈ2");
        if (check)
        {
            count++;
            print("count=" + count);
            if(goChild.gameObject.name.Contains("Aubergine")) goChild.transform.SetParent(basket[0].transform);
            else goChild.transform.SetParent(basket[1].transform);
            if (count == 5)
            {
                for (int i = 0; i < basket.Length; i++) basket[i].SetActive(false);
                for(int i=1; i<8;i++) planes[i].isTrigger = true;
                agent.isStopped = false; //�׺�޽� ��ž ��
            }
        }
    }
    
}
