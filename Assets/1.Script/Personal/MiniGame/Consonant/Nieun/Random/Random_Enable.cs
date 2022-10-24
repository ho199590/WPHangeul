using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Random_Enable : MonoBehaviour
{
    Index myIndex = new Index();  
    //�����ϰ� ������Ʈ Ȱ��ȭ 
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    public int[] shuffledArray; //���� �迭
    private void Awake()
    {
        var indexArray = Enumerable.Range(0, gameObjects.Count).ToArray();//gameObjects ������ŭ ����
        System.Random random = new System.Random();
        shuffledArray = indexArray.OrderBy(x => random .Next()).ToArray();
        Choice();
    }
    public void Choice()//������ �ϳ��� Ȱ��ȭ �ȴ�.
    {
        if (myIndex.index < 5)
        {
            gameObjects[shuffledArray[myIndex.index]].SetActive(true);
            myIndex.index++;
        }
    }
}
public class Index
{
    public int index;
    public int Number
    {
        get => index;

        set => index = value;   
    }
}

