using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    int BalloonTouch=0;//ǳ�� ��ġ ī��Ʈ
    bool TriggerOnOff;
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 3)//�ټ��� Ŭ�������� 
        {
            TouchClear();
            isTrigger_F();
        }
    }
    protected void TouchClear()//ǳ�� ���� 
    {
        Animal.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }
    protected void isTrigger_F()//�ٴ� �浹
    {
        Animal.GetComponentInChildren<Collider>().isTrigger = false;
    }
    
}
