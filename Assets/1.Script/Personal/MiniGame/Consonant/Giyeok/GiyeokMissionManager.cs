using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
// https://flowtree.tistory.com/46
// https://wolstar.tistory.com/5

public class GiyeokMissionManager : MonoBehaviour
{
    #region ����
    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������
    [SerializeField]
    GameObject pico; //���� ĳ����
    [SerializeField]
    GameObject hand; //�巡�� ������ ��
    [SerializeField]
    GameObject invisible; //ȭ�� ��Ȱ��ȭ�� ���� ������Ʈ
    [SerializeField]
    Image zoomPosition; //�����Ⱑ �̵��� ��ġ��
    private Vector3 posi; //3d������Ʈ�� ���콺�巡�� ���� ���� �����̰� �ϱ� ���Կ�
    private float z_saved; //3d������Ʈ�� ���콺�巡�� ���� ���� �����̰� �ϱ� ���Կ� //x,y���� ������ �ִ� ���콺�� ��ġ���� z���� �ο����ֱ� ���� ����� ����
    new Collider collider; //�浹ó���� �ݶ��̴� �����
    GameObject savedOb; //�巡�� �� �����⿡ �浹ó���� �ִϸ��̼��� �����ִ� �ϱ� ���� �浹 �ݶ��̴� ����� �Ķ����
    [SerializeField]
    GameObject startRayPosi; //������ ���� �Ѱ������ ���̸� ��� ��ġ�� �������� �ڽ� ������Ʈ
    [Tooltip("�� ���� ���� �̸��� �Է��� �ּ���")]
    [SerializeField]
    string obName; //�� ���� ���� �̸�

    //private Animator animatorPico; //���� ����ĳ���Ͱ� �����߾���
    private Animator animatorColl;
    private Animator animatorSave;
    int count = 0; //�浹�Ϸ� üũ�� �Ķ����
    bool playstart; //�Ҹ� ��� ���� �ٲ� �� �������� ��������� �ƴ��� üũ�ϴ¿� �Ķ����
    bool check; //���콺�ٿ�ÿ� ó���� �ѹ��� ���� �ȳ� ��Ҹ� �����Բ� �ϴ� �Ķ����
    SpeakerHandler speakerHandler;

