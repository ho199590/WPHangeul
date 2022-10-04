using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Vector3 balloonPosition;//풍선 위치값
    [SerializeField]
    Vector3 animalPosition;//오브젝트 위치값
    [SerializeField]
    GameObject balloonOb;
    Rigidbody rb;//iskinematic
    Collider col;//IsTrigger
    Balloon_Move isMove;//BalloonMove true변수

    private void Start()
    {
        animalPosition = this.transform.position;//동물 위치 저장
        balloonPosition = this.transform.position;//풍성 위치 저장
        isMove = GetComponentInParent<Balloon_Move>();
        rb = GetComponent<Rigidbody>();
        rb = GetComponentInChildren<Rigidbody>();
        col = GetComponent<Collider>();
    }
    public void ReMove()
    {
        col.isTrigger = true;
        this.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);
        isMove.enabled = true;
        rb.useGravity = false;
        rb.isKinematic = false;
        print("ReMove실행");
    }
}
