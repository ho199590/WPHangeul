using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Test : MonoBehaviour
{
    [SerializeField]
    GameObject Cube;


    private void Start()
    {
    }
    private void Update()
    {
      transform.position = Vector3.Lerp(transform.position, Cube.transform.position, 0.1f * Time.deltaTime);
    }
}
