using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadHandler : MonoBehaviour
{
    #region ����
    SnakeMovement movement;
    FriendsSpawnHandler FS;

    #endregion

    #region �Լ�
    private void Start()
    {
        movement = FindObjectOfType<SnakeMovement>();
        FS = FindObjectOfType<FriendsSpawnHandler>();
    }

    private void OnTriggerEnter(Collider col)
    {   
        if (col.GetComponentInParent<SnakeMovement>())
        {
            print("����");
            return;
        }

        if (col.GetComponentInChildren<LODGroup>())
        {
            movement.AddBodyPart(col.gameObject);
            Destroy(col.gameObject);

            //spawn
            FS.FriendSpawn();
        }


    }
    #endregion
}
