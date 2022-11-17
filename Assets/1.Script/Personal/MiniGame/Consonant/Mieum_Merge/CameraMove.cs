using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
//유니티 슬로우모션 팁
//https://nanalistudios.tistory.com/11?category=934664
public class CameraMove : MonoBehaviour
{
    public static Action CameraEvents;
    public static Action<GameObject> MoveEvents;
    //레시피 오브젝트 
    [SerializeField]
    GameObject recipe;      //레시피 변수
    bool b_recipe;          //레시피형 불값
    //카메라 
    [SerializeField]
    Camera m_Camera;        //Talk가 끝나고 일어날 이벤트의 카메라 변수 
    [SerializeField]
    GameObject target;      //바라볼 타겟
    Vector3 dePosition;     //카메라 원위치 저장 변수
    Quaternion deRotation;     //카메라 원방향 저장 변수

    //슬로우 모션 변수  
    private float slowFactor = 0.05f; //슬로우 값
    private float slowLenght = 4f;    //슬로우 지속 시간 값
    private void Awake()
    {
        CameraEvents = () =>
        {
            DoCamera();
        };
        MoveEvents = (GameObject ob) =>
        {
            AnswerMovie(ob);
        };
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
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
            recipe.transform.DOLocalMoveX(-753 , 5f).SetEase(Ease.Linear).SetLoops(1);
            b_recipe = false;
        }
    }
    private void AnswerMovie(GameObject ob)
    {
        dePosition = transform.position; //원위치 저장 
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
        m_Camera.transform.DORotateQuaternion(Quaternion.Euler(90f, ob.transform.position.y, ob.transform.position.z), 5f);
        m_Camera.transform.DOMove(new Vector3(ob.transform.position.x,8.5f,ob.transform.position.z), 5);
        StartCoroutine(FeverTime(ob));
    }
    IEnumerator FeverTime(GameObject ob)
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(5.35f);
            StarExplosion.ExplosionEffect(ob);
            yield return new WaitForSecondsRealtime(0.15f);
            Time.timeScale = 0.01f;
            yield return new WaitForSecondsRealtime(5f);
            Time.timeScale = 1f;//잠깐 멈추고 다시 시작되어야 하는 시점 
            CameraMoveReset();
            Destroy(ob);
            yield break;
         }
    }
    //카메라 위치 초기화 
    private void CameraMoveReset()
    {
        m_Camera.transform.DOMove(dePosition, 3f);
        m_Camera.transform.DORotateQuaternion(deRotation, 3f);
    }


}
