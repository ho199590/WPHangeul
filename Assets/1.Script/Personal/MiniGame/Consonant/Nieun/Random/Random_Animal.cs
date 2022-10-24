using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Random_Animal : MonoBehaviour
{
    public int[] shuffledArray; //���� �迭
    //�⿪ ���� ������Ʈ �ڽ��� �ڽ� ������Ʈ ���� ��ŭ ���� �迭 ����
    private void Awake()
    {
        var indexArray = Enumerable.Range(0, transform.GetChild(0).transform.GetChild(0).childCount).ToArray();
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random.Next()).ToArray();
    }
}
