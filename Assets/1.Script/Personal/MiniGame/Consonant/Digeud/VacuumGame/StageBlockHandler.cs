using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지 스폰 && 스테이지 빛나기 조정용
public class StageBlockHandler : MonoBehaviour
{
    #region 변수
    [SerializeField]
    GameObject beacon;
    Vector3 beaconPos;

    VacuumController controller;
    int state = 0;

    [SerializeField]
    GameObject TestOBJ;
    #endregion

    private void Awake()
    {
        beaconPos = beacon.transform.position;
        controller = FindObjectOfType<VacuumController>();
        controller.BeaconCheck += BeaconOff;

//      ObjectSpawn(TestOBJ);
    }

    public (Vector3, int) GetBlock() { return (beaconPos, state); }
    public void BeaconOn()
    {
        beacon.SetActive(true);
        state = 1;
    }
    public void BeaconOff()
    {
        beacon.SetActive(false);
        state = 0;
    }
    public Transform ObjectSpawn(GameObject Target)
    {
        var t = Instantiate(Target, beaconPos, Quaternion.identity);

        return t.transform;
    }
    private void OnDestroy() { controller.BeaconCheck -= BeaconOff; }
}

