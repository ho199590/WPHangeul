using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnviromentMove : MonoBehaviour
{
    [SerializeField]
    Transform[] target;

    private void Start()
    {
        foreach (Transform t in target)
        {
            t.DOMove(new Vector3(t.position.x, t.position.y - 1f, t.position.z), 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}
