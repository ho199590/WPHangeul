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
        //기존위치 값 저장 
        dePosition = transform.position;
    }
    //Mouse Event
    //오브젝트 클릭시
    private void OnMouseDown()
    {
/*        print("OnMouseDown");*/
    }
    //오브젝트 드래그 중일때
    private void OnMouseDrag()
    {
/*        print("OnMouseDrag");*/
        //마우스 좌표를 받아온다.
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,100);
        //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    //오브젝트 드래그 끝날시
    private void OnMouseUp()
    {
/*        print("OnMouseUp");*/
        //원위치로 돌아감
        StartCoroutine(ResetPosition());
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("충돌했을때");
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
