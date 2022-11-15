using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalAI : MonoBehaviour
{
    [SerializeField]
    Transform[] movePoint;    //���� ��ǥ ��ġ 
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
        transform.position = spawnPoint.transform.position; //ù���۽� ��ġ
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
    //������Ʈ�� �ӵ��� 0�� �Ǹ� ���� ���������� �Ѿ�ٴ� ��
    if (animal.velocity == Vector3.zero)
    {

        animal.SetDestination(walkpoint[count++].position);
        //���̻� walkpoint�� ������ ī��Ʈ�� �ٽ� 0���� �ʱ�ȭ �Ͽ� ������������ ����
        if (count >= walkpoint.Length)
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
public float currTime;     //�ð��� ����� ������ �ϳ� ������ش�.
[SerializeField]
GameObject[] Monster;      //���� �迭
[SerializeField]
GameObject SpwanPoint;     //���� ���� ��ġ 
[SerializeField]
GameObject[] movePoint;    //���� ��ǥ ��ġ 
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
        currTime += Time.deltaTime; //�ð��� �帣�� �������.
                                    //������Ʈ�� ���ʸ��� ������ ������ ���ǹ����� ����� (5��)
        if (currTime > 5)
        {
            //������ ������Ʈ�� �ҷ��´�.
            GameObject monster = Instantiate(Monster[Random.Range(0, Monster.Length)]);
            num = Random.Range(0, movePoint.Length);
            //SpwanPoint ���� monster�� �����ϰ� �Ѵ�.
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
            //�ð��� 0���� �ǵ����ָ� , 5�ʸ��� �ݺ��Ѵ�.

        }
        yield return null;
    }
}
IEnumerator RandomMove(GameObject monster)
{
    print("�Լ� ����");
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