using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionList : MonoBehaviour
{
    [SerializeField]
    GameObject[] missionOb;       //미션 오브젝트
    public static Action<GameObject> ObjectOn;
    private void Awake()
    {
        ObjectOn = (GameObject ob) =>
        {
            MissionOn(ob);
        };
    }
    public void MissionOn(GameObject ob)
    {
        for(int i  = 0; i < missionOb.Length; i++)
        {
            //i번째와 ob이름이 같고 비활성화 된 오브젝트만 활성화 시킨다.
            if (missionOb[i].gameObject.name == ob.name && !missionOb[i].activeSelf)
            {
                missionOb[i].SetActive(true);
                return;
            }
        }
    }
}
