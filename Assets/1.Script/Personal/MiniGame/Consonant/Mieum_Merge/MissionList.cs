using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionList : MonoBehaviour
{
    [SerializeField]
    GameObject[] missionOb;       //미션 오브젝트
    public static Action<GameObject> ObjectOn;
    SpeakerHandler speakerHandler;
    ScoreHandler scoreHandler;
    int num= 0;//증감 변수 
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
                //i번째와 ob이름이 같고 비활성화 된 오브젝트만 활성화 시킨다.
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