using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
//����Ű��� ��(Mover)�� �ڵ� �������� ������ִ� NavMesh���  
public class NaviMoveHandler : MonoBehaviour
{
    [Tooltip ("������� ������ ���� ��ġ(NavMesh�� Ÿ��)�� �־��ּ���")]
    [SerializeField]
    Transform[] target; //������� NavMesh�� �������� �־��ֱ�
    [SerializeField]
    GameObject[] speechBubble; //ù��° ������ ��ǳ����
    public GameObject invisible; //�� ��ž�� ������ �ʴ� OnTrigger�� ������Ʈ
    public GameObject drop; //�ݶ��̴��� �����ִ� ������ ���� 
    public GameObject center; //������ ������ ������ ��� ��ġ�� ����ٱ���
    public GameObject[] basket;

    Vector3 destination;
    NavMeshAgent agent;
    int index;

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
        FindObjectOfType<QuizTouchHandle>().QuizCheck1 += Quiz1Right;
        FindObjectOfType<DragNDropHandle>().QuizCheck2 += Quiz2Right;
    }
    void Update()
    {
        agent.SetDestination(destination); //NavMesh�� Ÿ��(��������) ����
    }
    public void OnTriggerEnter(Collider other) //Collider�� Is Trigger üũ�Ǿ��־�� �浹 //�������(��������X)
    {
        if (other != null)
        {

            print("��Ʈ���ſ���" + other);
            if (other.gameObject.name.Contains("Invisible"))
            {
                print("ù��° ����Ʈ");
                agent.isStopped = true; //�׺�޽� ��ž
                for (int i = 0; i < speechBubble.Length; i++) speechBubble[i].SetActive(true); //��ǳ�� ��Ÿ����
                drop.GetComponent<Rigidbody>().useGravity = true;
                
            }
            if(other.gameObject.name.Contains("Invisible(1)"))
            {
                print("�ι�° ����Ʈ");
            }
            if (other.gameObject.name.Contains("Invisible(2)"))
            {
                print("����° ����Ʈ");
            }
            if (other.gameObject.name.Contains("Invisible(3)"))
            {
                print("�׹�° ����Ʈ");
            }
            if (other.gameObject.name.Contains("Invisible(4)"))
            {
                print("����!");
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
        }
    }
    public void Quiz1Right()
    {
        print("�̺�Ʈ ���� �׽�Ʈ1");
        speechBubble[1].SetActive(false);
        invisible.SetActive(false);
        drop.GetComponent<Collider>().isTrigger = true;
        center.SetActive(false);
        agent.isStopped = false; //�׺�޽� ��ž ��
    }
    public void Quiz2Right()
    {
        print("�̺�Ʈ ���� �׽�Ʈ2");
        for(int i = 0; i < basket.Length; i++) basket[i].SetActive(false);
    }
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
}
