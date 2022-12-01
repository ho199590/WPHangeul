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
        Vector3 vec = new Vector3(ob.transform.position.x, -0.8f, ob.transform.position.z);
        GameObject stars = Instantiate(Star[UnityEngine.Random.Range(0, Star.Length)]);
        stars.transform.localScale = new Vector3(8f, 8f, 8f);
        stars.transform.position = vec;
    }
}
