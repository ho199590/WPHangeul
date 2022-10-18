using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal_Move : MonoBehaviour
{
    Balloon_Move b_Move;
    bool ismove;
    public bool move = false;
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
        if(other.gameObject.layer == LayerMask.NameToLayer("NieunAnswer"))//���� ���忡 �꿴���� 
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
    public void AnimalMove()//������ ������ġ�� �̵� 
    {
        StartCoroutine(MoveFunction());
    }
    IEnumerator MoveFunction()
    {
        //���� �Ÿ��� ��������  0.05f �̻��̸� ����
        while (Vector3.Distance(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position) > 0.5f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            //���� ��ġ���� ������ ���� Lerp�� �̵�
            transform.position = Vector3.Lerp(transform.position, animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position, Time.deltaTime);
        }
        //while���� ������ ������ġ�� Ÿ����ġ�� ����
        transform.position = animalMovePosition.AnimalPoint[this.random_AnimalChoice.number].transform.position;
        yield break;
    }
}
