using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
//방향키대신 공(Mover)의 자동 움직임을 담당해주는 NavMesh기능  
public class NaviMoveManager : MonoBehaviour
{
    [Tooltip ("순서대로 도착할 곳의 위치(NavMesh의 타겟)를 넣어주세요")]
    [SerializeField]
    Transform[] target; //순서대로 NavMesh의 도착지점 넣어주기
    [SerializeField]
    GameObject[] speechBubble; //첫번째 퀴즈의 말풍선들
    [SerializeField]
    Collider[] planes; //바닥에 숨어있는 모든 충돌처리용 plane
    public Collider drop; //콜라이더를 갖고있는 떨어진 과일 
    public GameObject center; //떨어진 과일이 떨어질 가운데 위치용 투명바구니
    public GameObject[] basket;

    Vector3 destination;
    NavMeshAgent agent;
    int index;
    int count = 0;
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
    //공이 골목을 지나가면서 통과해서 만나 지나가게 될 충돌처리
    public void OnTriggerEnter(Collider other) //Collider에 Is Trigger 체크되어있어야 충돌 //통과가능(물리연산X)
    {
        if (other != null)
        {
            print("온트리거엔터" + other);
            if (other.gameObject.name.Contains("InvisibleWall1"))
            {
                print("첫번째 퀘스트");
                agent.isStopped = true; //네브메쉬 스탑
                center.SetActive(true);
                drop.GetComponent<Rigidbody>().useGravity = true;
                for (int i = 0; i < speechBubble.Length; i++) speechBubble[i].SetActive(true); //말풍선 나타나기
                FindObjectOfType<QuizTouchHandle>().QuizCheck1 += Quiz1Right; //QuizTouchHandle스크립트가 여러군데 들어가 있으면 랜덤으로 어느 QuizCheck이벤트에 들어가 있을지 모름!
            }
            else if (other.gameObject.name.Contains("InvisibleWall2"))
            {
                print("두번째 퀘스트");
                agent.isStopped = true; //네브메쉬 스탑
                for (int i = 0; i < basket.Length;i++) basket[i].SetActive(true);
                var draghandles = FindObjectsOfType<DragNDropHandle>(); //스크립트가 여러군데 들어가 있으면 각각의 스크립트의 이벤트에 밑에처럼 일일히 넣어줘야 함 //FindObject"s"OfType
                foreach (var item in draghandles)
                {
                    item.QuizCheck2 += Quiz2Right;
                }
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime * 50);
        }
    }
    //코너에서 만난 collider의 설정되어 있는 회전 방향으로 공도 똑같이 회전
    private void OnCollisionEnter(Collision collision) //Collider에 Is Trigger 체크안되어 있어야 충돌 //통과안됨(물리연산O)
    {
        if (collision != null)
        {
            print("온콜리전엔터" + collision.gameObject.name);
            if (index < 4)
            {
                DestinationIndex = ++index; //다음 타겟으로 인덱스값 +1해서 넘겨주기
                print(index);
            }
        }
    }
    void Quiz1Right()
    {
        print("이벤트 실행 테스트1");
        speechBubble[1].SetActive(false);
        drop.isTrigger = true;
        planes[0].isTrigger = true;
        center.SetActive(false);
        agent.isStopped = false; //네브메쉬 스탑 끝
    }
    void Quiz2Right(bool check, GameObject goChild)
    {
        print("이벤트 실행 테스트2");
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
                agent.isStopped = false; //네브메쉬 스탑 끝
            }
        }
    }
    
}
