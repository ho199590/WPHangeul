using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsSpawnHandler : MonoBehaviour
{
    #region º¯¼ö
    [SerializeField]
    GameObject[] Friends;

    public Vector3 center;
    public Vector3 size;

    Quaternion min;
    #endregion
    private void Start()
    {
        FriendSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FriendSpawn();
        }
    }

    public void FriendSpawn()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2) , Random.Range(-size.y / 2, size.y / 2) , Random.Range(-size.z / 2, size.z / 2));

        int index = Random.Range(0, Friends.Length);

        Instantiate(Friends[index], pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
