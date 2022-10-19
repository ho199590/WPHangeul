using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal_Move : MonoBehaviour
{
    //ǳ�� ������ ��ũ��Ʈ ����
    Balloon_Move b_Move;
    bool ismove;
    [SerializeField]
    AnimalMovePosition animalMovePosition;
    Random_AnimalChoice random_AnimalChoice;
    //�̺�Ʈ
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
    //Balloon_Move ��ũ��Ʈ Off
    private void MoveOnOff()//Balloon_Move ��ũ��Ʈ Off
    {
        b_Move.enabled = !ismove;
    }
    private void OnTriggerEnter(Collider other)
    {
        //�⿪ ���忡 �꿴����
        if (other.gameObject.layer == LayerMask.NameToLayer("GiyeokAnswer"))
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
        //���� ���忡 �꿴���� 
        if (other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))
        {
            ismove = true;
            MoveOnOff();
            FreezeVelocity();
        }
    }
    //ǳ������ �������� ������ ���� 0���� �ʱ�ȭ
    private void FreezeVelocity()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false ;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    //������ ������ġ�� �̵� 
    public void AnimalMove()
    {
        StartCoroutine(MoveFunction());
        
        print("LookAt");
    }
    IEnumerator MoveFunction()
    {
        this.transform.LookAt(animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position);
        //���� �Ÿ��� ��������  0.05f �̻��̸� ����
        while (Vector3.Distance(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position) > 0.5f)
        {
            //������Ű��
            yield return new WaitForSeconds(Time.deltaTime);
            
            //���� ��ġ���� ������ ���� Lerp�� �̵�
            transform.position = Vector3.Lerp(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position, Time.deltaTime);
        }
        //while���� ������ ������ġ�� Ÿ����ġ�� ����
        transform.position = animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position;
        yield break;
    }
}
