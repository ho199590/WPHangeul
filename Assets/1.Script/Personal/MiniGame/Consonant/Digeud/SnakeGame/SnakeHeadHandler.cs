using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

// �� �Ӹ� ����
public class SnakeHeadHandler : MonoBehaviour
{
    #region ����
    SnakeMovement movement;
    FriendsSpawnHandler FS;
    NavMeshAgent agent;
    // �߰� ���
    ShepherdController shepher;


    float originSpeed;
    #endregion

    #region �Լ�

    private void Start()
    {
        movement = FindObjectOfType<SnakeMovement>();
        FS = FindObjectOfType<FriendsSpawnHandler>();
        shepher = FindObjectOfType<ShepherdController>();

        agent = GetComponent<NavMeshAgent>();
        originSpeed = agent.speed;

        
    }


    private void OnTriggerEnter(Collider col)
    {   
        if (col.GetComponentInParent<SnakeMovement>()){return;}

        if (col.GetComponentInChildren<LODGroup>())
        {
            movement.AddBodyPart(col.gameObject);
            Destroy(col.gameObject);

            FS.FriendSpawn();
        }
        if (col.GetComponent<ShepherdController>())
        {
            transform.DOKill();
            agent.speed = originSpeed;  
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<ShepherdController>()){agent.speed = originSpeed;}
    }

    private void Update() 
    {
        agent.SetDestination(shepher.transform.position);
        agent.speed += Time.deltaTime;
    }
    #endregion
}
