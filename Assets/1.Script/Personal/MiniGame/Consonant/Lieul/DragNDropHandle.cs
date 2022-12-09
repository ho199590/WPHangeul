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
    GameObject crashLieul;
    [SerializeField]
    GameObject resetLieul;
    [SerializeField]
    GameObject fakeActive;
    Vector3 originPosi;
    private void OnEnable()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        originPosi = transform.position;
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
        crashLieul.SetActive(true);
        GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
    }
    //3��° ����Ʈ�� //�巡�׹����� ����� �� ����ũ�� �� ���� ������Ʈ �����״��ؼ� �巡�׹��� ������ �������� �ϱ�
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Block"))
        {
            print("���Զ� �浹�ߴ�");
            resetLieul.SetActive(true);
            gameObject.transform.position = originPosi;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            FindObjectOfType<QuizManager>().AddNRemove = gameObject;
            gameObject.SetActive(false);       
        }
    }
    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().useGravity = true;
        collider = CheckOb();
        if (collider != null && collider.gameObject == crashLieul)
        {
            print("�浹üũȮ��");
            gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
            fakeActive.SetActive(true);
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
