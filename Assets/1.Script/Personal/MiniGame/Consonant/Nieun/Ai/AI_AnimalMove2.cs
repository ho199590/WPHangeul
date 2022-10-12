using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_AnimalMove2 : MonoBehaviour
{
    GameObject red;
    NavMeshAgent nav;

    private void Start()
    {
        red = GameObject.Find("Alligator_LOD0");
        nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        nav.SetDestination(red.transform.position);
    }
}
