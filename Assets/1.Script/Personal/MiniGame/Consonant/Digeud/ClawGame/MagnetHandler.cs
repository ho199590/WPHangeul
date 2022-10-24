using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 자석 부분 스크립트
public class MagnetHandler : MonoBehaviour
{
    #region
    ClawController clawController;

    Rigidbody rb;
    BoxCollider collider;
    
    #endregion

    #region
    private void Start()
    {
        clawController = FindObjectOfType<ClawController>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name != "Stage")
        {   
            collision.transform.SetParent(transform);
        }

        clawController.MagnetCollisionInvoke();
    }
    #endregion
}

