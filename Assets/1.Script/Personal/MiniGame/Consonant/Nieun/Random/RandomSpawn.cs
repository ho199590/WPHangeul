using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�Ⱦ��� ��ũ��Ʈ
public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs; //�� ���� ������Ʈ�� �ִ´�
    private BoxCollider area;     //�ڽ��ݶ��̴��� ����� �������� ����
    private int count = 100;      //�� ���� ������Ʈ ����
    private List<GameObject> gameObject = new List<GameObject>();

    private void Start()
    {
        {
            area = GetComponent<BoxCollider>();

            for(int i = 0; i < count; ++i)
            {
                Spawn();//���� + ������ġ�� �����ϴ� �Լ�
            }
            area.enabled = false;
        }
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = 0.5f;
        float posZ = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
    private void Spawn()
    {
        int selection = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab =prefabs[selection];
        Vector3 spawnPos = GetRandomPosition();//������ġ�Լ�
        GameObject instance = Instantiate(selectedPrefab , spawnPos , Quaternion.identity);
        gameObject.Add(instance);
    }
}
