using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpwan : MonoBehaviour
{
    [SerializeField]
    GameObject[] animalOb;
    [SerializeField]
    int obCount;
    int num = 1;
    private void Start()
    {
        StartCoroutine(MonsterSpwan());
    }
    IEnumerator MonsterSpwan()
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);
            if (num == obCount)
            {
                print("°¹¼öÁ¦ÇÑ");
                StopAllCoroutines();
            }
   
        }
    }
}
