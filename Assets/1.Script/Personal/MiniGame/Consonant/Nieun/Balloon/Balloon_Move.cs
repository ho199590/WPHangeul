using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Move : MonoBehaviour
{
    public float startTime;

    public float minX, maxX;

    public float minY, maxY;
    [Range(1,100)]
    public float speedX;

    [Range(0, 100)]
    public int speedY;


    private int sign2 = -1;
    //ǳ�� ȸ��
    public float turnSpeed = 10;
    [SerializeField]
    GameObject Balloon;//ǳ��
    [SerializeField]
    GameObject Animal;//����
    [SerializeField]
    int sign1;

    private void FixedUpdate()
    {
        if (Time.time >= startTime)
        {
            //�̵� ���� ó��.
            transform.position += new Vector3(speedX * Time.deltaTime * sign1, speedY * Time.deltaTime * sign2, speedY * Time.deltaTime * sign2);
            if (transform.position.x <= minX || transform.position.x >= maxX)
            {
                sign1 *= -1;//��ȣ����
            }
            if (transform.position.y <= minY || transform.position.y >= maxY)
            {
                sign2 *= -1;
            }
        }
        //ǳ�� ȸ�� 
        Balloon.transform.Rotate(new Vector3(0,turnSpeed * Time.deltaTime, 0));
        Animal.transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
    }
}
