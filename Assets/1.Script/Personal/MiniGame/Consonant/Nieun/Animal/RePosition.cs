using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true변수
    [SerializeField]
    string Answer;//어디에 닿았는지 확인하기 위한 변수
    [SerializeField]
    GameObject bollon;//bollon 오브젝트
    [SerializeField]
    GameObject AnimalOb;//animal 오브젝트
    [SerializeField]
    ScoreHandler scoreCase; //미션완료시 공통 별 프리팹 
    Vector3 bollonPosi;
    Vector3 animal;//animal positon
    Vector3 perentsPosition;
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;//현재풍선 위치를 저장
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;
    }
    public void ReMove()//틀린애만 ReMove 시켜야함 
    {
        AnimalOb.gameObject.GetComponent<Collider>().isTrigger = true;
        AnimalOb.gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        AnimalOb.transform.position = animal;//원래 위치로 전환
        isMove.enabled = true;
        AnimalOb.GetComponent<Collider>().enabled = false;
        bollon.transform.position = bollonPosi;//풍선 원위치 전환
        bollon.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("정답");
            scoreCase.SetScore();
        }
        else
        {
            print("틀림");
            ReMove();
        }
    }
}
/*    [SerializeField]
    GameObject AnimalOb;
    [SerializeField]
    GameObject balloonOb;*/
/* Animal2.GetComponent<Collider>().isTrigger = true;
        AnimalOb.GetComponent<Rigidbody>().useGravity = false;
        AnimalOb.GetComponent<Rigidbody>().isKinematic = false;
        AnimalOb.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);*/
