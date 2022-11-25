using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class IntroText : MonoBehaviour
{
    [SerializeField]         //text 변수
    Text text;
    [SerializeField]
    string[] talk;          //인스펙터에서 대화 수정 할수있게 하고 배열로 선언
    [SerializeField]
    GameObject image;       //Image GameObject
    int talkIndex = 0; //배열 증감 변수
    bool imageTouch = true;  //대화 도중 터치 못하게 막기
    private void Start()
    {
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
            }
            else
            StartCoroutine(OnType());
        }

    }

}
