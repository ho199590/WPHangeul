using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{
    public static Action plusNum;
    private int index;

    Animator animalAnimation;

    private void Awake()
    {
        
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
                AnimalDance();
            }
        }
    }
    //�̼� Ŭ����� ���� �� �ִϸ��̼� ����
    private void AnimalDance()
    {
        print("���� ��");
    }
}
