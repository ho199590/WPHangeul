using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{   
    //�̺�Ʈ �Լ� ���
    public static Action plusNum;
    //������ ������ static ���� ����
    private static int index = 0;
    //SerializeField�� choice.Number�� ���° �������� Ȯ��
    [SerializeField]
    Random_AnimalChoice choice;
    private void Start()
    {
        //animalchoice ��ũ��Ʈ �ʱ�ȭ
        choice = GetComponent<Random_AnimalChoice>();
    }
    //Index ���� �Լ�
    public void IndexNum()
    {
        Index++;
    }
    //Property
    public int Index
    {
        get => index;
        set
        {
            index = value;
            if (index >= 5)
            {
                AnimalDance();
            }
        }
    }
    //�̼� Ŭ����� ���� �� �ִϸ��̼� ����
    public void AnimalDance()
    {
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Jump");
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Eyes_Happy");
    }
}
