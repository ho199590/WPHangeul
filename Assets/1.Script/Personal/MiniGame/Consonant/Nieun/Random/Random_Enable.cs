using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Random_Enable : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    public int[] shuffledArray; //랜덤 배열
    int index=0;
    private void Awake()
    {
        var indexArray = Enumerable.Range(0, gameObjects.Count).ToArray();//gameObjects 갯수만큼 랜덤
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random .Next()).ToArray();
        Choice();
    }
    public void Choice()//동물이 하나씩 활성화 된다.
    {
        if (index < 5)
        {
            gameObjects[shuffledArray[index]].SetActive(true);
            index++;
        }
    }
}
