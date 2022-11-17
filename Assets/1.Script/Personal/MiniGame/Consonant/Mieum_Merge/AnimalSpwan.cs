using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimalSpwan : MonoBehaviour
{
    [SerializeField]
    GameObject[] animalOb;  //ai 몬스터 프리팹 저장 변수
    [SerializeField]
    int obCount;            //인스펙터 창에서 몬스터 갯수를 지정 할수있게 선언
    [SerializeField]
    GameObject particle;    //오브젝트 충돌시 생성되는 파티클 변수 
    int num = 1;            //몬스터 생성시 증감 변수
    bool maxMonster;        //몬스터 max치인지 확인 변수
    public static Action<Vector3> plusCount; //Vector이벤트 변수
    private void Start()
    {
        StartCoroutine(MonsterSpwan()); //게임 시작시 코루틴 시작
        plusCount = (Vector3 vec) =>
        {
            PlusCount(vec);
        };
    }
    IEnumerator MonsterSpwan()          //게임 시작시 ai몬스터 랜덤하게 생성
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[UnityEngine.Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);    //생성하고 5초 지연 
            if (num == obCount)                     //일정 숫자를 넘어가면 생성 제한
            {
                print("갯수제한");
                maxMonster = true;
                StopAllCoroutines();
            }
        }
    }
    //한번에 많이 생성되는 버그 수정 해야함.
    public void PlusCount(Vector3 vec) //오브젝트가 삭제되면 카운트 증가 , Collider other에서 전달한 위치값 vec
    {
        obCount += 2;
        GameObject effect = Instantiate(particle);  
        effect.transform.position = vec; //Collider other 위치값에 effect생성
        Destroy(effect,1f);
        if (num<obCount && maxMonster)
        {
            StartCoroutine(MonsterSpwan());
        }
        else
        {
            maxMonster = false;
        }
    }
}
