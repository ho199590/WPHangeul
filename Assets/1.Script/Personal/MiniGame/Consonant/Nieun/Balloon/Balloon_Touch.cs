using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    int BalloonTouch=0;//ǳ�� ��ġ ī��Ʈ
    public Vector3 BalloonPosition;
    Balloon_Move ballMove;
    SpeakerHandler speakerHandler;
    private void Start()
    {
        ballMove = GetComponentInParent<Balloon_Move>();
        speakerHandler = FindObjectOfType<SpeakerHandler>();
    }
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 3)//�ټ��� Ŭ�������� 
        {
            ballMove.enabled = false;
            TouchClear();
            ColOnOff();
            BalloonTouch = 0;
        }
    }
    protected void TouchClear()//ǳ�� ���� 
    {
        speakerHandler.SoundByNum2(0);
        Animal.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
    }
    private void ColOnOff()//�ٴ۰� �浹 �ϱ����� �Լ�
    {
        Animal.GetComponent<Collider>().enabled = true;
    }
    
}
