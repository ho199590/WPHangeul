using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//�ַο����� ������ ���� �̵��ϴ� ��ũ��Ʈ
public class ArrowAniHandle : MonoBehaviour
{
    Animator arrowAni;
    [SerializeField]
    GameObject[] arrowPosi;
    public event Action ButtonActive;
    
    private void Start()
    {
        arrowAni = GetComponent<Animator>();
        StartCoroutine(Speed_forArrow());
    }
    //�ַο찡 �����̴� ����� ������Ű�鼭 �ִϸ��̼� �ٲ��ִ� �Լ�
    IEnumerator Speed_forArrow()
    {
        yield return new WaitForSeconds(5.5f);
        arrowAni.SetInteger("ArrowAction", 1);

        for(int i = 0; i< arrowPosi.Length; i++)
        {
            transform.LookAt(arrowPosi[i].transform);
            while (Vector3.Distance(transform.position, arrowPosi[i].transform.position) > 0.1f) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
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
        ButtonActive?.Invoke(); //�ַο찡 ������ �����ϸ� ���� ���� ��� �����ϴ� �̺�Ʈ ����
        yield break;
    }
}
