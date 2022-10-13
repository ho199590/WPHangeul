using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//���콺 Ŭ������ ������Ʈ�� Ŭ���ϴ� ��ũ��Ʈ
public class ObjectDetector : MonoBehaviour
{
    [System.Serializable]
    public class RaycastEvent : UnityEvent<Transform> { }  //�̺�Ʈ Ŭ���� ���� , ��ϵǴ� �̺�Ʈ �޼ҵ�� Transform�Ű������� 1���� ������ �޼ҵ�


    [HideInInspector]
    public RaycastEvent raycastEvent = new RaycastEvent();  //�̺�Ʈ Ŭ���� �ν��Ͻ� ������ �޸� �Ҵ�

    private Camera mainCamera;  //������ �����ϱ����� Camera 
    private Ray ray;            //������ ���� ���� ������ ���� ray
    private RaycastHit hit;     //������ �ε��� ������Ʈ ���� ������ ���� Raycasthit


    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ�  ���� ����
            // ray.origin : ������ ������ġ( = ī�޶���ġ)
            //ray.direction :������ �������
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                //�ε��� ������Ʈ�� Transform ������ �Ű������� �̺�Ʈ ȣ��
                raycastEvent.Invoke(hit.transform);
            }
        }



    }
}
