using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//마우스포인트따라 오브젝트 drag&drop으로 움직이게
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z값 부여용
    private Vector3 posi;
  
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    Vector3 GetMouseWorldPosition() //마우스포인트의 월드좌표값 부여용
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = z_saved;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDown()
    {
        z_saved = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        posi = gameObject.transform.position - GetMouseWorldPosition();
        if(GetComponent<Rigidbody>() == true) GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
    }
    private void OnMouseUp()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
}
