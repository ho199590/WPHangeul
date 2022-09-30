using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice_N : MonoBehaviour
{
   
    private void Start()
    {
        transform.GetChild(Random_Animal_N.instance.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
    }
}
