using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
public class CountDown : MonoBehaviour
{
    [Serializable]
    private class CountDownEvent : UnityEvent { }
    private CountDownEvent endOffCountDown;     //ī��Ʈ �ٿ� ���� �� �ܺ� �޼ҵ� ������ ���� �̺�Ʈ Ŭ���� ���

    private TextMeshProUGUI textCountDown;      //ī��Ʈ �ٿ� �ؽ�Ʈ�� ����ϴ� Text UI
    private AudioSource audionSource;           //ī��Ʈ �ٿ� ���� ���

    [SerializeField]
    private int maxFontSize;                    //��Ʈ�� �ִ� ũ��
    [SerializeField]
    private int minFontSize;                    //��Ʈ�� �ּ� ũ��

    private void Awake()
    {
        endOffCountDown = new CountDownEvent();
        textCountDown = GetComponent<TextMeshProUGUI>();
        audionSource = GetComponent<AudioSource>();
    }
    public void StartCountDown(UnityAction action, int start = 3 , int end = 1)
    {
        //StartCoroutine
    }
}
