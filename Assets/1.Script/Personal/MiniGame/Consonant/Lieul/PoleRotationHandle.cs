using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ǥ ȸ����
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
            print("ȸ���ڷ�ƾüũ"+transform.rotation.eulerAngles.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 5);
            yield return new WaitForSeconds(Time.deltaTime);
            if(transform.rotation.eulerAngles.y <= 180)
            {
                print("�극��ũüũ");
                break;
            }
        }
        transform.rotation = Quaternion.Euler(0,180,0);
        yield break;
    }
}
