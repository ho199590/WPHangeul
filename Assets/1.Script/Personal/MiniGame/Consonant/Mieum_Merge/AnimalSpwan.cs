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
        StartCoroutine(MonsterSpwan()); //���� ���۽� �ڷ�ƾ ����
        plusCount = (Vector3 vec) =>
        {
            PlusCount(vec);
        };
    }
    IEnumerator MonsterSpwan()          //���� ���۽� ai���� �����ϰ� ����
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[UnityEngine.Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);    //�����ϰ� 5�� ���� 
            if (num == obCount)                     //���� ���ڸ� �Ѿ�� ���� ����
            {
                print("��������");
                maxMonster = true;
                StopAllCoroutines();
            }
        }
    }
    //�ѹ��� ���� �����Ǵ� ���� ���� �ؾ���.
    public void PlusCount(Vector3 vec) //������Ʈ�� �����Ǹ� ī��Ʈ ���� , Collider other���� ������ ��ġ�� vec
    {
        obCount += 2;
        GameObject effect = Instantiate(particle);  
        effect.transform.position = vec; //Collider other ��ġ���� effect����
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
