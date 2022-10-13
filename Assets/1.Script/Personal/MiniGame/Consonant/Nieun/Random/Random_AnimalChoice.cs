using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    [SerializeField]
    Random_Animal Animal;
    private void Start()
    {
        Animal = transform.parent.GetComponentInParent<Random_Animal>();
        transform.GetChild(Animal.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
    }
}
