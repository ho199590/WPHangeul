using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Animation_Controller : MonoBehaviour
{   
    //이벤트 함수 등록
    public static Action plusNum;
    //정답을 맞출경우 static 변수 선언
    private static int index = 0;
    //SerializeField로 choice.Number로 몇번째 동물인지 확인
    [SerializeField]
    Random_AnimalChoice choice;
    private void Start()
    {
        //animalchoice 스크립트 초기화
        choice = GetComponent<Random_AnimalChoice>();
    }
    //Index 증가 함수
    public void IndexNum()
    {
        Index++;
    }
    //Property
    public int Index
    {
        get => index;
        set
        {
            index = value;
            if (index >= 5)
            {
                AnimalDance();
            }
        }
    }
    //미션 클리어시 동물 춤 애니메이션 수정
    public void AnimalDance()
    {
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Jump");
        transform.GetChild(choice.number).GetComponent<Animator>().Play("Eyes_Happy");
    }
}
