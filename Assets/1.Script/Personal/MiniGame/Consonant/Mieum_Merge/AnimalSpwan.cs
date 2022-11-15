using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpwan : MonoBehaviour
{
    [SerializeField]
    GameObject[] animalOb;  //ai ���� ������ ���� ����
    [SerializeField]
    int obCount;            //�ν����� â���� ���� ������ ���� �Ҽ��ְ� ����
    int num = 1;            //���� ������ ���� ����
    private void Start()
    {
        StartCoroutine(MonsterSpwan()); //���� ���۽� �ڷ�ƾ ����
    }
    IEnumerator MonsterSpwan()          //���� ���۽� ai���� �����ϰ� ����
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);    //�����ϰ� 5�� ���� 
            if (num == obCount)                     //���� ���ڸ� �Ѿ�� ���� ����
            {
                print("��������");
                StopAllCoroutines();
            }
   
        }
    }
}
