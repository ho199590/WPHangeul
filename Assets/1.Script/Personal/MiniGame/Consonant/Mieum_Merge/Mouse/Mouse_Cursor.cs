using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Cursor : MonoBehaviour
{
    //3D���콺 Ŀ�� 
    [SerializeField]
    GameObject cursor;

    private void Update()
    {
        //���콺 ��ǥ�� �޾ƿ´�.
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        cursor.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
