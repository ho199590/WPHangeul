using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Drag : MonoBehaviour
{
    [SerializeField]
    GameObject Balloon;//움직일 풍선 오브젝트

    private void Start()
    {

    }
    private void OnMouseUp()
    {
        print("드래그중 아님");
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseDrag()
    {
        //마우스 좌표를 받아온다.
        Vector3 mousePosition
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 40);
        //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        Balloon.GetComponent<Rigidbody>().useGravity = false;
    }
}
