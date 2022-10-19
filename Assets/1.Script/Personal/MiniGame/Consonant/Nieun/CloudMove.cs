using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float startTime = 1f;//Scene�� ���۵ǰ� �����Ŀ� �������� ����
    public float minX, maxX; //x������ �̵��� ��𼭺��� ������ ���� ���� 
    public float minY, maxY; //y������ �̵��� ��𼭺��� ������ ���� ����
    public float speedX; //x�� �̵� �ӵ�
    public float speedY; //y�� �̵� �ӵ�

    [SerializeField]
    int sign1; //��� ���⿡�� �������� ���� üũ
    private int sign2 = -1;
    //������ �ð���ŭ ������ ������
    private void FixedUpdate()
    {
        if (Time.time >= startTime)
        {
            transform.position += new Vector3(speedX * Time.deltaTime * sign1, speedY * Time.deltaTime * sign2, 0);
            if (transform.position.x <= minX || transform.position.x >= maxX)
            {
                sign1 *= -1;//��ȣ����
            }
            if (transform.position.y <= minY || transform.position.y >= maxY)
            {
                sign2 *= -1;
            }
        }

    }
}
