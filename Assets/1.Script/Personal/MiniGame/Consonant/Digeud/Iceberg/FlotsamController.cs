using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlotsamController : MonoBehaviour
{
    #region 변수
    public Transform[] wayPoint;
    public Transform TargetPoint, FishBasket;

    [SerializeField]
    GameObject[] Fishes;
    
    List<GameObject> FishesList = new List<GameObject>();
    #endregion

    #region 함수

    private void Start()
    {
        foreach(Transform t in wayPoint)
        {
            int i = Random.Range(0, Fishes.Length);
            var fish = Instantiate(Fishes[i], t.position, Quaternion.identity, FishBasket);
        }
    }
    #endregion
}
