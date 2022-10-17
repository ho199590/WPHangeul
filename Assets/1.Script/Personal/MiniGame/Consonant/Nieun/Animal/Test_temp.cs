using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_temp : MonoBehaviour
{
   Random_AnimalChoice  random_AnimalChoice;
    int num;
    private void Start()
    {
        random_AnimalChoice = GetComponent<Random_AnimalChoice>();
        print(random_AnimalChoice.number);
    }
}
