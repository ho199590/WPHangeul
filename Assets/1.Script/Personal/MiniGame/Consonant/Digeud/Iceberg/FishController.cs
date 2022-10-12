using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//����� ������
public class FishController : MonoBehaviour
{
    #region ����
    FlotsamController controller;
    List<Transform> Point = new List<Transform>();


    bool canMove = false;
    [SerializeField]
    bool knock;
    public bool Knock
    {
        get => knock;
        set
        {
            knock = value;
            StopAllCoroutines();
            if (!knock)
            {
                StartCoroutine(WaitMove());
            }
        }
    }
    
    int ind;
    public int Index
    {
        get => ind;
        set
        {
            ind = value % Point.Count;
            nextIndex = (Index + 1) % Point.Count;
            MoveNext();
        }
    }
    [SerializeField]
    int nextIndex;

    #endregion
    #region �Լ�
    private void Awake()
    {
        controller = FindObjectOfType<FlotsamController>();
        Point = controller.wayPoint;
    }
    private void OnEnable()
    {
        GetComponent<Collider>().isTrigger = true;
    }


    public void MoveNext()
    {   
        canMove = false;
        transform.DOMove(Point[nextIndex].position, 2f).From(transform.position).SetEase(Ease.Linear).OnComplete(() => Index++);
        transform.DOLookAt(Point[nextIndex].position, 2f);
        GetComponent<Collider>().isTrigger = true;
        transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    private void OnMouseDown()
    {
        transform.DOKill();
        transform.GetComponentInChildren<SpriteRenderer>().enabled = false;        
    }

    private void OnMouseUp()
    {
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().angularDrag = 0;
        canMove = true;
        Knock = false;
    }

    

    IEnumerator WaitMove()
    {
        print("�տ� ����");
        yield return new WaitForSeconds(3);
        if (canMove)
        {
            MoveNext();
            yield break;
        }
        yield break;
    }
    #endregion
}
