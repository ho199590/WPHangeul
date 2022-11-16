using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class CameraMove : MonoBehaviour
{
    public static Action CameraEvents;
    //������ ������Ʈ 
    [SerializeField]
    GameObject recipe;      //������ ����
    bool b_recipe;          //�������� �Ұ�
    //ī�޶� 
    [SerializeField]
    Camera m_Camera;        //Talk�� ������ �Ͼ �̺�Ʈ�� ī�޶� ���� 
    [SerializeField]
    GameObject target;      //�ٶ� Ÿ��
    Image image;
    private void Awake()
    {
        CameraEvents = () =>
        {
            DoCamera();
        };
    }
    //ī�޶� �̵� 
    public void DoCamera()
    {
        m_Camera.transform.DOMove(target.transform.position, 5);
        b_recipe = true;
    }
    private void Update()
    {
        //���� ���� ���� 
        if(b_recipe && Vector3.Distance(m_Camera.transform.position , target.transform.position) <= 3f)
        {
            recipe.SetActive(true);
            recipe.transform.DOLocalMoveY(300 , 1).SetEase(Ease.Linear).SetLoops(1);
            b_recipe = false;
        }
    }
}
