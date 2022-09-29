using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Random_Animal : MonoBehaviour
{
    [SerializeField]
    GameObject RandomOb_Animal;

    int[] shuffledArray; //·£´ý ¹è¿­
    int shufflearrayindex; //intÇü ·£´ý º¯¼ö
    public void Start()
    {
        ShuffleArray();
        print(shuffledArray[shufflearrayindex]);
        for (int i = 0; i< RandomOb_Animal.transform.childCount; i++)
        {
            RandomOb_Animal.transform.GetChild(i).gameObject.SetActive(i == shuffledArray[shufflearrayindex]);
        }
    }
    protected void ShuffleArray()
    {
        var indexArray = Enumerable.Range(0, RandomOb_Animal.transform.childCount).ToArray();
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random.Next()).ToArray();
        shufflearrayindex = 0;
    }
}
