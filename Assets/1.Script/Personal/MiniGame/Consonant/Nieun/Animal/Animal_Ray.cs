using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//µ¿¹° Ray
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
        ray.origin = transform.position;
        ray.direction = -transform.up;
        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            dropPosition.position = hitinfo.point;
        }
    }
}
