using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCHint : MonoBehaviour
{
    bool onOff;     //Ȱ��ȭ ��Ȱ��ȭ 
    [SerializeField]
    GameObject resipe , typingMessage;  //��Ʈ ������Ʈ
    public static Action Hint;
    private void Start()
    {
        Hint = () =>
        {
            onOff = true;
        };
    }
    private void OnMouseDown()
    {
        if(onOff)
        {
            resipe.SetActive(true);
            typingMessage.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        resipe.SetActive(false);
        typingMessage.SetActive(false);
    }
}
