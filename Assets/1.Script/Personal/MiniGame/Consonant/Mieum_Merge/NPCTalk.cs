using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCTalk : MonoBehaviour
{
    //카메라 
    [SerializeField]
    Camera m_Camera;        //Talk가 끝나고 일어날 이벤트의 카메라 변수 
    //Text Effect
    [SerializeField]         //text 변수
    Text text;
    [SerializeField]
    string[] talk;          //인스펙터에서 대화 수정 할수있게 하고 배열로 선언
    [SerializeField]
    GameObject talkImage;   //대화가 끝나고 비활성화 해야하기 때문에 선언
    [SerializeField]
    GameObject npc;    //대화가 끝나고 비활성화 해야하기 때문
    int talkIndex = 0; //배열 증감 변수
    bool imageTouch = true;  //대화 도중 터치 못하게 막기
    //Npc와 대화가 끝나면 오브젝트 활성화
    [SerializeField]
    GameObject animalOb;
    [SerializeField]
    GameObject animalObTwo;
    private void OnMouseDown()
    {
        if (imageTouch)
        {
            if (talk.Length == talkIndex)//대화가 다끝났다면 Npc를 꺼준다
            {
                NpcOf();
            }
            else
            {
                StartCoroutine(OnType());//대화가 끝나지 않았다면 다음 대화로 넘어간다.
            }
        }
    }
    //대화창 끄기
    private void NpcOf()
    {
        talkImage.SetActive(false);
        npc.SetActive(false);
        animalOb.SetActive(true);
        animalObTwo.SetActive(true);    
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
        yield return new WaitForSeconds(1f);
        imageTouch = true;
        yield break;
    }
}
