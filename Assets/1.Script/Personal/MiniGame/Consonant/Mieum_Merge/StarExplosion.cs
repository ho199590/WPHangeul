using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StarExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject[] Star;      //�� ������

    //�̺�Ʈ ���� 
    public static Action<GameObject> ExplosionEffect;

    private void Awake()
    {
        ExplosionEffect = (GameObject ob) =>
        {
            Effect(ob);
        };
    }
    //�� particle ����
    public void Effect(GameObject ob)
    {
        GameObject stars = Instantiate(Star[UnityEngine.Random.Range(0, Star.Length)]);
        stars.transform.localScale = new Vector3(5f, 5f, 5f);
        stars.transform.position = ob.transform.position;
    }
}
