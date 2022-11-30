using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//이정표 회전용
public class PoleRotationHandle : MonoBehaviour
{
    //private void Update()
    //{
    //    transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 500);
    //}
    float lerpTime; //Lerp의 도착때까지 총 소요시간
    float currentTime; //등속운동을 위한 델타타임 저장용
    //Vector3 startposition; //Lerp에서 사용할 시작위치 고정용으로 첫 위치를 담아둘 변수
    Quaternion startposition;
    private void Start()
    {
        //StartCoroutine(PoleRotate());
        StartCoroutine(Pole());
    }
    IEnumerator Pole()
    {
        lerpTime = 0.5f;
        currentTime = 0;
        startposition = transform.rotation;
        while ((currentTime / lerpTime) < 1) //Lerp의 등속
        {
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startposition, Quaternion.identity, currentTime / lerpTime);
            yield return null;
        }
    }
    IEnumerator PoleRotate()
    {
        while(transform.rotation.eulerAngles.y > 180)
        {
            print("회전코루틴체크"+transform.rotation.eulerAngles.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 5);
            yield return new WaitForSeconds(Time.deltaTime);
            if(transform.rotation.eulerAngles.y <= 180)
            {
                print("브레이크체크");
                break;
            }
        }
        transform.rotation = Quaternion.Euler(0,180,0);
        yield break;
    }
}
