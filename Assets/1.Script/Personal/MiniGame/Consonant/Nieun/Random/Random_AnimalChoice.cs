using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    [SerializeField]
    Random_Animal Animal;
    [SerializeField]
    string Animal_Ob;
    private void Start()
    {
        Animal = GameObject.Find(Animal_Ob).GetComponent<Random_Animal>();
        transform.GetChild(Animal.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
    }
}
