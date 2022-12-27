using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionList : MonoBehaviour
{
    [SerializeField]
    GameObject[] missionOb;       //�̼� ������Ʈ
    public static Action<GameObject> ObjectOn;
    SpeakerHandler speakerHandler;
    ScoreHandler scoreHandler;
    int num= 0;//���� ���� 
    private void Awake()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        scoreHandler = FindObjectOfType<ScoreHandler>();
        ObjectOn = (GameObject ob) =>
        {
            StartCoroutine(MissionOn2(ob));
        };
    }
    IEnumerator MissionOn2(GameObject ob)
    {
        for (int i = 0; i < missionOb.Length; i++)
        {
            if (ob != null)
            {
                //i��°�� ob�̸��� ���� ��Ȱ��ȭ �� ������Ʈ�� Ȱ��ȭ ��Ų��.
                if (missionOb[i].gameObject.name == ob.name && !missionOb[i].activeSelf)
                {
                    yield return new WaitForSecondsRealtime(11.4f);
                    missionOb[i].SetActive(true);
                    speakerHandler.SoundByNum2(3);
                    scoreHandler.SetScore();
                    num++;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }


}