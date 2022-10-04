using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(Random_Animal.instance.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
    }
}
