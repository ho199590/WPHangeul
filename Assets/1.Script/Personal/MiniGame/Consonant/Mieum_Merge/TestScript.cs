using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    GameObject magic;
    [SerializeField]
    Camera m_Camera;       //Canvas Cameraº¯¼ö
    void Update()
    {
        float distance = m_Camera.ScreenToWorldPoint(transform.position).z;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 20f, distance);
        Vector3 objPos = m_Camera.ScreenToWorldPoint(mousePos);
        magic.transform.position = objPos;
    }
}
