using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TriggerEvent : MonoBehaviour
{
/*    private void OnTriggerEnter(Collider other)
    {
        print("�� �ȵǳİ�");
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        print("�� �ȵǳİ�");
    }
}
