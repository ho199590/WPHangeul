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
    private CountDownEvent endOffCountDown;     //카운트 다운 종료 후 외부 메소드 실행을 위해 이벤트 클래스 사용

    private TextMeshProUGUI textCountDown;      //카운트 다운 텍스트를 출력하는 Text UI
    private AudioSource audionSource;           //카운트 다운 사운드 재생

    [SerializeField]
    private int maxFontSize;                    //폰트의 최대 크기
    [SerializeField]
    private int minFontSize;                    //폰트의 최소 크기

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
