using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IntroDrag : MonoBehaviour
{
    [SerializeField]
    Camera m_Camera;       //Canvas Camera����
    [SerializeField]
    GameObject answerOb;   //���� ���� 
    Vector3 dePosition;    //���� ��ġ ���� ����
    [SerializeField]
    GameObject particle;    //��ƼŬ ������Ʈ
    //���� ��ġ�� ���� 
    private void Start()
    {
        dePosition  = transform.position;
    }

    //Canvas �������� ī�޶� �巡�� , ���� �ȸ¾Ƽ� y�� - 20f �ع��� ��
    void OnMouseDrag()
    {
        float distance = m_Camera.ScreenToWorldPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 20f, distance);
        Vector3 objPos = m_Camera.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
    }
    //���콺 ���ϴ� ���� ����ġ
    private void OnMouseUp()
    {
        transform.position = dePosition;
    }
    //���� ������Ʈ���� ������ ���� ���� Ȱ��ȭ 
    private void OnTriggerEnter(Collider other)
    {
        if(transform.name == other.gameObject.name)
        {
            answerOb.SetActive(true);
            this.transform.GetComponent<Collider>().enabled = false;
            GameObject par = Instantiate(particle);
            par.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            par.transform.position = new Vector3(answerOb.transform.position.x,answerOb.transform.position.y + 0.5f,answerOb.transform.position.z);
            Destroy(par, 1.5f);
            IntroText.PlusIndex();
        }
    }
}
