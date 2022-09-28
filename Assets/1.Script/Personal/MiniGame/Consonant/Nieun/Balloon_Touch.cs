using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    [SerializeField]
    GameObject Balloon;//풍선 오브젝트 

    int BalloonTouch=0;
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 5)//다섯번 클릭했을때 
        {
            TouchClear();
            isTrigger_F();
        }
    }
    protected void TouchClear()//풍선 낙하 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        Balloon.SetActive(false);
    }
    protected void isTrigger_F()//바닦이랑 충돌
    {
        Animal.GetComponent<Collider>().isTrigger = false;
    }
}
