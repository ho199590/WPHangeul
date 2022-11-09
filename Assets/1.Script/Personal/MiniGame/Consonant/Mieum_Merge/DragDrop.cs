using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*using Rito.MouseEvents;*/
//https://rito15.github.io/posts/unity-toy-custom-mouse-events/
public class DragDrop : MonoBehaviour
{
    [SerializeField]
    GameObject hangleOb;

    private Vector3 dePosition;
    private void Awake()
    {
        //������ġ �� ���� 
        dePosition = transform.position;
    }
    //Mouse Event
    //������Ʈ Ŭ����
    private void OnMouseDown()
    {
/*        print("OnMouseDown");*/
    }
    //������Ʈ �巡�� ���϶�
    private void OnMouseDrag()
    {
/*        print("OnMouseDrag");*/
        //���콺 ��ǥ�� �޾ƿ´�.
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,100);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    //������Ʈ �巡�� ������
    private void OnMouseUp()
    {
/*        print("OnMouseUp");*/
        //����ġ�� ���ư�
        StartCoroutine(ResetPosition());
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("�浹������");
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
