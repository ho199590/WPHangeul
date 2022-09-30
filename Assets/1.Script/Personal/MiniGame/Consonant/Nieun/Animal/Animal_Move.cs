using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    Rigidbody Animal_Ob;
    bool ismove;
    private void Start()
    {
        b_Move = GetComponentInParent<Balloon_Move>();
        Animal_Ob = GetComponent<Rigidbody>();
    }
    private void MoveOnOff()
    {
        b_Move.enabled = !ismove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("GiyeokAnswer"))
        {
            print("��� �浹");
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))
        {
            print("���� �浹");
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
    }
    private void FreezeVelocity()//ǳ������ �������� ������ ���� 0���� �ʱ�ȭ
    {
        Animal_Ob.velocity = Vector3.zero;
        Animal_Ob.angularVelocity = Vector3.zero;
    }
}
