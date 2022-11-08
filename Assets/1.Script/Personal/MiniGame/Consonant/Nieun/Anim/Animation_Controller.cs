using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{
    [SerializeField]
    Animator[] animator;
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

    //미션 클리어시 동물 춤 애니메이션 수정
    private void AnimalDance()
    {
        animator[0].SetInteger("Crow_LOD0Ani", 1);
        animator[1].SetInteger("SnappingTurtle_LOD0", 1);
        animator[2].SetInteger("Lobster_LOD0", 1);
        animator[3].SetInteger("Goldfish_LOD0Ani", 1);
        animator[4].SetInteger("Hedgehog_LOD0Ani", 1);
        animator[5].SetInteger("Camel_LOD0Ani", 1);
        animator[6].SetInteger("Sloth_LOD0", 1);
    }
    private void OnMouseDown()
    {
        /*Index++;*/
/*        animator[0].SetInteger("Camel_LOD0Ani", 1);
        animator[1].SetInteger("Crow_LOD0Ani", 1);
        animator[2].SetInteger("Goldfish_LOD0Ani", 1);
        animator[3].SetInteger("Hedgehog_LOD0Ani", 1);
        animator[4].SetInteger("Sloth_LOD0", 1);
        animator[5].SetInteger("Lobster_LOD0", 1);
        animator[6].SetInteger("SnappingTurtle_LOD0", 1);*/
    }

    private void AnimalFace()
    {

    }
}
