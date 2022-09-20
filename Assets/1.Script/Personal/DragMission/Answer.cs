using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//��ü�̼��� ��� �ϼ��Ͽ� ī��Ʈ���� ���϶� �����߾�並 ����
public class Answer : MonoBehaviour
{
    [SerializeField]
    GameObject pico;
    [SerializeField]
    GameObject zoom;
    [SerializeField]
    Image zoomPosition;
    [SerializeField]
    GameObject invisible;
    Animator animator;
    void Start()
    {
        GetComponent<ForCount>().Connect += AnswerIs;
    }
    void AnswerIs(object sender, CountParameter e)
    {
        if (e.count1 == 4)
        {
            //SoundInterface.instance.SoundPlay(5);
            print("�̼ǳ� : ���� win(hi-host) �ִϸ��̼� �÷���");
            zoom.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Delay_forZoom());
            animator = pico.GetComponent<Animator>();
            animator.SetInteger("PicoAction", 3); //���ھִϸ��̼� �߿� 3�� hi-host�ѱ�
        }
    }
    //�̼ǿϼ��� �����Ⱑ ���� õõ�� ���ڸ��� ���Բ�
    IEnumerator Delay_forZoom()
    {
        yield return new WaitForSeconds(2.5f);
        zoom.transform.position = zoomPosition.transform.position;
        zoom.GetComponent<CapsuleCollider>().enabled = false;
        yield break;
    }
}
