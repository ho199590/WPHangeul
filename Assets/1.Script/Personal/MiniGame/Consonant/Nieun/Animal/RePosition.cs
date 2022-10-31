using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true����
    [SerializeField]
    string Answer;//��� ��Ҵ��� Ȯ���ϱ� ���� ����
    [SerializeField]
    string notAnswer; //Ʋ������
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
    Animal_Move animal_Move;//���� ������ ���� 
    [SerializeField]
    Animation_Controller animal_Controller;

    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;//����ǳ�� ��ġ�� ����
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;//�θ�tranceform ����
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        balloon_Touch = this.transform.parent.GetChild(1).GetComponent<Balloon_Touch>();
        random_Enable = FindObjectOfType<Random_Enable>();
        animal_Move = GetComponent<Animal_Move>();//�ڱ��ڽ��� animalMove�� ��� �´�
    }
    //����ġ�� ���ư��� �Լ� 
    public void ReMove()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        PositionReset();
        isMove.enabled = true;
        GetComponent<Collider>().enabled = false;
        bollon.transform.position = bollonPosi;//ǳ�� ����ġ ��ȯ
        bollon.SetActive(true);//ǳ�� �ٽ� Ȱ��ȭ

    }
    //���忡 �꿴���� �Ͼ�� ��Ȳ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("����");
            random_Enable.Choice();
            scoreCase.SetScore();//�� ���ھ �ö󰣴�
            animal_Move.AnimalMove();
            //�����϶��� IndexNum()�Լ� ȣ
            animal_Controller.IndexNum();
        }
        if (other.gameObject.name == "DefaultCollision" || other.gameObject.name == notAnswer)
        {
            print("Ʋ��");
            //����
            speakerHandler.SoundByNum2(1);
            //����ġ
            ReMove();
            //����ũ�� ���� 
            balloon_Touch.SizeReset();
        }
    }
    //���� ����ġ�� ��ȯ
    private void PositionReset()
    {
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        transform.position = animal;//���� ��ġ�� ��ȯ
    }
}

