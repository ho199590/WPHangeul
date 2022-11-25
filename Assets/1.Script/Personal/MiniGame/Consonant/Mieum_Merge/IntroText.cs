using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class IntroText : MonoBehaviour
{
    [SerializeField]         //text 변수
    Text text;
    [SerializeField]
    string[] talk;          //인스펙터에서 대화 수정 할수있게 하고 배열로 선언
    [SerializeField]
    GameObject image , introMap;       //Image GameObject
    BoxCollider2D boxCollider;
    [SerializeField]
    GameObject[] ob;
    int talkIndex = 0; //배열 증감 변수
    bool imageTouch = true;  //대화 도중 터치 못하게 막기
    public static Action PlusIndex; 
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        PlusIndex = () => { UpIndex(); };
        StartCoroutine(OnType());//대화가 끝나지 않았다면 다음 대화로 넘어간다
    }

    IEnumerator OnType()
    {
        text.text = " ";
        imageTouch = false;
        foreach (char item in talk[talkIndex++])
        {
            text.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        imageTouch = true;
        yield return new WaitForSeconds(1f);
        yield break;
    }
    private void OnMouseDown()
    {
        if(imageTouch)
        {
            if (talk.Length == talkIndex)//대화가 다끝났다면
            {
                CameraMove.CameraReset();//카메라 이동
                image.SetActive(false);
                introMap.SetActive(false);
            }
            else
            StartCoroutine(OnType());
        }
    }
    private void Update()
    {
        if(talkIndex==4 || talkIndex == 6)
        {
            if(talkIndex == 4)
            {
                ob[0].SetActive(true);
            }
            if (talkIndex == 6)
            {
                ob[1].SetActive(true);
            }
            boxCollider.enabled = false;
        }
    }
    public void UpIndex()
    {
        StartCoroutine(OnType());
        boxCollider.enabled = true; 
    }
}
