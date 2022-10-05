using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    int BalloonTouch=0;//풍선 터치 카운트
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
        if(BalloonTouch == 3)//다섯번 클릭했을때 
        {
            TouchClear();
            BalloonTouch = 0;
        }
    }
    protected void TouchClear()//풍선 낙하 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }

    
}
