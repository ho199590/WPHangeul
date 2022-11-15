using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //몬스터 목표 위치 
    [SerializeField]
    Transform spawnPoint;
    NavMeshAgent agent;
    Rigidbody rigid;
    Animator anim;
    int num;
    public string animname;

    protected void Awake()
    {
        movePoint = GameObject.Find("Point").GetComponentsInChildren<Transform>();
        spawnPoint = GameObject.Find("SpwanPoint").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        num = Random.Range(0, movePoint.Length);
        anim.SetInteger(animname, 3);
        transform.position = spawnPoint.transform.position; //첫시작시 위치
    }
    private void Update()
    {
        agent.SetDestination(movePoint[num].transform.position);
        FreezeVelocity();
        if (Vector3.Distance(transform.position, movePoint[num].transform.position) < 1f)
        {
            num = Random.Range(0, movePoint.Length);
        }
    }
    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
/*
NavMeshAgent animal = null;
Rigidbody rb;
[SerializeField] Transform[] walkpoint = null;
int count = 0;


void MovetoNextPoint()
{
    //오브젝트가 속도가 0이 되면 다음 포지션으로 넘어간다는 뜻
    if (animal.velocity == Vector3.zero)
    {

        animal.SetDestination(walkpoint[count++].position);
        //더이상 walkpoint가 없을때 카운트들 다시 0으로 초기화 하여 시작지점으로 간다
        if (count >= walkpoint.Length)
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
public float currTime;     //시간을 담당할 변수를 하나 만들어준다.
[SerializeField]
GameObject[] Monster;      //몬스터 배열
[SerializeField]
GameObject SpwanPoint;     //몬스터 생성 위치 
[SerializeField]
GameObject[] movePoint;    //몬스터 목표 위치 
int num;
bool move = false;
private void Start()
{
    StartCoroutine(AnimalMove());
}
IEnumerator AnimalMove()
{
    while (true)
    {
        currTime += Time.deltaTime; //시간이 흐르게 만들어줌.
                                    //오브젝트를 몇초마다 생성할 것인지 조건문으로 만든다 (5초)
        if (currTime > 5)
        {
            //생성할 오브젝트를 불러온다.
            GameObject monster = Instantiate(Monster[Random.Range(0, Monster.Length)]);
            num = Random.Range(0, movePoint.Length);
            //SpwanPoint 에서 monster를 생성하게 한다.
            monster.transform.position = SpwanPoint.transform.position;

            while (Vector3.Distance(monster.transform.position, movePoint[num].transform.position) >= 1f)
            {
                if (Vector3.Distance(monster.transform.position, movePoint[num].transform.position) <= 1.5f)
                {
                    StartCoroutine(RandomMove(monster));
                    currTime = 0;
                    continue;
                }
                monster.transform.position = Vector3.Lerp(monster.transform.position, movePoint[num].transform.position, 0.05f);
                yield return new WaitForSeconds(Time.fixedDeltaTime);

            }
            //시간을 0으로 되돌려주면 , 5초마다 반복한다.

        }
        yield return null;
    }
}
IEnumerator RandomMove(GameObject monster)
{
    print("함수 실행");
    int rnum = Random.Range(0, movePoint.Length);

    while (true)
    {
        monster.transform.position = Vector3.Lerp(monster.transform.position, movePoint[rnum].transform.position, 0.05f);
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        if (Vector3.Distance(monster.transform.position, movePoint[rnum].transform.position) == 0f)
        {
            rnum = Random.Range(0, movePoint.Length);
        }
    }

}*/