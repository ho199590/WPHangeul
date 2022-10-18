using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropHandle : MonoBehaviour
{
    private float z_saved;
    private Vector3 posi;

    public event System.Action<bool, GameObject> QuizCheck2;
    Vector3 GetMouseWorldPosition()
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
        GetComponent<Rigidbody>().useGravity = true;
        QuizCheck2?.Invoke(true, gameObject);
    }
}
