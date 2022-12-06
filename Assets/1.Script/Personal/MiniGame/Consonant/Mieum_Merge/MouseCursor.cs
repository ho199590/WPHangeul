using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    GameObject magic;
    [SerializeField]
    Camera m_Camera;       //Canvas Camera����
    private void Start()
    {
        //Cursor.visible = false;
    }
    void Update()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z; //World ��ǥ�� ��ũ�� ��ǥ�� ��ȯ , z��ǥ�� ī�޶��� ǥ����ġ 
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);             //ScreenToWorldPoint�� �ٽ� ��ũ���� ���콺 ��ǥ�� ������Ʈ�� ��ǥ�� ��ȯ.
        objPos.y = 3f;//y�� ����
        magic.transform.position = objPos;
    }
}
