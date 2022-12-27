using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;
    [SerializeField]
    string Answer;
    [SerializeField]
    string notAnswer; 
    [SerializeField]
    GameObject bollon;
    [SerializeField]
    ScoreHandler scoreCase; 
    Vector3 bollonPosi;
    Vector3 animal;
    Vector3 perentsPosition; 
    SpeakerHandler speakerHandler;
    Balloon_Touch balloon_Touch; 
    Random_Enable random_Enable;
    Animal_Move animal_Move;
    [SerializeField]
    Animation_Controller animation_Controller;

    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        balloon_Touch = this.transform.parent.GetChild(1).GetComponent<Balloon_Touch>();
        random_Enable = FindObjectOfType<Random_Enable>();
        animal_Move = GetComponent<Animal_Move>();
    }
    //����ġ�� ���ư��� �Լ� 
    public void ReMove()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        PositionReset();
        isMove.enabled = true;
        GetComponent<Collider>().enabled = false;
        bollon.transform.position = bollonPosi;
        bollon.SetActive(true);

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
            animation_Controller.AnimalDance();
        }
        if (other.gameObject.name == "DefaultCollision" || other.gameObject.name == notAnswer)
        {
            print("Ʋ��");
            speakerHandler.SoundByNum2(1);
            ReMove();
            balloon_Touch.SizeReset();
        }
    }
    //���� ����ġ�� ��ȯ
    private void PositionReset()
    {
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        transform.position = animal;
    }
}

