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
        //������Ʈ�� �ӵ��� 0�� �Ǹ� ���� ���������� �Ѿ�ٴ� ��
        if(animal.velocity == Vector3.zero)
        {

            animal.SetDestination(walkpoint[count++].position);
            //���̻� walkpoint�� ������ ī��Ʈ�� �ٽ� 0���� �ʱ�ȭ �Ͽ� ������������ ����
            if(count >= walkpoint.Length)
            {
                count = 0;
            }
        }
    }
    private void Start()
    {
        animal = GetComponent<NavMeshAgent>();//NaveMesh �ʱ�ȭ
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
