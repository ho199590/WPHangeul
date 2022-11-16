using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NPCTalk : MonoBehaviour
{
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
    GameObject npcBalloon;  //Npc��ǳ��

    private void OnMouseDown()
    {
        if (imageTouch)
        {
            if (talk.Length == talkIndex)//��ȭ�� �ٳ����ٸ� Npc�� ���ش�
            {
                Npc();
                NpcMove();
                CameraMove.CameraEvents();//ī�޶� �̵� �Լ� ȣ��
            }
            else
            {
                StartCoroutine(OnType());//��ȭ�� ������ �ʾҴٸ� ���� ��ȭ�� �Ѿ��.
            }
        }
    }
    //��ȭâ ����
    private void Npc()
    {
        talkImage.SetActive(false);
        animalOb.SetActive(true);  
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
    //Npc�̵� Dotween
    private void NpcMove()
    {
        Vector3 pos = new Vector3(130f, 646f, -7779f);
        Vector3 ros = new Vector3(30f, 145f, -18f);
        Vector3 scale = new Vector3(1000f, 1000f, 1000f);
        npc.transform.DOLocalMove(pos, 3f);
        npc.transform.DOLocalRotate(ros, 3f, RotateMode.Fast);
        npc.transform.DOScale(scale, 3f);
        npcBalloon.SetActive(true);
    }
}
