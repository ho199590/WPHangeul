using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_AnimalMove2 : MonoBehaviour
{
    public Transform targetPos;
    NavMeshAgent nav;
    private void Start()
    {

        nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        nav.SetDestination(targetPos.position);
    }
}
