using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionList : MonoBehaviour
{
    [SerializeField]
    GameObject[] missionOb;       //�̼� ������Ʈ
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
            //i��°�� ob�̸��� ���� ��Ȱ��ȭ �� ������Ʈ�� Ȱ��ȭ ��Ų��.
            if (missionOb[i].gameObject.name == ob.name && !missionOb[i].activeSelf)
            {
                missionOb[i].SetActive(true);
                return;
            }
        }
    }
}
