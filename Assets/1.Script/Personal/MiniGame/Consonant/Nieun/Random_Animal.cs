using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Random_Animal : MonoBehaviour
{
    [SerializeField]
    GameObject randomOb_in_Balloon;

    int[] shuffledArray; //·£´ý ¹è¿­
    int shufflearrayindex; //intÇü ·£´ý º¯¼ö
    public void Start()
    {
        ShuffleArray();
        for (int i = 0; i<randomOb_in_Balloon.transform.childCount; i++)
        {
            randomOb_in_Balloon.transform.GetChild(i).gameObject.SetActive(i == shuffledArray[shufflearrayindex]);
        }
    }
    protected void ShuffleArray()
    {
        var indexArray = Enumerable.Range(0, randomOb_in_Balloon.transform.childCount).ToArray();
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random.Next()).ToArray();
        shufflearrayindex = 0;
    }
}
