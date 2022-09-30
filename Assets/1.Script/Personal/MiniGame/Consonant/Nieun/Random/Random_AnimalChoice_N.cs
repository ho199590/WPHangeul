using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    [SerializeField]
    GameObject Ob;

    private void Start()
    {
        transform.GetChild(Random_Animal.instance.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
    }
}
