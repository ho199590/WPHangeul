using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Ray : MonoBehaviour
{
    //particle ��ġ
    [SerializeField]
    Transform dropPosition;
    //Ray�� �����Ѵ�
    Ray ray = new();
    //�ѹ� Ȱ��ȭ�� �Ǿ�����
    private void OnEnable()
    {
        dropPosition.gameObject.SetActive(true);
    }
    private void Update()
    {
        //�ڽ��� ��ġ�� �����Ѵ�(origin : �ڱ��ڽ� ������)
        ray.origin = transform.position;
        //�������� ��ġ�� �����Ѵ�(direction : ������ ������)
        ray.direction = -transform.up;
        //���̸� ���� ���� ���� ��ġ�� dropPosition ��ġ�� �����Ѵ�.
        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            dropPosition.position = hitinfo.point;
        }
    }
}
