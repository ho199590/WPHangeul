using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true변수
    Vector3 animal;//animal positon
    [SerializeField]
    GameObject bollonPosition;

    Vector3 bollonPosi;
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollonPosition.transform.position;//현재풍선 위치를 저장
        animal = this.transform.position;
    }
    public void ReMove()
    {  
        isMove.enabled = true;
        this.transform.position = animal;//원래 위치로 전환
        bollonPosition.transform.position = bollonPosi;//풍선 원위치 전환
    }
}/*    [SerializeField]
    GameObject AnimalOb;
    [SerializeField]
    GameObject balloonOb;*/
/* Animal2.GetComponent<Collider>().isTrigger = true;
        AnimalOb.GetComponent<Rigidbody>().useGravity = false;
        AnimalOb.GetComponent<Rigidbody>().isKinematic = false;
        AnimalOb.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);*/
