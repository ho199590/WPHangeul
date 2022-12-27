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
    //원위치로 돌아가는 함수 
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
    //농장에 닿였을때 일어나는 상황
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("정답");
            random_Enable.Choice();
            scoreCase.SetScore();//별 스코어가 올라간다
            animal_Move.AnimalMove();
            animation_Controller.AnimalDance();
        }
        if (other.gameObject.name == "DefaultCollision" || other.gameObject.name == notAnswer)
        {
            print("틀림");
            speakerHandler.SoundByNum2(1);
            ReMove();
            balloon_Touch.SizeReset();
        }
    }
    //동물 원위치로 전환
    private void PositionReset()
    {
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        transform.position = animal;
    }
}

