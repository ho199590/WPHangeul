using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private float destoryDistance = 15;
    private AreaSpawner areaSpawner;
    private Transform playerTransform;

    public void Setup(AreaSpawner areaSpawner, Transform playerTransform)
    {
        this.areaSpawner = areaSpawner;
        this.playerTransform = playerTransform;
    }
    private void Update()
    {
        //�÷��̾� �ڷ� ���� �Ǹ� ���ο� ������ �����ϰ� �ڿ��ִ� ������ ���� ��Ų��
        if(playerTransform.position.z - transform.position.z >= destoryDistance)
        {
            //���ο� ������ ����
            areaSpawner.SpawnArea();
            //���� ������ ����
            Destroy(gameObject);
        }
    }
}
