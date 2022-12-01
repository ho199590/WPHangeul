using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCHint : MonoBehaviour
{
    Collider col;   //Ŭ��Ȱ��ȭ ��Ȱ��ȭ�� �ݶ��̴�
    bool onOff;     //Ȱ��ȭ ��Ȱ��ȭ 
    [SerializeField]
    GameObject resipe , typingMessage;  //��Ʈ ������Ʈ
    public static Action Hint;
    public static Action ColliderOn;
    private void Start()
    { 
        col = GetComponent<Collider>(); //�ʱ�ȭ 
        col.enabled = false;            //ù���۽� �ݶ��̴� ��
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
    public void ColOn()//�ݶ��̴� Ŵ
    {
        col.enabled = true;
    }
}
