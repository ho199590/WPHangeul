using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://intrepidgeeks.com/tutorial/rotation-that-can-be-used-in-the-game-is-made-in-unity-quaternion#!
//����ǥ ȸ����
public class PoleRotationHandle : MonoBehaviour
{
    float lerpTime; //Lerp�� ���������� �� �ҿ�ð�
    float currentTime; //��ӿ�� ���� ��ŸŸ�� �����
    Quaternion startposition; //Lerp���� ����� ������ġ ���������� ù ��ġ�� ��Ƶ� ����

    [SerializeField]
    int direction; //����ǥ�� �����ص� ������ �ε����� �޾ƿ���
    int[] directions = { 180, 270, 180, 90}; //���渶�� �ִ� ����ǥ�� ȸ���ؾߵ� ���� �����

    private void Start()
    {
        StartCoroutine(Pole(direction));
    }
    IEnumerator Pole(int index)
    {
        lerpTime = 0.5f;
        currentTime = 0;
        startposition = transform.rotation;
        while ((currentTime / lerpTime) < 1) //Lerp�� ���
        {
            currentTime += Time.deltaTime;
            //Vector3.up�� ��, y�� �������� ���� ������ŭ ȸ��
            transform.rotation = Quaternion.Slerp(startposition, Quaternion.AngleAxis(directions[index], Vector3.up), currentTime / lerpTime); 
            yield return null;
        }
    }
}
