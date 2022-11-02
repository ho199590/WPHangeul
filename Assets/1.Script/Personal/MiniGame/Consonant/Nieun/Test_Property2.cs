using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Cube2
{
    Test_Property2 test = new Test_Property2();
    private int currentHP;
    public int CurrenHP
    {
        get => currentHP;

        set
        {
            currentHP = value;
            if(currentHP == 5)
            {
                
                test.Test();
            }
        }
    }

}

public class Test_Property2 : MonoBehaviour
{
    [SerializeField]
    Animator animator;//동물 anim
    public static Action Num;
    Cube2 cube2 = new Cube2();
    private void Awake()
    {
        Num = () =>
        {
            OnNum();
        };
    }
    private void OnMouseDown()
    {
        /*   print(cube2.currentHP);*/
        ++cube2.CurrenHP;
        print(cube2.CurrenHP + "증가");
    }

    public void OnNum()
    {
        cube2.CurrenHP++;
    }
    public void Test()
    {
        print("이벤트발생");
        animator.SetInteger("Camel_LOD0_SH", 1);
    }
}
