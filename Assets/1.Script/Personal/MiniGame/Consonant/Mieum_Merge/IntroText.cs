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
    GameObject ob1,ob2;          //레시피 변수
    int talkIndex = 0; //배열 증감 변수
    private void Start()
    {
        StartCoroutine(OnType());//대화가 끝나지 않았다면 다음 대화로 넘어간다.
    }

    IEnumerator OnType()
    {
        text.text = " ";
        foreach (char item in talk[talkIndex++])
        {
            text.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        yield break;
    }
    private void OnMouseDown()
    {
        StartCoroutine(OnType());
    }
    private void Update()
    {
        if (talkIndex == 4)
        {
            ob1.SetActive(true);
        }
        else if (talkIndex == 6)
        {
            ob2.SetActive(true);
        }
    }
}
