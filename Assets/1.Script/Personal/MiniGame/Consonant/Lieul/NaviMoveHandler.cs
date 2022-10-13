using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//����Ű��� ��(Mover)�� �ڵ� �������� ������ִ� NavMesh���  
public class NaviMoveHandler : MonoBehaviour
{
    [Tooltip ("������� ������ ���� ��ġ(NavMesh�� Ÿ��)�� �־��ּ���")]
    public Transform[] target; //������� NavMesh�� �������� �־��ֱ�
    Vector3 destination;
    NavMeshAgent agent;
    int index;
    [SerializeField]
    Rigidbody[] drop;
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
    public void OnTriggerEnter(Collider other) //Collider�� Is Trigger üũ�Ǿ��־�� �浹 //�������(��������X)
    {
        if (other != null)
        {
            print("��Ʈ���ſ���" + other);
            if (index < 4)
            {
                DestinationIndex = ++index; //���� Ÿ������ �ε����� +1�ؼ� �Ѱ��ֱ�
                print(index);
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
            other.gameObject.SetActive(false);
            if (other.gameObject.name.Contains("Fin"))
            {
                print("����!");
            }
        }
    }
    private void OnCollisionEnter(Collision collision) //Collider�� Is Trigger üũ�ȵǾ� �־�� �浹 //����ȵ�(��������O)
    {
        if (collision != null)
        {
            print("���ݸ�������" + collision.gameObject.name);
            if (collision.gameObject.name.Contains("Invisible")) 
            { 
                agent.isStopped = true;
                for (int i = 0; i < drop.Length; i++)
                {
                    drop[i].useGravity= true;
                }
            }
            
        }
    }
}
