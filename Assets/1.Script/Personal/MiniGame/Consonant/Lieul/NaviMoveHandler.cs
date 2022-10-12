using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMoveHandler : MonoBehaviour
{
    public Transform[] target;
    Vector3 destination;
    NavMeshAgent agent;
    int index;
    [SerializeField]
    Rigidbody[] drop;
    public int DestinationIndex
    {
        get => index;
        set
        {
            index = value;
            destination = target[index].position;
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = target[0].position;
    }
    void Update()
    {
        agent.SetDestination(destination);
    }
    public void OnTriggerEnter(Collider other) //Collider�� Is Trigger üũ�Ǿ��־�� �浹 //�������(��������X)
    {
        if (other != null)
        {
            print("��Ʈ���ſ���" + other);
            if (index < 4)
            {
                DestinationIndex = ++index;
                print(index);
            }
            //Rotation = other;
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
            //if (other.gameObject.name.Contains("Right"))
            //{
            //    while (i < purple.Length)
            //    {
            //        purple[i].transform.GetComponent<Rigidbody>().useGravity = true;
            //        i++;
            //    }
            //}
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
