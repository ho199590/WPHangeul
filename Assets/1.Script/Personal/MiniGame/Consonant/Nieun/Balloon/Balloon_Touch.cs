using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    int BalloonTouch=0;//ǳ�� ��ġ ī��Ʈ
    SpeakerHandler speakerHandler;
    public Vector3 BalloonPosition;
    private void Start()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>();
    }
    private void OnMouseDown()
    {
        BalloonTouch++;
        speakerHandler.SoundByNum2(0);
        if(BalloonTouch == 3)//�ټ��� Ŭ�������� 
        {
            TouchClear();
            BalloonTouch = 0;
        }
    }
    protected void TouchClear()//ǳ�� ���� 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }

    
}
