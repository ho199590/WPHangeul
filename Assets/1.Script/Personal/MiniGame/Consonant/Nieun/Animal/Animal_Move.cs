using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    Rigidbody Animal_Ob;
    Collider Animal_Col;
    bool ismove;
    private void Start()
    {
        b_Move = GetComponentInParent<Balloon_Move>();
        Animal_Ob = GetComponent<Rigidbody>();
        Animal_Col = GetComponent<Collider>();
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
        Animal_Col.isTrigger = false ;
        Animal_Ob.velocity = Vector3.zero;
        Animal_Ob.angularVelocity = Vector3.zero;
    }
}
