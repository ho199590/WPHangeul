using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;                  //�̵��ӵ�
    private Vector3 moveDirection = Vector3.zero; //�̵�����

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;//������Ʈ�� �̵���Ų��.
    }
    public void MoveTo(Vector3 direction)       //�̵������� ����
    {
        moveDirection = direction;
    }
}
