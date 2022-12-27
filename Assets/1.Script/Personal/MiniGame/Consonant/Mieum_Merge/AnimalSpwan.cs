using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimalSpwan : MonoBehaviour
{
    [SerializeField]
    GameObject[] animalOb;  //ai ���� ������ ���� ����
    [SerializeField]
    int obCount;            //�ν����� â���� ���� ������ ���� �Ҽ��ְ� ����
    [SerializeField]
    GameObject particle;    //������Ʈ �浹�� �����Ǵ� ��ƼŬ ���� 
    int num = 1;            //���� ������ ���� ����
    bool maxMonster;        //���� maxġ���� Ȯ�� ����
    public static Action<Vector3> plusCount; //Vector�̺�Ʈ ����
    private void Start()
    {
        StartCoroutine(MonsterSpwan()); 
        plusCount = (Vector3 vec) =>
        {
            PlusCount(vec);
        };
    }
    IEnumerator MonsterSpwan()        
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[UnityEngine.Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);    
            if (num == obCount)                     
            {
                print("��������");
                maxMonster = true;
                StopAllCoroutines();
            }
        }
    }
   
    public void PlusCount(Vector3 vec) 
    {
        obCount += 2;
        GameObject effect = Instantiate(particle);  
        effect.transform.position = vec; 
        Destroy(effect,1f);
        if (num<obCount && maxMonster)
        {
            StartCoroutine(MonsterSpwan());
        }
        else
        {
            maxMonster = false;
        }
    }
}
