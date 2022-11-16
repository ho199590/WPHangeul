using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lerp로 등장할때 움직임 
public class AppearHandle : MonoBehaviour
{
    float lerpTime = 2f; //등장시 소요시간
    float currentTime = 0;
    Vector3 start;
    [SerializeField]
    Transform arrive; //도착지점
    void Start()
    {
        start = transform.position;
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        while (currentTime / lerpTime < 1)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, arrive.position, currentTime / lerpTime);
            yield return null;
        }
    }
}
