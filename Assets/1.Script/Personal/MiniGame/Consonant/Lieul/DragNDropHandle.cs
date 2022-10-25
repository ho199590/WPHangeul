using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���콺����Ʈ���� ������Ʈ drag&drop���� �����̰�
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z�� �ο���
    private Vector3 posi;
    Collider collider;
    [SerializeField]
    Collider lieulPosi;
    bool check = false;
    [SerializeField]
    GameObject active;
  
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    Vector3 GetMouseWorldPosition() //���콺����Ʈ�� ������ǥ�� �ο���
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
        collider = CheckOb();
        if(collider != null && collider.gameObject == lieulPosi.gameObject)
        {
            print("�浹üũȮ��");
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
            print("�浹ó�� ����" + hit.collider);
            return hit.collider;
        }
        else return null;
    }
}
