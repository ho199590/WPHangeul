using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//마우스포인트따라 오브젝트 움직이기
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z값 부여용
    private Vector3 posi;

    public event System.Action<bool, GameObject> QuizCheck2; ////두번째 퀴즈 맞췄을 때 발생할 이벤트
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

    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
    }
    private void OnMouseUp()
    {
        
        QuizCheck2?.Invoke(true, gameObject);

    }
}
