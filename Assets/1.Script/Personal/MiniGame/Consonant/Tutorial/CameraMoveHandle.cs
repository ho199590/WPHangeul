using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveHandle : MonoBehaviour
{
    [SerializeField]
    Transform camPosi;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    GameObject mask;
    Vector3 startposition;

    float lerpTime;
    float currentTime;
    float move;
    void Start()
    {
        StartCoroutine(Move2());   
    }
    IEnumerator Move2()
    {
        GetComponent<Camera>().depth = 2;
        startposition = transform.position;
        lerpTime = 10f;
        currentTime = 0;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            transform.position = Vector3.Lerp(startposition, camPosi.position, move);
            transform.position = Vector3.Lerp(startposition, camPosi.position, curve.Evaluate(currentTime / lerpTime));
            yield return null;
        }
        mask.SetActive(true);
    }
}
