using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField]
    private MoleFSM[] moles;        //�ʿ� �����ϴ� �δ�����
    [SerializeField]
    private float spawnTime;        //�δ��� ���� �ֱ�

    //�δ��� ���� Ȯ�� (Normal : 85% , Red : 10% , Blue : 5%)
    private int[] spawnPercents = new int[3] { 34, 33, 33 };
    public void Setup()
    {
        StartCoroutine("SpawnMole");
    }
    private IEnumerator SpawnMole()
    {
        while(true)
        {
            //0 ~ Moles.Lenght1 �� ������ ���� ����
            int index = Random.Range(0, moles.Length);

            //���õ� �δ����� �Ӽ� ����
            moles[index].MoleType = SpawnMoleType();

            // index��° �δ����� ���¸� "moveUp"���� ����
            moles[index].ChangeState(MoleState.MoveUp);

            //spawnTime �ð����� ���
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private MoleType SpawnMoleType()
    {
        int percent = Random.Range(0, 100);
        float cumulative = 0;
        for (int i = 0; i < spawnPercents.Length; ++i)
        {
            cumulative += spawnPercents[i];
            
            if(percent < cumulative)
            {
                return (MoleType)i;
            }
        }
        return MoleType.Normal;
    }
}
