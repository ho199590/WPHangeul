using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//���� �������� ���� ��ũ��Ʈ
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
    //Balloon_Move ��ũ��Ʈ Off
    private void MoveOnOff()//Balloon_Move ��ũ��Ʈ Off
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
    }
    //������ �������� ���� �Լ�
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
