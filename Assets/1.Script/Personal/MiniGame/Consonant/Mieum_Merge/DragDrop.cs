using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*using Rito.MouseEvents;*/
//https://rito15.github.io/posts/unity-toy-custom-mouse-events/
public class DragDrop : MonoBehaviour
{
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
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,20);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    //������Ʈ �巡�� ������
    private void OnMouseUp()
    {
/*        print("OnMouseUp");*/
    }
    private void OnMouseOver()
    {
        print("OnMouseOver");
    }
}
