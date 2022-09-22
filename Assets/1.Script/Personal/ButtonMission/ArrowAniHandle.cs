using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowAniHandle : MonoBehaviour
{
    Animator arrowAni;
    [SerializeField]
    GameObject[] arrowPosi;
    int index = 0 ;
    int[] vecZ = new int[] { 60, -85, -100, 90, 120 };
    
    private void Start()
    {
        arrowAni = GetComponent<Animator>();
        StartCoroutine(Speed_forArrow());
        //��⼭ �����Ҹ� ����ϴ� �̺�Ʈ ����
    }
    
    //�ַο찡 �����̴� ����� ������Ű�鼭 �ִϸ��̼� �ٲ��ִ� �Լ�
    IEnumerator Speed_forArrow()
    {
        yield return new WaitForSeconds(5.5f);
        arrowAni.SetInteger("ArrowAction", 1);
        while (index < arrowPosi.Length)
        {
            print("�ݺ�üũ");
            //transform.Rotate(new Vector3(0, vecZ[index], 0) * Time.deltaTime);
            transform.Rotate(0, vecZ[index], 0);
            while (Vector3.Distance(transform.position, arrowPosi[index].transform.position) > 0.2f) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
            {
                transform.position = Vector3.Lerp(transform.position, arrowPosi[index].transform.position, Time.deltaTime * 0.7f);
                yield return new WaitForSeconds(Time.deltaTime);
                if (Vector3.Distance(transform.position, arrowPosi[index].transform.position) == 0)
                {
                    break;
                }
            }
            transform.position = arrowPosi[index].transform.position;
            index++;
        }
        arrowAni.SetInteger("ArrowAction", 2);
        yield break;
    }
}
