using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//방향키대신 공(Mover)의 자동 움직임을 담당해주는 NavMesh기능  
public class NaviMoveHandler : MonoBehaviour
{
    [Tooltip ("순서대로 도착할 곳의 위치(NavMesh의 타겟)를 넣어주세요")]
    public Transform[] target; //순서대로 NavMesh의 도착지점 넣어주기
    Vector3 destination;
    NavMeshAgent agent;
    int index;
    [SerializeField]
    Rigidbody[] drop;
    //콜라이더를 OnTrigger로 만났을때 방향 NavMesh의 타겟(도착지점)을 다음 타겟으로 바꿔주는 프로퍼티 
    public int DestinationIndex
    {
        get => index; //타겟(Transform)들의 배열 인덱스 
        set
        {
            index = value;
            destination = target[index].position; //NavMesh의 도착지점을 타겟의 위치로 지정
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = target[0].position; //처음에 한번만 1번 타겟(도착지점)으로 NavMesh가 출발하도록 설정
    }
    void Update()
    {
        agent.SetDestination(destination); //NavMesh의 타겟(도착지점) 지정
    }
    public void OnTriggerEnter(Collider other) //Collider에 Is Trigger 체크되어있어야 충돌 //통과가능(물리연산X)
    {
        if (other != null)
        {
            print("온트리거엔터" + other);
            if (index < 4)
            {
                DestinationIndex = ++index; //다음 타겟으로 인덱스값 +1해서 넘겨주기
                print(index);
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
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
