using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_temp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print($"�浹{collision}");
    }
    private void OnTriggerEnter(Collider other)
    {
        print($"Ʈ���� {other}");
    }
}
