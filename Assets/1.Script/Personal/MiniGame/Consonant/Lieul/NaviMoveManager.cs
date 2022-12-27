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
    GameObject[] poles; //ȸ���� ����ǥ
    [SerializeField]
    Camera endCam;
    [SerializeField]
    GameObject minimap; //RawImage
    public Animation[] stores;

    Vector3 destination;
    NavMeshAgent agent;
    //��� ȸ����
    int index;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = target[0].position; //ó���� �ѹ��� 1�� Ÿ��(��������)���� NavMesh�� ����ϵ��� ����
    }
    void Update()
    {
        agent.SetDestination(destination); //NavMesh�� Ÿ��(��������) ����
    }
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
    //���� ���� ī��Ʈ �� ������ 0�Ǹ� �ٽ� �׺�޽� �����̰� ���ִ� ������Ƽ
    public bool Check
    {
        set
        {
            if(value) agent.isStopped = false;
            for (int i = 0; i < stores.Length; i++) stores[i].GetComponent<Animation>().enabled = true;
        }
    }
    //���� ����� �������鼭 �����ݶ��̴��� ������ �׺� �޽� ��ž
    public void OnTriggerEnter(Collider other) //Collider�� Is Trigger üũ�Ǿ��־�� �浹 //�������(��������X)
    {
        if (other != null)
        {
            print("��Ʈ���ſ���" + other);
            agent.isStopped = true;
            for (int i = 0; i < stores.Length; i++) stores[i].GetComponent<Animation>().enabled = false;
        }
    }
    //�ڳʿ��� ���� ����collider�� �����Ǿ� �ִ� ȸ�� �������� ���� �Ȱ��� ȸ��
    private void OnCollisionEnter(Collision collision) //Collider�� Is Trigger üũ�ȵǾ� �־�� �浹 //����ȵ�(��������O)
    {
        if (collision != null)
        {
            print("���ݸ�������" + collision.gameObject.name);
            if (index < 4)
            {
                poles[index].GetComponent<PoleRotationHandle>().enabled = true;
                DestinationIndex = ++index; //���� Ÿ������ �ε����� +1�ؼ� �Ѱ��ֱ�
            }
            if (collision.gameObject.name.Contains("Fin"))
            {
                Camera.main.enabled = false;
                //endCam.depth = 1;
                //endCam.enabled = true;
                endCam.GetComponent<Animator>().enabled = true;
                minimap.SetActive(false);
            }
        }
    }
}
