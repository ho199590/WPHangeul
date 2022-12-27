using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //���� ��ǥ ��ġ ����
    [SerializeField]
    Transform spawnPoint;     //���� ���� ��ġ ����
    [SerializeField]
    GameObject particle;      //���� �巡�� ��ƼŬ
    [SerializeField]
    GameObject answerAnimal;  //�ϼ��ɶ� ���� ���� ���� 
    NavMeshAgent agent;       //NMA ����
    Rigidbody rigid;          //�浹�� �Ͼ�� ���� ��Ȳ ���� ����
    Animator anim;            //���� ���� �ִϸ��̼� ���� ����
    private IEnumerator coroutine;//�ڷ�ƾ ���� ����
    int randomInt;            //movePoint ������ �����ϰ� ���� �� ����
    bool monsterDrag = false;         //�巡�� ���϶��� �⵹ �Ǻ� ����
    SpeakerHandler speaker;     //����Ŀ ����
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
        agent.speed = 0f;//���� �ӵ� ��
        rigid.isKinematic = false;
        speaker.SoundByNum2(3);//����
    }
    private void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);            
        objPos.y = 0f;//y�� ����
        transform.position = objPos;

        monsterDrag = true;
        GameObject effect = Instantiate(particle);
        effect.transform.position = transform.position;
        Destroy(effect, 0.5f);
    }
    private void OnMouseUp()
    {
        agent.speed = 3.5f;//���� �ӵ� ��
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