    float currentTime; //lerp�� deltaTime������
    Vector3 startPosi; //lerp�� ������ġ ������
    #endregion
    private void Awake()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(ZoomMove(6f));
    }
    private void Start()
    {
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForFixedUpdate();
        invisible.SetActive(true);
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        speakerHandler.SoundByNum2(0);
        speakerHandler.SoundByNum(1);
        //animatorPico = pico.GetComponent<Animator>();
        check = true;
        scoreCase.SceneComplete += MissionComplete;
        yield break;
    }
    #region �Լ�
    //�����Ⱑ �� ������ õõ�� ������ ������ִ� ���� �Լ�
    IEnumerator ZoomMove(float lerpTime)
    {
        currentTime = 0;
        startPosi = transform.position;
        while((currentTime/lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosi, zoomPosition.transform.position, currentTime / lerpTime);
            //transform.position = Vector3.Lerp(startPosi, zoomPosition.transform.position, Mathf.SmoothStep(0, 1, currentTime / lerpTime));
            yield return null;
        }
        //---------------------------------------------------------------- Lerp �߸�������� ��-----------------------------------------------------------------------
        //while (Vector3.Distance(transform.position, zoomPosition.transform.position) > 0.2f)//�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        //{
        //    transform.position = Vector3.Lerp(transform.position, zoomPosition.transform.position, Time.deltaTime * 0.7f);
        //    yield return new WaitForSeconds(Time.deltaTime*0.2f); //���ڸ��� ���ư��� �ӵ� �����ϴ� ��
        //    if (Vector3.Distance(transform.position, zoomPosition.transform.position) <= 0.2f)
        //    {
        //        break;
        //    }
        //}
        //transform.position = zoomPosition.transform.position;
        if (lerpTime == 6f) //Awake���� ȣ��ÿ��� �ؾ��� ��
        {
            invisible.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = true;
            hand.SetActive(true);
        }
    }
    private void OnMouseDown()
    {
        if (check)
        {
            speakerHandler.SoundByNum(2);
        }
        check = false;
        hand.SetActive(false);
        transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true); //�����⸦ �巡�׽����� �� ������ �ֺ� ��Ӱ����ִ� ������ũ 
        z_saved = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        posi = gameObject.transform.position - GetMouseWorldPosition();
        count = 0;
    }
    //�巡�� �� �浹ó���� ������Ʈ�� �ִϸ��̼��� ��ġ�� �ʰ� �ִϸ��̼��� �׶��׶� �ٲ���
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
        speakerHandler.SoundByNum(8);
        //animatorPico.SetInteger("PicoAction", 4);
        collider = CheckOb(); //�浹ó�� �Լ� ȣ��
        if(collider != null)
        {
            if (!playstart)
            {
                speakerHandler.SoundByNum(9);
                if (collider.GetComponentInChildren<AudioSource>()) collider.GetComponentInChildren<AudioSource>().Play();
                playstart = true;
            }
            animatorColl = collider.gameObject.GetComponent<Animator>();
            if (collider.transform.parent.name.Contains(obName))
            {
                if (savedOb != null && savedOb.GetComponent<CapsuleCollider>().enabled && savedOb.transform.parent.name.Contains(obName))
                { //����ó���� �ȵ� ������Ʈ�� ĸ���ݶ��̴��� �����ִ�
                    animatorSave = savedOb.GetComponent<Animator>();
                    animatorSave.SetInteger(savedOb.gameObject.name + "Ani", 0);
                }
                savedOb = collider.gameObject; //������ �浹 ������Ʈ�� ����ص״ٰ� �ִϸ��̼��� �ٲ��ֱ� ���� ����� 
                animatorColl = collider.gameObject.GetComponent<Animator>();
                animatorColl.SetInteger(collider.name + "Ani", 2);
            }
            else
            { //����ó���� �Ϸ�� ������Ʈ���� ĸ���ݶ��̴� �����ִ�
                if (animatorColl != null && animatorColl.GetComponent<CapsuleCollider>().enabled) animatorColl.SetInteger(collider.name + "Ani", 0);
                if (savedOb != null && savedOb.GetComponent<CapsuleCollider>().enabled) animatorSave.SetInteger(savedOb.gameObject.name + "Ani", 0);
            }
        }
        else
        {
            playstart = false;
        }
    }
    //���콺 ���� �����̴� ������ �̵��� �Լ�
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition; 
        mousePoint.z = z_saved; //���콺����Ʈ�� z�� ������ֱ�
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    // ������� �浹ó���� �Լ�
    Collider CheckOb()
    {
        Ray ray = new Ray(startRayPosi.transform.position, transform.forward);
        if (Physics.SphereCast(ray, 0.1f, out RaycastHit hit))
        {
            print("�浹ó�� ����" + hit.collider);
            return hit.collider;
        }
        else return null;
    }    
    private void OnMouseUp()
    {
        playstart = false;
        collider = CheckOb();
        if (collider != null)
        {
            if (collider.transform.parent.name.Contains(obName))
            {
                speakerHandler.SoundByNum(3);
                invisible.transform.SetAsLastSibling();
                invisible.SetActive(true);
                count++;
                if (count == 1)
                {
                    print("�����Դܾ� Ȯ��");
                    //animatorPico.SetInteger("PicoAction", 1);
                    animatorColl.SetInteger(collider.name + "Ani", 1);
                    scoreCase.SetScore();
                    collider.gameObject.GetComponent<CapsuleCollider>().enabled = false; //����ó�� �Ϸ�� ������Ʈ�� �浹ó���� �����ϱ����� �ݶ��̴� ���ֱ�
                }
            }
            else
            {
                //animatorPico.SetInteger("PicoAction", 2);
                speakerHandler.SoundByNum(10);
                StartCoroutine(ZoomMove(0.5f));
            }
        }
    }
    private void OnMouseExit()
    {
        playstart = false;
    }
    //�̼ǿϷ�� ScoreCase�������� SceneComplete�̺�Ʈ�� �־��� �Լ�
    void MissionComplete()
    {
        print("�̼ǳ� : ���� win(hi-host) �ִϸ��̼� �÷���");
        invisible.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false; //������ ���ϰ� �ϱ�
        transform.GetChild(0).gameObject.SetActive(false); //����ũ ���ֱ�
        StartCoroutine(ZoomMove(1f));
        //animatorPico.SetInteger("PicoAction", 3); //���ھִϸ��̼� �߿� 3�� hi-host�ѱ�   
    }
    #endregion
}
