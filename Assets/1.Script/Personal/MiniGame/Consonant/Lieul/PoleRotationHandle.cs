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
    float lerpTime; //Lerp�� ���������� �� �ҿ�ð�
    float currentTime; //��ӿ�� ���� ��ŸŸ�� �����
    //Vector3 startposition; //Lerp���� ����� ������ġ ���������� ù ��ġ�� ��Ƶ� ����
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
        while ((currentTime / lerpTime) < 1) //Lerp�� ���
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
