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
    private void Start()
    {
        StartCoroutine(PoleRotate());
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
