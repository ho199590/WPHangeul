using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
//����Ƽ ���ο��� ��
//https://nanalistudios.tistory.com/11?category=934664
public class CameraMove : MonoBehaviour
{
    public static Action CameraEvents;
    public static Action<GameObject> MoveEvents;
    //Canvas
    [SerializeField]
    GameObject canvas;
    //������ ������Ʈ 
    [SerializeField]
    GameObject recipe;         //������ ����
    bool b_recipe;             //�������� �Ұ�
    //ī�޶� 
    [SerializeField]
    Camera m_Camera;           //Talk�� ������ �Ͼ �̺�Ʈ�� ī�޶� ���� 
    [SerializeField]
    GameObject target;         //�ٶ� Ÿ��
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
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
        CameraTargetPosition();
        speaker = FindObjectOfType<SpeakerHandler>();
    }
    
    private void Update()
    {
        //���� ���� ���� 
        if(b_recipe && Vector3.Distance(m_Camera.transform.position , target.transform.position) <= 3f)
        {
            recipe.SetActive(true);
            recipe.transform.DOLocalMoveX(-700, 5f).SetEase(Ease.Linear).SetLoops(1);
            b_recipe = false;
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
            Time.timeScale = 1f;//��� ���߰� �ٽ� ���۵Ǿ�� �ϴ� ���� 
            CameraMoveReset();
            ClearAnimal.Clear(ob);
            canvas.SetActive(true);//canvasŴ
            //AnswerAnimal(ob);
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
        b_recipe = true;
    }
    //ī�޶� ��ġ �ʱ�ȭ 
    private void CameraMoveReset()
    {
        m_Camera.transform.DOMove(dePosition, 3f);
        m_Camera.transform.DORotateQuaternion(deRotation, 3f);
    }
    //ī�޶� ��ġ ����
    private void CameraTargetPosition()
    {
        dePosition = target.transform.position; //����ġ ���� 
        deRotation = Quaternion.Euler(m_Camera.transform.eulerAngles);
    }
    //���� ���� �̵� 
    private void AnswerAnimal(GameObject ob)
    {
        ob.GetComponent<Animator>().SetInteger(ob.GetComponent<Animator>().GetParameter(0).name,1 );
    }
}
