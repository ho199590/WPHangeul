using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
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
        m_Camera.transform.DOMove(new Vector3(ob.transform.position.x,10f,ob.transform.position.z), 5);
        StartCoroutine(FeverTime(ob));
    }
    IEnumerator FeverTime(GameObject ob)
    {
        while(true)
        {
            Time.timeScale = 0.8f;
            yield return new WaitForSeconds(5f);
            Time.timeScale = 1f;
            m_Camera.transform.DOMove(dePosition, 3f);
            m_Camera.transform.DORotateQuaternion(deRotation, 3f);
            Destroy(ob);
            yield break;
        }
    }
}
