using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    [SerializeField]
    Random_Animal Animal;
    public int number;
    private void Start()
    {
        Animal = transform.parent.GetComponentInParent<Random_Animal>();
        transform.GetChild(Animal.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
        number = Animal.shuffledArray[transform.parent.GetSiblingIndex()];
    }
}
