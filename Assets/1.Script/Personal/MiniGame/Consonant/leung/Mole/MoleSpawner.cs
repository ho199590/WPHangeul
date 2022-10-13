using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField]
    private MoleFSM[] moles;        //�ʿ� �����ϴ� �δ�����
    [SerializeField]
    private float spawnTime;        //�δ��� ���� �ֱ�

    public void Start()
    {
        StartCoroutine("SpawnMole");

    }
    private IEnumerator SpawnMole()
    {
        while(true)
        {
            //0 ~ Moles.Lenght1 �� ������ ���� ����
            int index = Random.Range(0, moles.Length);
            // index��° �δ����� ���¸� "moveUp"���� ����
            moles[index].ChangeState(MoleState.MoveUp);

            //spawnTime �ð����� ���
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
