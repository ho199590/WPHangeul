using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBlockController : MonoBehaviour
{
    #region
    [SerializeField]
    VacuumLevelHandler Level;
    VacuumController controller;
        
    VacuumLevel def;
    int state = 0;

    [SerializeField]
    public GameObject block, beacon, liner, point;

    [SerializeField]
    Material[] mats;

    [SerializeField]
    int checkAnswer;

    Vector3 beaconPos;
    #endregion

    private void Awake()
    {
        Level = FindObjectOfType<VacuumLevelHandler>();
        
        def = Level.Level;

        beaconPos = beacon.transform.position;
        controller = FindObjectOfType<VacuumController>();
        controller.BeaconCheck += BeaconOff;

        BeaconOff();
    }

    public (Vector3, int) GetBlock() { return (block.transform.position, state); }
    public void BeaconOn()
    {   
        beacon.SetActive(true);
        //point.SetActive(true);
        //liner.GetComponent<SpriteRenderer>().color = Color.white;

        point.GetComponent<Renderer>().material = mats[1];

        if (def == VacuumLevel.Easy)
        {
            block.GetComponent<Renderer>().material = mats[1];
        }

        state = 1;
    }
    public void BeaconOff()
    {
        beacon.SetActive(false);
        //point.SetActive(false);

        point.GetComponent<Renderer>().material = mats[0];
        point.GetComponent<Renderer>().material.color = new Color(
            point.GetComponent<Renderer>().material.color.r, 
            point.GetComponent<Renderer>().material.color.g, 
            point.GetComponent<Renderer>().material.color.b, 
            0.4f);


        if (def == VacuumLevel.Easy)
        {
            block.GetComponent<Renderer>().material = mats[0];
        }
        state = 0;
    }
    public Transform ObjectSpawn(GameObject Target)
    {
        var t = Instantiate(Target, beaconPos, Quaternion.identity);

        return t.transform;
    }
    private void OnDestroy() { controller.BeaconCheck -= BeaconOff; }


    public void OffBlock(float offset)
    {
        block.GetComponent<Renderer>().material.color = new Color(0, 0, 0, offset);
        point.GetComponent<Renderer>().enabled = false;
        transform.GetComponent<Collider>().enabled = false;
        liner.SetActive(false);        
    }

}
