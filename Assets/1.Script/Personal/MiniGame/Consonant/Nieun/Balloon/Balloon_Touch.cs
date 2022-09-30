using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    [SerializeField]
    GameObject Balloon;//ǳ�� ������Ʈ 

    int BalloonTouch=0;
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 5)//�ټ��� Ŭ�������� 
        {
            TouchClear();
            isTrigger_F();
        }
    }
    protected void TouchClear()//ǳ�� ���� 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        Balloon.SetActive(false);
    }
    protected void isTrigger_F()//�ٴ��̶� �浹
    {
        Animal.GetComponent<Collider>().isTrigger = false;
    }
}
