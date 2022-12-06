using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    GameObject magic;
    [SerializeField]
    Camera m_Camera;       //Canvas Camera변수
    private void Start()
    {
        //Cursor.visible = false;
    }
    void Update()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; //World 좌표를 스크린 좌표로 전환 , z좌표가 카메라의 표준위치 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);             //ScreenToWorldPoint가 다시 스크린의 마우스 좌표를 오브젝트의 좌표로 전환.
        objPos.y = 3f;//y축 고정
        magic.transform.position = objPos;
    }
}
