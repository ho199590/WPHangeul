using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Drag : MonoBehaviour
{
    [SerializeField]
    GameObject Balloon;//������ ǳ�� ������Ʈ

    private void Start()
    {

    }
    private void OnMouseUp()
    {
        print("�巡���� �ƴ�");
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseDrag()
    {
        //���콺 ��ǥ�� �޾ƿ´�.
        Vector3 mousePosition
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 40);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        Balloon.GetComponent<Rigidbody>().useGravity = false;
    }
}
