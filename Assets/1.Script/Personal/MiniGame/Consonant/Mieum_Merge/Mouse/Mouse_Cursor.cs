using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Cursor : MonoBehaviour
{
    //3D마우스 커서 
    [SerializeField]
    GameObject cursor;

    private void Update()
    {
        //마우스 좌표를 받아온다.
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
        //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
        cursor.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
