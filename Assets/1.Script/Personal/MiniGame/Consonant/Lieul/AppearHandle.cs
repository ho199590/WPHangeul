using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lerp�� �����Ҷ� ������ 
public class AppearHandle : MonoBehaviour
{
    float lerpTime = 2f; //����� �ҿ�ð�
    float currentTime = 0;
    Vector3 start;
    Vector3 startrotation;
    [SerializeField]
    Transform arrive; //��������

    Animator animator;
    void Start()
    {
        start = transform.position;
        StartCoroutine(Appear());
        if (GetComponent<Animator>()) 
        { 
            animator = GetComponent<Animator>();
            animator.SetInteger("Lieul_Quiz", 1);
        }
    }
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, currentTime / lerpTime);
    //}
    IEnumerator Appear()
    {
        while (currentTime / lerpTime < 1)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, arrive.position, currentTime / lerpTime);
            yield return null;
        }
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            if (animator.parameterCount != 0)
                animator.SetInteger("Lieul_Quiz", 2);
            else
                animator.enabled = false;
        }
    }
}
