using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour
{
    [SerializeField]
    GameObject hangleOb;
    private Vector3 dePosition;
    private void Awake()
    {
        //������ġ �� ���� 
        dePosition = transform.position;
        //���콺 Ŀ�� ���� 
    }
    //������Ʈ �巡�� ���϶�
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,100);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        StartCoroutine(ResetPosition());
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.gameObject.SetActive(false);
        collision.gameObject.SetActive(false);
        hangleOb.SetActive(true);
        hangleOb.transform.position = collision.transform.position;
    }
    IEnumerator ResetPosition()
    {
        while(Vector3.Distance(transform.position , dePosition) >3)        
        {
            transform.position = Vector3.MoveTowards(transform.position, dePosition,
                Time.deltaTime * 50);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = dePosition;
        yield break;
    }
}
