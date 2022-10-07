using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishController : MonoBehaviour
{
    #region 변수
    FlotsamController controller;
    Transform[] Point;

    Rigidbody rb;

    public bool Knock = false;
    
    int index;    
    int nextIndex;
    #endregion
    #region 함수
    private void OnEnable()
    {
        controller = FindObjectOfType<FlotsamController>();
        Point = controller.wayPoint;
        index = transform.GetSiblingIndex();
        if (index == Point.Length - 1)
        {
            nextIndex = 0;
        }
        else
        {
            nextIndex = index + 1;
        }
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
        MoveNext();
    }

    public void SettingIndex()
    {
        index++;
        if(index == Point.Length)
        {
            index = 0;
        }
        if (index == Point.Length - 1)
        {
            nextIndex = 0;
        }
        else
        {   
            nextIndex = index + 1;
        }
    }

    public void MoveNext()
    {
        transform.DOMove(Point[nextIndex].position, 2f).From(Point[index].position).SetEase(Ease.Linear).OnComplete(() => SettingIndex());
        transform.DOLookAt(Point[nextIndex].position, 2f).OnComplete(() => MoveNext()) ;
    }

    private void OnMouseDown()
    {
        Vector3 Point = controller.TargetPoint.position;
        Point += new Vector3(Random.Range(-10, 10) / 10.0f, Random.Range(-10, 01) / 10.0f, Random.Range(-10, 10) / 10.0f);

        transform.DOKill();
        transform.GetComponent<Collider>().enabled = false;
        transform.DOJump(Point, 4f, 1, 2f).OnComplete(() => { rb.useGravity = true; transform.GetComponent<Collider>().enabled = true; GetComponent<Collider>().isTrigger = false; });
    }
    #endregion
}
