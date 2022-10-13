using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true����
    [SerializeField]
    string Answer;//��� ��Ҵ��� Ȯ���ϱ� ���� ����
    [SerializeField]
    GameObject bollon;//bollon ������Ʈ
    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������ 
    Vector3 bollonPosi;
    Vector3 animal;//animal positon
    Vector3 perentsPosition; //ReMove()�Ҷ� ���������� ����� tranceform
    SpeakerHandler speakerHandler;
    Balloon_Touch balloon_Touch; //Balloon_Touch SizeReset() �Լ� �ҷ�����
    Random_Enable random_Enable;//�����Ͻ� ���� ������Ʈ Ȱ��ȭ Choice()�Լ� �ҷ�����
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;//����ǳ�� ��ġ�� ����
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;//�θ�tranceform ����
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        balloon_Touch = this.transform.parent.GetChild(1).GetComponent<Balloon_Touch>();
        random_Enable = FindObjectOfType<Random_Enable>();
    }
    public void ReMove()//Ʋ���ָ� ReMove ���Ѿ��� 
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        PositionReset();
        isMove.enabled = true;
        GetComponent<Collider>().enabled = false;
        bollon.transform.position = bollonPosi;//ǳ�� ����ġ ��ȯ
        bollon.SetActive(true);//ǳ�� �ٽ� Ȱ��ȭ

    }
    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("����");
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;//������ �浹 ó�� 0
            /* this.GetComponent<Rigidbody>().useGravity = false; //�߷� ��
            this.GetComponent<NavMeshAgent>().enabled = true; //�׺�޽��� ���ش�
            this.gameObject.GetComponent<AI_AnimalMove2>().enabled = true;//�ڱ� �ڽſ� �޸� AI_AnimalMove2 �� ���ش�*/
            random_Enable.Choice();
            scoreCase.SetScore();
        }
        else
        {
            print($"{balloon_Touch}Ʋ��");
            speakerHandler.SoundByNum2(1);
            ReMove();
            balloon_Touch.SizeReset();
            
        }
    }
    private void PositionReset()
    {
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        transform.position = animal;//���� ��ġ�� ��ȯ
    }
}

