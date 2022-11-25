using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class IntroText : MonoBehaviour
{
    [SerializeField]         //text ����
    Text text;
    [SerializeField]
    string[] talk;          //�ν����Ϳ��� ��ȭ ���� �Ҽ��ְ� �ϰ� �迭�� ����
    [SerializeField]
    GameObject image , introMap;       //Image GameObject
    BoxCollider2D boxCollider;
    [SerializeField]
    GameObject[] ob;
    int talkIndex = 0; //�迭 ���� ����
    bool imageTouch = true;  //��ȭ ���� ��ġ ���ϰ� ����
    public static Action PlusIndex; 
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        PlusIndex = () => { UpIndex(); };
        StartCoroutine(OnType());//��ȭ�� ������ �ʾҴٸ� ���� ��ȭ�� �Ѿ��
    }

    IEnumerator OnType()
    {
        text.text = " ";
        imageTouch = false;
        foreach (char item in talk[talkIndex++])
        {
            text.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        imageTouch = true;
        yield return new WaitForSeconds(1f);
        yield break;
    }
    private void OnMouseDown()
    {
        if(imageTouch)
        {
            if (talk.Length == talkIndex)//��ȭ�� �ٳ����ٸ�
            {
                CameraMove.CameraReset();//ī�޶� �̵�
                image.SetActive(false);
                introMap.SetActive(false);
            }
            else
            StartCoroutine(OnType());
        }
    }
    private void Update()
    {
        if(talkIndex==4 || talkIndex == 6)
        {
            if(talkIndex == 4)
            {
                ob[0].SetActive(true);
            }
            if (talkIndex == 6)
            {
                ob[1].SetActive(true);
            }
            boxCollider.enabled = false;
        }
    }
    public void UpIndex()
    {
        StartCoroutine(OnType());
        boxCollider.enabled = true; 
    }
}
