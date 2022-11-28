using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCHint : MonoBehaviour
{
    bool onOff;     //활성화 비활성화 
    [SerializeField]
    GameObject resipe , typingMessage;  //힌트 오브젝트
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
