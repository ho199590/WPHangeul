using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//마우스포인트따라 오브젝트 drag&drop으로 움직이게
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z값 부여용
    private Vector3 posi;
    Collider collider;
    [SerializeField]
    GameObject lieulPosi;
    bool check = false;
    [SerializeField]
    GameObject active;
    Vector3 originPosi;
  
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        originPosi = transform.position;
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
        if (GetComponent<Rigidbody>()) 
        { 
            //GetComponent<Rigidbody>().useGravity = false;
            //GetComponent<Rigidbody>().isKinematic = true;
        }
        lieulPosi.SetActive(true);
        GetComponent<Rigidbody>().useGravity = false;

    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
        Debug.DrawRay(transform.position, transform.right * 5, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Cube")
        {
            print("벽이랑 직충돌했다");
            gameObject.SetActive(false);
            lieulPosi.SetActive(false);
            //GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().useGravity = true;
        collider = CheckOb();
        if (collider != null && collider.gameObject == lieulPosi)
        {
            print("충돌체크확인");
            gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
            active.SetActive(true);
            FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        }
    }
    Collider CheckOb()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, 0.1f, out RaycastHit hit))
        {
            print("충돌처리 감지" + hit.collider);
            return hit.collider;
        }
        else return null;
    }
}
