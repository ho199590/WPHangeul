using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    int BalloonTouch=0;//풍선 터치 카운트
    public Vector3 BalloonPosition;
    Balloon_Move ballMove;
    private void Start()
    {
        ballMove = GetComponentInParent<Balloon_Move>();
    }
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 3)//다섯번 클릭했을때 
        {
            ballMove.enabled = false;
            TouchClear();
            ColOnOff();
            BalloonTouch = 0;
        }
    }
    protected void TouchClear()//풍선 낙하 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }
    private void ColOnOff()
    {
        Animal.GetComponent<Collider>().enabled = true;
    }
    
}
