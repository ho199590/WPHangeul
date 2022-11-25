using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class IntroText : MonoBehaviour
{
    [SerializeField]         //text ����
    Text text;
    [SerializeField]
    string[] talk;          //�ν����Ϳ��� ��ȭ ���� �Ҽ��ְ� �ϰ� �迭�� ����
    [SerializeField]
    GameObject image;       //Image GameObject
    int talkIndex = 0; //�迭 ���� ����
    bool imageTouch = true;  //��ȭ ���� ��ġ ���ϰ� ����
    private void Start()
    {
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
            }
            else
            StartCoroutine(OnType());
        }

    }

}
