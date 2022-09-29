using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    [SerializeField]
    GameObject RandomOb;
    public int ChoiceNum;
    Random_Animal Random_Animal;
    void Start()
    {
        Random_Animal = GameObject.Find("Giyeok").GetComponent<Random_Animal>();
        print(Random_Animal.shuffledArray[ChoiceNum]);
        RandomOb.transform.GetChild(Random_Animal.shuffledArray[ChoiceNum]).gameObject.SetActive(true);
    }
}
