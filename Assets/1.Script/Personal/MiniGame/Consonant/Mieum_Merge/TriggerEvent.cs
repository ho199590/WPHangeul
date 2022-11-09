using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TriggerEvent : MonoBehaviour
{
/*    private void OnTriggerEnter(Collider other)
    {
        print("왜 안되냐고");
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        print("왜 안되냐고");
    }
}
