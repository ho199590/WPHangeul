using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalAI : MonoBehaviour
{
    NavMeshAgent animal = null;
    Rigidbody rb;
    [SerializeField] Transform[] walkpoint = null;
    int count = 0;


    void MovetoNextPoint()
    {
        //오브젝트가 속도가 0이 되면 다음 포지션으로 넘어간다는 뜻
        if(animal.velocity == Vector3.zero)
        {

            animal.SetDestination(walkpoint[count++].position);
            //더이상 walkpoint가 없을때 카운트들 다시 0으로 초기화 하여 시작지점으로 간다
            if(count >= walkpoint.Length)
            {
                count = 0;
            }
        }
    }
    private void Start()
    {
        animal = GetComponent<NavMeshAgent>();//NaveMesh 초기화
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("MovetoNextPoint", 0f, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.angularVelocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
