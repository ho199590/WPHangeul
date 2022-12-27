using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //몬스터 목표 위치 변수
    [SerializeField]
    Transform spawnPoint;     //몬스터 생성 위치 변수
    [SerializeField]
    GameObject particle;      //몬스터 드래그 파티클
    [SerializeField]
    GameObject answerAnimal;  //완성될때 나올 동물 변수 
    NavMeshAgent agent;       //NMA 변수
    Rigidbody rigid;          //충돌시 일어나는 예외 상황 방지 변수
    Animator anim;            //동물 시작 애니메이션 지정 변수
    private IEnumerator coroutine;//코루틴 변수 선언
    int randomInt;            //movePoint 순서를 랜덤하게 저장 할 변수
    bool monsterDrag = false;         //드래그 중일때만 출돌 판별 변수
    SpeakerHandler speaker;     //스피커 변수
    protected void Awake()
    {
        GetPoint();
        VariableRest();
        coroutine = AiMonster(); 
        randomInt = Random.Range(0, movePoint.Length); 
        anim.SetInteger(anim.GetParameter(0).name, 5);   
        transform.position = spawnPoint.transform.position; 
        StartCoroutine(coroutine);
        speaker = FindObjectOfType<SpeakerHandler>();
    }
    IEnumerator AiMonster()
    {
        while (true)
        {
            agent.SetDestination(movePoint[randomInt].transform.position);
            FreezeVelocity();
            Destination();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void FreezeVelocity() 
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    void GetPoint()
    {
        movePoint = GameObject.Find("Point").GetComponentsInChildren<Transform>();
        spawnPoint = GameObject.Find("SpwanPoint").GetComponent<Transform>();
    }
    void VariableRest() 
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Destination() 
    {
        if (Vector3.Distance(transform.position, movePoint[randomInt].transform.position) < 1f)
        {
            randomInt = Random.Range(0, movePoint.Length);
        }
    }
  
    private void OnMouseDown()
    {
        agent.speed = 0f;//몬스터 속도 값
        rigid.isKinematic = false;
        speaker.SoundByNum2(3);//사운드
    }
    private void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);            
        objPos.y = 0f;//y축 고정
        transform.position = objPos;

        monsterDrag = true;
        GameObject effect = Instantiate(particle);
        effect.transform.position = transform.position;
        Destroy(effect, 0.5f);
    }
    private void OnMouseUp()
    {
        agent.speed = 3.5f;//몬스터 속도 값
        monsterDrag = false;
        rigid.isKinematic = true;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (monsterDrag && col.transform.name == transform.name)
        {
            Destroy(col.gameObject);  
            Destroy(this.gameObject);
            AnimalSpwan.plusCount(col.contacts[0].point);
            AnswerAnimal(col);
        }
    }
    private void AnswerAnimal(Collision col)
    {
        speaker.SoundByNum2(2);
        GameObject animalOb = Instantiate(answerAnimal);
        animalOb.transform.position = col.contacts[0].point;
        animalOb.transform.position = new Vector3(animalOb.transform.position.x, 5f, animalOb.transform.position.z);
        animalOb.GetComponent<Animator>().SetInteger(animalOb.GetComponent<Animator>().GetParameter(0).name, 4);
        CameraMove.MoveEvents(animalOb);
        MissionList.ObjectOn(animalOb);
    }
}