using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://bloodstrawberry.tistory.com/992
public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //몬스터 목표 위치 변수
    [SerializeField]
    Transform spawnPoint;     //몬스터 생성 위치 변수
    NavMeshAgent agent;       //NMA 변수
    Rigidbody rigid;          //충돌시 일어나는 예외 상황 방지 변수
    Animator anim;            //동물 시작 애니메이션 지정 변수
    private IEnumerator coroutine;//코루틴 변수 선언
    public string animname;   //어떤 애니메이션을 실행할지 인스펙터창에서 지정 변수
    int randomInt;            //movePoint 순서를 랜덤하게 저장 할 변수
    bool monsterDrag = false;         //드래그 중일때만 출돌 판별 변수
    protected void Awake()
    {
        GetPoint();
        VariableRest();
        coroutine = AiMonster(); //Aimonster()코루틴으로 초기화 
        randomInt = Random.Range(0, movePoint.Length); //랜덤 변수 저장
        anim.SetInteger(animname, 3);   //애니메이션 시작
        transform.position = spawnPoint.transform.position; //첫시작시 위치
        StartCoroutine(coroutine);
    }
    IEnumerator AiMonster()             //Ai몬스터 이동시작
    {
        while (true)
        {
            agent.SetDestination(movePoint[randomInt].transform.position); //ai몬스터 목적지로 이동 시작
            FreezeVelocity();
            Destination();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void FreezeVelocity()   //ai 몬스터 충돌시 뒤로 밀리는 충돌 멈춤 
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    void GetPoint()  //Point들의 위치를 인스펙터에 저장한다.
    {
        movePoint = GameObject.Find("Point").GetComponentsInChildren<Transform>();
        spawnPoint = GameObject.Find("SpwanPoint").GetComponent<Transform>();
    }
    void VariableRest() //변수 초기화.
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Destination() //목적지에 도착하면 목적지 변경
    {
        if (Vector3.Distance(transform.position, movePoint[randomInt].transform.position) < 1f)
        {
            randomInt = Random.Range(0, movePoint.Length);
        }
    }
    //ai몬스터들 드래그 할시 
    private void OnMouseDown()
    {
        agent.speed = 0f;
    }
    private void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; //World 좌표를 스크린 좌표로 전환 , z좌표가 카메라의 표준위치 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);             //ScreenToWorldPoint가 다시 스크린의 마우스 좌표를 오브젝트의 좌표로 전환.
        objPos.y = 0f;//y축 고정
        transform.position = objPos;

        monsterDrag = true;
    }
    private void OnMouseUp()
    {
        agent.speed = 3.5f;
        monsterDrag = false;

    }
    private void OnTriggerEnter(Collider other) //드래그 중일때 똑같은 몬스터면 삭제
    {
        if (monsterDrag && other.name == transform.name)
        {
            Destroy(other.gameObject);  //두 오브젝트 삭제
            Destroy(this.gameObject);
            AnimalSpwan.plusCount(other.transform.position);//몬스터 갯수 Count 늘려줌 
        }
    }
}