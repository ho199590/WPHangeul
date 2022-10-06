using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterGizemo : MonoBehaviour
{
    public Vector3 center;
    public float radius;


    private void OnDrawGizmosSelected()
    {
        center = transform.position;

        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawSphere(center, radius);
    }
}
