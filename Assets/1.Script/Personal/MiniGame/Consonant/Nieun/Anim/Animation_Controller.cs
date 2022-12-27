using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//정답일때 동물 행동
public class Animation_Controller : MonoBehaviour
{   
    public static Action plusNum;
    private static int index = 0;
    [SerializeField]
    Random_AnimalChoice choice;
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
                AnimalDance();
            }
        }
    }
    public void AnimalDance()
    {
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Jump");
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Eyes_Happy");
    }
}
