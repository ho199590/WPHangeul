using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//동물 Spwan
public class AnimalSpwan : MonoBehaviour
{
    [SerializeField]
    GameObject[] animalOb;  
    [SerializeField]
    int obCount;           
    [SerializeField]
    GameObject particle;    
    int num = 1;            
    bool maxMonster;        //몬스터 max치인지 확인 변수
    public static Action<Vector3> plusCount;
    private void Start()
    {
        StartCoroutine(MonsterSpwan()); 
        plusCount = (Vector3 vec) =>
        {
            PlusCount(vec);
        };
    }
    IEnumerator MonsterSpwan()        
    {
        while (true)
        {
            GameObject monster = Instantiate(animalOb[UnityEngine.Random.Range(0, animalOb.Length)]);
            num++;
            yield return new WaitForSeconds(5f);    
            if (num == obCount)                     
            {
                maxMonster = true;
                StopAllCoroutines();
            }
        }
    }
   
    public void PlusCount(Vector3 vec) 
    {
        obCount += 2;
        GameObject effect = Instantiate(particle);  
        effect.transform.position = vec; 
        Destroy(effect,1f);
        if (num<obCount && maxMonster)
        {
            StartCoroutine(MonsterSpwan());
        }
        else
        {
            maxMonster = false;
        }
    }
}
