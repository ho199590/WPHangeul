using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float startTime = 0;//Scene�� ���۵ǰ� �����Ŀ� �������� ����
    public float minX, maxX; //x������ �̵��� ��𼭺��� ������ ���� ���� 
    public float minY, maxY; //y������ �̵��� ��𼭺��� ������ ���� ����
    public float speedX; //x�� �̵� �ӵ�
    public float speedY; //y�� �̵� �ӵ�

    [SerializeField]
    int sign1; //��� ���⿡�� �������� ���� üũ
    private int sign2 = -1;
    private void FixedUpdate()
    {
        if(Time.time > startTime)
        {
            transform.position += new Vector3(speedX * Time.deltaTime * sign1, speedY * Time.deltaTime * sign2, 0);
        }
    }
}
