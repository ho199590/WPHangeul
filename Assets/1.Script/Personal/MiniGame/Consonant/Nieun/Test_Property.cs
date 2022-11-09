using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_Property : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
       animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        print("Å¬¸¯¤¡");
        animator.SetInteger("Camel_LOD0_SH", 1);
    }
}
