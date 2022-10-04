using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShepherdController : MonoBehaviour
{
    #region º¯¼ö    
    NavMeshAgent agent;
    [SerializeField]
    GameObject Parti;
    #endregion

    private void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Cube")
                {
                    agent.SetDestination(hit.point);
                    var eff = Instantiate(Parti);
                    eff.transform.position = hit.point + new Vector3(0,1,0);
                }   
                //transform.position = hit.point;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Cube")
                {
                    agent.SetDestination(hit.point);
                }
                //transform.position = hit.point;
            }
        }
    }
}
