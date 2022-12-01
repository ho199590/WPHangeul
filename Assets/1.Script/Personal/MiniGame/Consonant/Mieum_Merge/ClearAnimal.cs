using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//정답을 맞추면 정답 이미지칸에 오브젝트 이동 
public class ClearAnimal : MonoBehaviour
{
    [SerializeField]
    GameObject[] ob;

    int index = 0;
    public static Action<GameObject> Clear;

    private void Awake()
    {
        Clear = (GameObject ob) =>
        {
            ChangeOb(ob);
        };
    }

    public void ChangeOb(GameObject clearob)
    {
        clearob.transform.position = ob[index].transform.position;
        index++;
    }
}
