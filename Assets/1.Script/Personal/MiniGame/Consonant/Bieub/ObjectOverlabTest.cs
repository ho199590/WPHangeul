using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOverlabTest : MonoBehaviour
{
    [SerializeField]
    int Index;
    bool OverlabCheck = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ObjectOverlabTest>())
        {
            if(other.gameObject.GetComponent<ObjectOverlabTest>().Index > Index)
            {
                OverlabCheck = true;
            }
            else
            {
                OverlabCheck = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OverlabCheck)
            {
                print("���� �ε���" + gameObject.name);
            }
            else
            {
                print("ū �ε���" + gameObject.name);
            }
        }
    }
}
