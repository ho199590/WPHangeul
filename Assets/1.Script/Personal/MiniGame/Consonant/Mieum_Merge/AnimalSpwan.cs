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
    ParticleSystem particle;
    int num = 1;            //���� ������ ���� ����
    bool maxMonster;        //���� maxġ���� Ȯ�� ����
    public static Action plusCount; //�̺�Ʈ ����

    private void Start()
    {
        StartCoroutine(MonsterSpwan()); //���� ���۽� �ڷ�ƾ ����
        plusCount = () =>
        {
            PlusCount();
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
    public void PlusCount() //������Ʈ�� �����Ǹ� ī��Ʈ ���� 
    {
        obCount += 2;
        print(num);
        if (num<obCount && maxMonster)
        {
            StartCoroutine(MonsterSpwan());
            print("�ٽ� �ڷ�ƾ ����");
        }
        else
        {
            maxMonster = false;
        }

    }
    
}
