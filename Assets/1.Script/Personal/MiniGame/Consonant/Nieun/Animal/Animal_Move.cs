using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    Random_AnimalChoice animal_PointNum;
    AnimalMovePosition animalPoint_G,animalPoint_N;
    bool ismove;
    private void Start()
    {
        b_Move = GetComponentInParent<Balloon_Move>();
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
        if (other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))//니은 농장에 닿였을때 
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
}
