using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCHint : MonoBehaviour
{
    Collider col;   //클릭활성화 비활성화할 콜라이더
    bool onOff;     //활성화 비활성화 
    [SerializeField]
    GameObject resipe , typingMessage;  //힌트 오브젝트
    public static Action Hint;
    public static Action ColliderOn;
    private void Start()
    { 
        col = GetComponent<Collider>(); //초기화 
        col.enabled = false;            //첫시작시 콜라이더 끔
        Hint = () =>{ onOff = true; typingMessage.SetActive(true);};
        ColliderOn = () => { ColOn(); };
    }
    private void OnMouseDown()
    {
        if(onOff)
        {
            resipe.SetActive(true);
            typingMessage.SetActive(false);
        }
    }
    private void OnMouseUp()
    {
        resipe.SetActive(false);
        typingMessage.SetActive(true);
    }
    public void ColOn()//콜라이더 킴
    {
        col.enabled = true;
    }
}
