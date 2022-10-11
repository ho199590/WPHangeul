using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishController : MonoBehaviour
{
    #region 변수
    FlotsamController controller;
    List<Transform> Point = new List<Transform>();

    Rigidbody rb;

    bool canMove = false;
    public bool Knock = false;    
    public int index;    

    int nextIndex;

    float timer = 0;
    #endregion
    #region 함수
    private void OnEnable()
    {
        controller = FindObjectOfType<FlotsamController>();

        Point = controller.wayPoint;        

        if (index == Point.Count - 1)
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
        if(index == Point.Count)
        {
            index = 0;
        }
        if (index == Point.Count - 1)
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
        canMove = false;
        transform.DOMove(Point[nextIndex].position, 2f).From(transform.position).SetEase(Ease.Linear).OnComplete(() => SettingIndex());
        transform.DOLookAt(Point[nextIndex].position, 2f).OnComplete(() => MoveNext()) ;
        transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    private void OnMouseDown()
    {
        transform.DOKill();
        transform.GetComponentInChildren<SpriteRenderer>().enabled = false;        
    }

    private void OnMouseOver()
    {
        timer = 0;
    }

    private void OnMouseUp()
    {
        
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().angularDrag = 0;

        timer = 0;
        canMove = true;
    }

    private void Update()
    {
        if (!Knock)
        {
            timer += Time.deltaTime;
        }
        if(timer > 50)
        {
            if (canMove)
            {
                MoveNext();
                timer = 0;
            }
        }
    }
    #endregion
}
