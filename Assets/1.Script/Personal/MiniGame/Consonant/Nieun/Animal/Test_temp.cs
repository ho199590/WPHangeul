using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_temp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print($"충돌{collision}");
    }
    private void OnTriggerEnter(Collider other)
    {
        print($"트리거 {other}");
    }
}
