using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Random_Animal : MonoBehaviour
{
    [SerializeField]
    GameObject RandomOb_Animal;

    public int[] shuffledArray; //·£´ý ¹è¿­
    public static Random_Animal instance;

    private void Awake()
    {
        instance = this;
        var indexArray = Enumerable.Range(0, RandomOb_Animal.transform.childCount).ToArray();
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random.Next()).ToArray();
    }

}
