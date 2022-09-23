using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowAniHandle : MonoBehaviour
{
    Animator arrowAni;
    [SerializeField]
    GameObject[] arrowPosi;
    
    int[] vecZ = new int[] { 60, -85, -100, 90, 120 };
    
    private void Start()
    {
        arrowAni = GetComponent<Animator>();
        StartCoroutine(Speed_forArrow());
        //요기서 라디오소리 재생하는 이벤트 연결
    }
    
    //애로우가 움직이는 모습을 지연시키면서 애니메이션 바꿔주는 함수
    IEnumerator Speed_forArrow()
    {
        yield return new WaitForSeconds(5.5f);
        arrowAni.SetInteger("ArrowAction", 1);

        for(int i = 0; i< arrowPosi.Length; i++)
        {
            transform.LookAt(arrowPosi[i].transform);
            while (Vector3.Distance(transform.position, arrowPosi[i].transform.position) > 0.1f) //둘사이의 거리가 있는 동안 //첨에 0으로 했다가 너무 느려서 10으로 바꿈
            {
                transform.position = Vector3.Lerp(transform.position, arrowPosi[i].transform.position, Time.deltaTime * 0.8f);
                
                //transform.rotation = Quaternion.Slerp(transform.rotation, arrowPosi[i].transform.rotation, Time.time);
                yield return new WaitForSeconds(Time.deltaTime);
                if (Vector3.Distance(transform.position, arrowPosi[i].transform.position) <= 0.1f)
                {
                    break;
                }
            }
            transform.position = arrowPosi[i].transform.position;
        }
        arrowAni.SetInteger("ArrowAction", 2);
        yield break;
    }
    
}
