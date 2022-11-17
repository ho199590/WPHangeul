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
    //������ ������Ʈ 
    [SerializeField]
    GameObject recipe;      //������ ����
    bool b_recipe;          //�������� �Ұ�
    //ī�޶� 
    [SerializeField]
    Camera m_Camera;        //Talk�� ������ �Ͼ �̺�Ʈ�� ī�޶� ���� 
    [SerializeField]
    GameObject target;      //�ٶ� Ÿ��
    Vector3 dePosition;     //ī�޶� ����ġ ���� ����
    Quaternion deRotation;     //ī�޶� ������ ���� ����

    //���ο� ��� ����  
    private float slowFactor = 0.05f; //���ο� ��
    private float slowLenght = 4f;    //���ο� ���� �ð� ��
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
            recipe.transform.DOLocalMoveX(-753 , 5f).SetEase(Ease.Linear).SetLoops(1);
            b_recipe = false;
        }
    }
    private void AnswerMovie(GameObject ob)
    {
        dePosition = transform.position; //����ġ ���� 
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
            Time.timeScale = 1f;//��� ���߰� �ٽ� ���۵Ǿ�� �ϴ� ���� 
            CameraMoveReset();
            Destroy(ob);
            yield break;
         }
    }
    //ī�޶� ��ġ �ʱ�ȭ 
    private void CameraMoveReset()
    {
        m_Camera.transform.DOMove(dePosition, 3f);
        m_Camera.transform.DORotateQuaternion(deRotation, 3f);
    }


}
