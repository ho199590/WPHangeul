using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Drag : MonoBehaviour
{
    [SerializeField]
    GameObject Balloon;//���߿� ���ٴϴ� ǳ�� ������Ʈ
    private void Start()
    {

    }
    private void OnMouseUp()
    {
        /*Random_Move.isMove = true;*/
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
    }
}
