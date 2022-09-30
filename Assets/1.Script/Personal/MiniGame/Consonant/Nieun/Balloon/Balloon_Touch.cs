using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    int BalloonTouch=0;//풍선 터치 카운트
    bool TriggerOnOff;
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 3)//다섯번 클릭했을때 
        {
            TouchClear();
            isTrigger_F();
        }
    }
    protected void TouchClear()//풍선 낙하 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }
    protected void isTrigger_F()//바닥 충돌
    {
        Animal.GetComponentInChildren<Collider>().isTrigger = false;
    }
    
}
