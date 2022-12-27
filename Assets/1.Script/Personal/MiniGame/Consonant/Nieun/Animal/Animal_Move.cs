using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//동물 목적지로 가는 스크립트
public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    bool ismove; 
    [SerializeField]
    AnimalMovePosition animalMovePosition;
    Random_AnimalChoice random_AnimalChoice;
    public static Action animal_move;

    private void Awake()
    {
        animal_move = () =>
        {
            AnimalMove();
        };
    }
    private void Start()
    {
        b_Move = GetComponentInParent<Balloon_Move>();
        random_AnimalChoice = GetComponent<Random_AnimalChoice>(); 
    }
    //Balloon_Move 스크립트 Off
    private void MoveOnOff()//Balloon_Move 스크립트 Off
    {
        b_Move.enabled = !ismove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GiyeokAnswer"))
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
        //니은 농장에 닿였을때 
        if (other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
    }
    //풍선에서 떨어지는 물리적 연산 0으로 초기화
    private void FreezeVelocity()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false ;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    //동물이 지정위치로 이동 
    public void AnimalMove()
    {
        StartCoroutine(MoveFunction());
    }
    //동물이 목적지로 가는 함수
    IEnumerator MoveFunction()
    {
        this.transform.LookAt(animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position);
        while (Vector3.Distance(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position) > 0.5f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            
            transform.position = Vector3.Lerp(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position, Time.deltaTime);
        }
        transform.position = animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position;
        yield break;
    }
}
