using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Random_Animal : MonoBehaviour
{
    public int[] shuffledArray; //���� �迭
    [SerializeField]
    GameObject Animal;
    private void Awake()
    {
        var indexArray = Enumerable.Range(0, Animal.transform.childCount).ToArray();
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random.Next()).ToArray();
    }
}
