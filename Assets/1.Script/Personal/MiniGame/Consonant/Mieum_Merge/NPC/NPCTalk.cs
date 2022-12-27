using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Permissions;
//게임 첫 대화 기능
public class NPCTalk : MonoBehaviour
{
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
    GameObject npcBalloon;  //Npc말풍선
    SpeakerHandler speaker; //스피커 변수
    private void Awake()
    {
        speaker = FindObjectOfType<SpeakerHandler>();
    }
    private void OnMouseDown()
    {
        if (imageTouch)
        {
            if (talk.Length == talkIndex)//대화가 다끝났다면 Npc를 꺼준다
            {
                Npc();
                NpcMove();
                CameraMove.CameraEvents();//카메라 이동 함수 호출
            }
            else
            {
                StartCoroutine(OnType());//대화가 끝나지 않았다면 다음 대화로 넘어간다.
            }
        }
        speaker.SoundByNum2(3);
    }
    //대화창 끄기
    private void Npc()
    {
        talkImage.SetActive(false);
        animalOb.SetActive(true);  
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
    //Npc이동 Dotween
    private void NpcMove()
    {
        Vector3 pos = new Vector3(-1813.252f, 278.0504f, -9733.855f);
        Vector3 ros = new Vector3(-15.019f, -206.942f, 2.116f);
        Vector3 scale = new Vector3(1000f, 1000f, 1000f);
        npc.transform.DOLocalMove(pos, 3f);
        npc.transform.DOLocalRotate(ros, 3f, RotateMode.Fast);
        npc.transform.DOScale(scale, 3f);
    }
}
