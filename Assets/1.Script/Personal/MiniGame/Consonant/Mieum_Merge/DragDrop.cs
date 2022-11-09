using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*using Rito.MouseEvents;*/
//https://rito15.github.io/posts/unity-toy-custom-mouse-events/
public class DragDrop : MonoBehaviour
{
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
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,20);
        //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    //오브젝트 드래그 끝날시
    private void OnMouseUp()
    {
/*        print("OnMouseUp");*/
    }
    private void OnMouseOver()
    {
        print("OnMouseOver");
    }
}
