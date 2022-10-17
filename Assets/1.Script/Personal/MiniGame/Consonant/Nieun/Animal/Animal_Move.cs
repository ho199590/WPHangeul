using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    bool ismove;
    [SerializeField]
    AnimalMovePosition animalMovePosition;
    Random_AnimalChoice random_AnimalChoice;
    private void Start()
    {
        b_Move = GetComponentInParent<Balloon_Move>();
        random_AnimalChoice = GetComponent<Random_AnimalChoice>();
    }

    private void MoveOnOff()//Balloon_Move 스크립트 Off
    {
        b_Move.enabled = !ismove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("GiyeokAnswer"))//기역 농장에 닿였을때
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))//니은 농장에 닿였을때 
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
    }
    private void FreezeVelocity()//풍선에서 떨어지는 물리적 연산 0으로 초기화
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false ;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void AnimalMove()//동물이 지정위치로 이동 
    {
        while (true)
        {
            //현재 위치에서 목적지 까지 Lerp로 이동
            transform.position = Vector3.Lerp(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position, Time.deltaTime);
            //현재 위치와 목적지 사이의 거리가 1f미만이면 멈춤
            if (Vector3.Distance(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position) <= 1f)
            {
                break;
            }
        }
    }

}
