using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    #region 변수
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    int BalloonTouch=0;//풍선 터치 카운트
    public Vector3 BalloonPosition;
    SpeakerHandler speakerHandler;
    //[풍선이 터지면 동물작아지는 변수]
    private float size = 0.8f; //원하는 사이즈
    private float speed = 1f; //작아 질때의 속도
    private float time = 0;
    #endregion
    #region
    private void Start()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>(); 
    }
    private void OnMouseDown()
    {
        /*세번클릭했을때
          BalloonTouch++;
                if(BalloonTouch == 3)
                {
                    StartCoroutine(Down());
                    TouchClear();
                    ColOnOff();
                    BalloonTouch = 0;
                }*/
        Animal.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        StartCoroutine(Down());
        TouchClear();
        ColOnOff();
    }
    protected void TouchClear()//풍선 낙하 
    {
        speakerHandler.SoundByNum2(0);
        Animal.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.SetActive(false);
    }
    private void ColOnOff()//바닦과 충돌 하기위한 함수
    {
        Animal.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Down()//동물 점점 작아짐 
    {
        while (Animal.transform.localScale.x >= size)
        {
            print("작아지는중");
            Animal.transform.localScale = new Vector3(Animal.transform.localScale.x - 0.01f ,
            Animal.transform.localScale.y - 0.01f , Animal.transform.localScale.z - 0.01f);
        }
        yield break;

    }
    public void SizeReset()//실패시 동물 크기 리셋
    {
        Animal.transform.localScale = new Vector3(1f, 1f, 1f);
        print($"{Animal}실행");
    }
    #endregion
}

