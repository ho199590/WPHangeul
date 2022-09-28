using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadHandler : MonoBehaviour
{
    #region 변수
    SnakeMovement movement;
    FriendsSpawnHandler FS;

    #endregion

    #region 함수
    private void Start()
    {
        movement = FindObjectOfType<SnakeMovement>();
        FS = FindObjectOfType<FriendsSpawnHandler>();
    }

    private void OnTriggerEnter(Collider col)
    {   
        if (col.GetComponentInParent<SnakeMovement>())
        {
            print("꼬리");
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
