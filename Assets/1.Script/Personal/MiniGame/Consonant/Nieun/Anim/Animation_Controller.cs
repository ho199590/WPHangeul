using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{
    public static Action plusNum;

    private static int index = 0;

    [SerializeField]
    Random_AnimalChoice choice;

    [SerializeField]
    GameObject animalOb;

    private void Start()
    {
        choice = GetComponent<Random_AnimalChoice>();
    }
    public void IndexNum()
    {
        Index++;
    }
    public int Index
    {
        get => index;
        set
        {
            index = value;
            if (index >= 5)
            {
                print("animaldace�Լ� ����");
                AnimalDance();
            }
        }
    }
    //�̼� Ŭ����� ���� �� �ִϸ��̼� ����
    public void AnimalDance()
    {
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Jump");
    }
}
