using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{
    public static Action plusNum;
    private int index;
    [SerializeField]
    Animator animalAnimation;
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
    //미션 클리어시 동물 춤 애니메이션 수정
    private void AnimalDance()
    {
        print("동물 춤");
    }
}
