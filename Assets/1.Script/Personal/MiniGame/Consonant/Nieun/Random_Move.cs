using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Move : MonoBehaviour
{
    private Rigidbody cube;

    private void Start()
    {
        StartCoroutine(MoveObject());
    }
    IEnumerator MoveObject()
    {
        cube = GetComponent<Rigidbody>(); 
        while(true)
        {
            float dir1 = Random.Range(-0.5f, 0.5f);
            float dir2 = Random.Range(-0.5f, 0.5f);
            float dir3 = Random.Range(-0.5f, 0.5f);

            yield return new WaitForSeconds(2);
            cube.velocity = new Vector3(dir1, dir2, dir3);
        }
    }
}
