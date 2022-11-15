using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    Transform[] ob;

    private void Start()
    {

        ob = GameObject.Find("Point").GetComponentsInChildren<Transform>();
    }
}
