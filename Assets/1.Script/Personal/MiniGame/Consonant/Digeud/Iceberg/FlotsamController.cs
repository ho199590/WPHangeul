using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ǥ���� ��Ʈ�ѷ�
public class FlotsamController : MonoBehaviour
{
    #region ����
    // ����� ���ƴٴ� ���
    public List<Transform> wayPoint = new List<Transform>();
    // ������ ����� Ž�� �θ�
    public Transform FishBasket;
    //����� ����
    public int Count;

    //����� ������
    [SerializeField]
    GameObject[] Fishes;

    // ���� ��ġ�� ����Ʈ
    [SerializeField]
    List<int> indexList = new List<int>();
    #endregion

    #region �Լ�

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            wayPoint.Add(transform.GetChild(i).transform);
        }

        for (int i = 0; i < Count; i++)
        {
            AddFish();
        }
    }

    //����� �߰�
    public void AddFish()
    {   
        int h = Random.Range(0, Fishes.Length);
        int[] array = indexList.ToArray();

        int p = RandomNumberPicker.GetRandomNumberByArray(array, wayPoint.Count);
        indexList.Add(p);

        if(indexList.Count >= wayPoint.Count)
        {
            indexList.Clear();
        }

        var fish = Instantiate(Fishes[h], wayPoint[p].position, Quaternion.identity, FishBasket);
        fish.GetComponent<FishController>().Index = p;
    }
    #endregion
}
