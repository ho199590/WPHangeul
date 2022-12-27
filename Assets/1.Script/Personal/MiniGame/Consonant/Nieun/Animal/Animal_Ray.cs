using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Ray : MonoBehaviour
{
    [SerializeField]
    Transform dropPosition;
    Ray ray = new();
    private void OnEnable()
    {
        dropPosition.gameObject.SetActive(true);
    }
    private void Update()
    {
        //자신의 위치를 저장한다(origin : 자기자신 시작점)
        ray.origin = transform.position;
        //도착지점 위치를 저장한다(direction : 끝지점 도착점)
        ray.direction = -transform.up;
        //레이를 쏴서 맞은 지점 위치를 dropPosition 위치로 저장한다.
        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            dropPosition.position = hitinfo.point;
        }
    }
}
