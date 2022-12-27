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
    //ī�޶� 
    [SerializeField]
    Camera m_Camera;           //Talk�� ������ �Ͼ �̺�Ʈ�� ī�޶� ���� 
    [SerializeField]
    GameObject target;         //�ٶ� Ÿ��
    [SerializeField]
    GameObject npcBalloon;     //NPC��ǳ��
    [SerializeField]
    GameObject introOb;        //Intro ������Ʈ
    Vector3 dePosition;        //ī�޶� ����ġ ���� ����
    Quaternion deRotation;     //ī�޶� ������ ���� ����
    SpeakerHandler speaker;    //����Ŀ 
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
        //���� ���� ���� 
        if(Vector3.Distance(m_Camera.transform.position , target.transform.position) <= 3f)
        {
           /* recipe.transform.DOLocalMoveX(-327f, 3f).SetEase(Ease.Linear).SetLoops(1).OnComplete(() => { IntroDrag.Position(); });*/
        }
    }
    //���� ���� �׼�
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

    //���� ���߸� ī�޶� �̵� 
    private void AnswerMovie(GameObject ob)
    {
        canvas.SetActive(false);//canvas ��
        m_Camera.transform.DORotateQuaternion(Quaternion.Euler(90f, ob.transform.position.y, ob.transform.position.z), 5f);
        m_Camera.transform.DOMove(new Vector3(ob.transform.position.x, 13.5f, ob.transform.position.z), 5);
        StartCoroutine(FeverTime(ob));
    }
    //���� ���� ī�޶� �̵� 
    public void DoCamera()
    {
        m_Camera.transform.DOMove(target.transform.position, 5);
        StartCoroutine(IntroMove());
    }
    //ī�޶� ��ġ �ʱ�ȭ 
    private void CameraMoveReset(GameObject ob)
    {
        Destroy(ob);
        CameraPosition();
    }
    //ī�޶� ��ġ ����
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
    //Intro������ ī�޶� ������ ��ȯ 
    public void CameraPosition()
    {
        m_Camera.transform.DOMove(target.transform.position, 3f);
        m_Camera.transform.DOLocalRotateQuaternion(target.transform.rotation, 3f);
        NPCHint.Hint();
    }
    //��ǳ�� , IntroMapŴ
    private void IntroStart()
    {
        npcBalloon.SetActive(true);
        introOb.SetActive(true);
    }

}
