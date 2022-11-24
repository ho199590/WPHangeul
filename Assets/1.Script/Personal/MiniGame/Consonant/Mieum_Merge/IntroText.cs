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
    GameObject ob1,ob2;          //������ ����
    int talkIndex = 0; //�迭 ���� ����
    private void Start()
    {
        StartCoroutine(OnType());//��ȭ�� ������ �ʾҴٸ� ���� ��ȭ�� �Ѿ��.
    }

    IEnumerator OnType()
    {
        text.text = " ";
        foreach (char item in talk[talkIndex++])
        {
            text.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        yield break;
    }
    private void OnMouseDown()
    {
        StartCoroutine(OnType());
    }
    private void Update()
    {
        if (talkIndex == 4)
        {
            ob1.SetActive(true);
        }
        else if (talkIndex == 6)
        {
            ob2.SetActive(true);
        }
    }
}
