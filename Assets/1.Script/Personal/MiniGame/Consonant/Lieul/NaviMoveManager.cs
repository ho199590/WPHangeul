using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField]
    Collider drop; //콜라이더를 갖고있는 떨어진 과일 
    [SerializeField]
    GameObject center; //떨어진 과일이 떨어질 가운데 위치용 투명바구니
    [SerializeField]
    GameObject[] basket;

    Vector3 destination;
    NavMeshAgent agent;

    //골목 회전용
    int index;
    //쌓이는 퀴즈 정답처리 카운트용
    int invokeCount = 0;
    //정답처리 카운트용
    int answerCount = 0;
    //이전 퀴즈정답처리 카운트 갯수 저장용
    int preQuizNum = 1;
    public event System.Action QuizCheck; //퀴즈 맞췄을 때 발생할 이벤트

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
    //퀴즈 남은 갯수...?
    public int QuizNum
    {
        //get => preQuizNum; 
        set 
        {
            invokeCount++;
            if(preQuizNum == value) preQuizNum = value; //"이전 퀴즈"의 정답처리 카운트 갯수 저장용
            print("이전퀴즈의 num체크:" + preQuizNum);
            if (invokeCount < value + preQuizNum) 
            {
                answerCount++;
                print("answerCount값 체크 :" + answerCount);
                if (answerCount == value) 
                {
                    QuizCheck?.Invoke();
                    agent.isStopped = false;
                    preQuizNum = value; //이전 퀴즈꺼에서 그 다음 퀴즈껄로 변경
                }
            }
            else 
            { 
                QuizCheck?.Invoke();
                agent.isStopped = false;
            }
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
            agent.isStopped = true; //네브메쉬 스탑
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
            }
        }
    }
    //첫번째 퀴즈용 이벤트에 담을 정답 함수
    //void Quiz1Right()
    //{
    //    print("이벤트 실행 테스트1");
    //    speechBubble[1].SetActive(false);
    //    drop.isTrigger = true;
    //    planes[0].isTrigger = true;
    //    center.SetActive(false);
    //    agent.isStopped = false; //네브메쉬 스탑 끝
    //}
    //두번째 퀴즈용 이벤트에 담을 정답 함수
    //void Quiz2Right(bool check, GameObject goChild)
    //{
    //    print("이벤트 실행 테스트2");
    //    if (check)
    //    {
    //        count++;
    //        print("count=" + count);
    //        if(goChild.gameObject.name.Contains("Aubergine")) goChild.transform.SetParent(basket[0].transform);
    //        else goChild.transform.SetParent(basket[1].transform);
    //        if (count == 5)
    //        {
    //            for (int i = 0; i < basket.Length; i++) basket[i].SetActive(false);
    //            for(int i=1; i<8;i++) planes[i].isTrigger = true;
    //            agent.isStopped = false; //네브메쉬 스탑 끝
    //        }
    //    }
    //}
    
}
