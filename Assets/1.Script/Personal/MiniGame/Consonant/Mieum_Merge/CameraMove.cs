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
    public static Action CameraReset;
    //Canvas
    [SerializeField]
    GameObject canvas;
    //카메라 
    [SerializeField]
    Camera m_Camera;           //Talk가 끝나고 일어날 이벤트의 카메라 변수 
    [SerializeField]
    GameObject target;         //바라볼 타겟
    [SerializeField]
    GameObject npcBalloon;     //NPC말풍선
    [SerializeField]
    GameObject introOb;        //Intro 오브젝트
    Vector3 dePosition;        //카메라 원위치 저장 변수
    Quaternion deRotation;     //카메라 원방향 저장 변수
    SpeakerHandler speaker;    //스피커 
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
        CameraReset = () =>
        {
            CameraPosition();
        };
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
        CameraTargetPosition();
        speaker = FindObjectOfType<SpeakerHandler>();
    }
    
    private void Update()
    {
        //무한 루프 방지 
        if(Vector3.Distance(m_Camera.transform.position , target.transform.position) <= 3f)
        {
           /* recipe.transform.DOLocalMoveX(-327f, 3f).SetEase(Ease.Linear).SetLoops(1).OnComplete(() => { IntroDrag.Position(); });*/
        }
    }
    //동물 정답 액션
    IEnumerator FeverTime(GameObject ob)
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(5.35f);
            StarExplosion.ExplosionEffect(ob);
            speaker.SoundByNum2(3);
            yield return new WaitForSecondsRealtime(0.2f);
            Time.timeScale = 0.01f;
            yield return new WaitForSecondsRealtime(5f);
            Time.timeScale = 1f;
            CameraMoveReset(ob);
            canvas.SetActive(true);
            yield break;
         }
    }

    //정답 맞추면 카메라 이동 
    private void AnswerMovie(GameObject ob)
    {
        canvas.SetActive(false);//canvas 끔
        m_Camera.transform.DORotateQuaternion(Quaternion.Euler(90f, ob.transform.position.y, ob.transform.position.z), 5f);
        m_Camera.transform.DOMove(new Vector3(ob.transform.position.x, 13.5f, ob.transform.position.z), 5);
        StartCoroutine(FeverTime(ob));
    }
    //게임 시작 카메라 이동 
    public void DoCamera()
    {
        m_Camera.transform.DOMove(target.transform.position, 5);
        StartCoroutine(IntroMove());
    }
    //카메라 위치 초기화 
    private void CameraMoveReset(GameObject ob)
    {
        Destroy(ob);
        CameraPosition();
    }
    //카메라 위치 저장
    private void CameraTargetPosition()
    {
        dePosition = target.transform.position;
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
    }
    IEnumerator IntroMove()
    {
        Vector3 pos = new Vector3(-12.34f, 10.21f, 2.31f);
        Vector3 ros = new Vector3(21.9f, -46.1f, -9.5f);
        while (true)
        {
            yield return new WaitForSeconds(5f);
            m_Camera.transform.DOLocalMove(pos, 3f);
            m_Camera.transform.DOLocalRotate(ros, 3f, RotateMode.Fast).OnComplete(() => { IntroStart();});
            yield break;
        }
    }
    //Intro끝나고 카메라 포지션 전환 
    public void CameraPosition()
    {
        m_Camera.transform.DOMove(target.transform.position, 3f);
        m_Camera.transform.DOLocalRotateQuaternion(target.transform.rotation, 3f);
        NPCHint.Hint();
    }
    //말풍선 , IntroMap킴
    private void IntroStart()
    {
        npcBalloon.SetActive(true);
        introOb.SetActive(true);
    }

}
