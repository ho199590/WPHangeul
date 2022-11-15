using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://bloodstrawberry.tistory.com/992
public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //���� ��ǥ ��ġ ����
    [SerializeField]
    Transform spawnPoint;     //���� ���� ��ġ ����
    NavMeshAgent agent;       //NMA ����
    Rigidbody rigid;          //�浹�� �Ͼ�� ���� ��Ȳ ���� ����
    Animator anim;            //���� ���� �ִϸ��̼� ���� ����
    private IEnumerator coroutine;//�ڷ�ƾ ���� ����
    public string animname;   //� �ִϸ��̼��� �������� �ν�����â���� ���� ����
    int randomInt;            //movePoint ������ �����ϰ� ���� �� ����
    bool monsterDrag = false;         //�巡�� ���϶��� �⵹ �Ǻ� ����
    protected void Awake()
    {
        GetPoint();
        VariableRest();
        coroutine = AiMonster(); //Aimonster()�ڷ�ƾ���� �ʱ�ȭ 
        randomInt = Random.Range(0, movePoint.Length); //���� ���� ����
        anim.SetInteger(animname, 3);   //�ִϸ��̼� ����
        transform.position = spawnPoint.transform.position; //ù���۽� ��ġ
        StartCoroutine(coroutine);
    }
    IEnumerator AiMonster()             //Ai���� �̵�����
    {
        while (true)
        {
            agent.SetDestination(movePoint[randomInt].transform.position); //ai���� �������� �̵� ����
            FreezeVelocity();
            Destination();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void FreezeVelocity()   //ai ���� �浹�� �ڷ� �и��� �浹 ���� 
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    void GetPoint()  //Point���� ��ġ�� �ν����Ϳ� �����Ѵ�.
    {
        movePoint = GameObject.Find("Point").GetComponentsInChildren<Transform>();
        spawnPoint = GameObject.Find("SpwanPoint").GetComponent<Transform>();
    }
    void VariableRest() //���� �ʱ�ȭ.
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Destination() //�������� �����ϸ� ������ ����
    {
        if (Vector3.Distance(transform.position, movePoint[randomInt].transform.position) < 1f)
        {
            randomInt = Random.Range(0, movePoint.Length);
        }
    }
    //ai���͵� �巡�� �ҽ� 
    private void OnMouseDown()
    {
        agent.speed = 0f;
    }
    private void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; //World ��ǥ�� ��ũ�� ��ǥ�� ��ȯ , z��ǥ�� ī�޶��� ǥ����ġ 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);             //ScreenToWorldPoint�� �ٽ� ��ũ���� ���콺 ��ǥ�� ������Ʈ�� ��ǥ�� ��ȯ.
        objPos.y = 0f;//y�� ����
        transform.position = objPos;

        monsterDrag = true;
    }
    private void OnMouseUp()
    {
        agent.speed = 3.5f;
        monsterDrag = false;

    }
    private void OnTriggerEnter(Collider other) //�巡�� ���϶� �Ȱ��� ���͸� ����
    {
        if (monsterDrag && other.name == transform.name)
        {
            Destroy(other.gameObject);  //�� ������Ʈ ����
            Destroy(this.gameObject);
            AnimalSpwan.plusCount(other.transform.position);//���� ���� Count �÷��� 
        }
    }
}