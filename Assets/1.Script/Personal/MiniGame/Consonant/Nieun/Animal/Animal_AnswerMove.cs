using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_AnswerMove : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//����
    private float startTime = 1f;
    [Range(1, 100)]
    public float speedX;
    [Range(0, 100)]
    public int speedZ;
    public float minX, maxX;
    public float minZ, maxZ;
    [SerializeField]
    int sign1;
    private int sign2 = -1;
  
    private void FixedUpdate()
    {
        
        if (Time.time >= startTime)
        {
            //�̵� ���� ó��
            transform.position += new Vector3(speedX * Time.deltaTime * sign1, 0, speedZ * Time.deltaTime * sign1);
            if (transform.position.x <= minX || transform.position.x >= maxX)
            {
                sign1 *= -1;//��ȣ����
            }
            if (transform.position.y <= minZ || transform.position.y >= maxZ)
            {
                sign2 *= -1;
            }
        }
    }
    
}
