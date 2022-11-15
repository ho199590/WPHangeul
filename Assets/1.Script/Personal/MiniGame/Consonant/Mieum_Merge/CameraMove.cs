using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class CameraMove : MonoBehaviour
{
    public static Action CameraEvents;
    //레시피 오브젝트 
    [SerializeField]
    GameObject recipe;      //레시피 변수
    bool b_recipe;          //레시피형 불값
    //카메라 
    [SerializeField]
    Camera m_Camera;        //Talk가 끝나고 일어날 이벤트의 카메라 변수 
    [SerializeField]
    GameObject target;      //바라볼 타겟
    Image image;
    private void Awake()
    {
        CameraEvents = () =>
        {
            DoCamera();
        };
    }
    //카메라 이동 
    public void DoCamera()
    {
        m_Camera.transform.DOMove(target.transform.position, 5);
        b_recipe = true;
    }
    private void Update()
    {
        //무한 루프 방지 
        if(b_recipe && Vector3.Distance(m_Camera.transform.position , target.transform.position) <= 3f)
        {
            recipe.SetActive(true);
            recipe.transform.DOLocalMoveY(300 , 1).SetEase(Ease.Linear).SetLoops(1);
            b_recipe = false;
        }
    }
}
