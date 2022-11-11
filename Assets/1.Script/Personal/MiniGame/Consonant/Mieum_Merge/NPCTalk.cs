using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCTalk : MonoBehaviour
{
    //ī�޶� 
    [SerializeField]
    Camera m_Camera;        //Talk�� ������ �Ͼ �̺�Ʈ�� ī�޶� ���� 
    //Text Effect
    [SerializeField]         //text ����
    Text text;
    [SerializeField]
    string[] talk;          //�ν����Ϳ��� ��ȭ ���� �Ҽ��ְ� �ϰ� �迭�� ����
    [SerializeField]
    GameObject talkImage;   //��ȭ�� ������ ��Ȱ��ȭ �ؾ��ϱ� ������ ����
    [SerializeField]
    GameObject npc;    //��ȭ�� ������ ��Ȱ��ȭ �ؾ��ϱ� ����
    int talkIndex = 0; //�迭 ���� ����
    bool imageTouch = true;  //��ȭ ���� ��ġ ���ϰ� ����
    //Npc�� ��ȭ�� ������ ������Ʈ Ȱ��ȭ
    [SerializeField]
    GameObject animalOb;
    [SerializeField]
    GameObject animalObTwo;
    private void OnMouseDown()
    {
        if (imageTouch)
        {
            if (talk.Length == talkIndex)//��ȭ�� �ٳ����ٸ� Npc�� ���ش�
            {
                NpcOf();
            }
            else
            {
                StartCoroutine(OnType());//��ȭ�� ������ �ʾҴٸ� ���� ��ȭ�� �Ѿ��.
            }
        }
    }
    //��ȭâ ����
    private void NpcOf()
    {
        talkImage.SetActive(false);
        npc.SetActive(false);
        animalOb.SetActive(true);
        animalObTwo.SetActive(true);    
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
        yield return new WaitForSeconds(1f);
        imageTouch = true;
        yield break;
    }
}
