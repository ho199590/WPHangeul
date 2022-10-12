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
    public void OnTriggerEnter(Collider other) //Collider에 Is Trigger 체크되어있어야 충돌 //통과가능(물리연산X)
    {
        if (other != null)
        {
            print("온트리거엔터" + other);
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
                print("도착!");
            }
        }
    }
    private void OnCollisionEnter(Collision collision) //Collider에 Is Trigger 체크안되어 있어야 충돌 //통과안됨(물리연산O)
    {
        if (collision != null)
        {
            print("온콜리전엔터" + collision.gameObject.name);
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
