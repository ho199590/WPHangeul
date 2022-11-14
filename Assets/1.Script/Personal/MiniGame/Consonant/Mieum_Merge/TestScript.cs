using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    private void Start()
    {
        StartCoroutine(CubeMove());
    }
    IEnumerator CubeMove()
    {
        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
            if(Vector3.Distance(transform.position , target.transform.position) <= 1f)
            {
                print("Á¾·á");
                yield break;
            }
        }
    }
}
