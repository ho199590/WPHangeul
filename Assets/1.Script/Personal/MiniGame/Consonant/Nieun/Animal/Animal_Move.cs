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
    private void MoveOnOff()//Balloon_Move ��ũ��Ʈ Off
    {
        b_Move.enabled = !ismove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("GiyeokAnswer"))//�⿪ ���忡 �꿴����
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))//���� ���忡 �꿴���� 
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
    }
    private void FreezeVelocity()//ǳ������ �������� ������ ���� 0���� �ʱ�ȭ
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false ;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
