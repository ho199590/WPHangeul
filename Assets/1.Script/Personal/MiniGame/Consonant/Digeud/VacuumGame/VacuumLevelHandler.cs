using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VacuumLevel
{
    Easy,
    Normal
}
// 게임 난이도 조정자
public class VacuumLevelHandler : MonoBehaviour
{
    #region
    [SerializeField]
    VacuumLevel level;
    public VacuumLevel Level
    {
        get { return level; }
        set {
            PosMove(value);
            level = value; 
        }
    }


    // 레벨이 세팅될 컨트롤러
    [SerializeField]
    VacuumArrayController vaController;
    [SerializeField]
    VacuumBlockController vbController;

    [SerializeField]
    Transform[] levelPos;

    List<GameObject> isInList = new List<GameObject>();
    #endregion

    #region

    public void GetObject(GameObject g){isInList.Add(g);}
    public void ClearList()
    {
        foreach(GameObject g in isInList)
        {
            Destroy(g);
        }
        isInList.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Level = level;
        }
    }

    private void Awake()
    {
        if (vaController == null)
            vaController = FindObjectOfType<VacuumArrayController>();
        if (vbController == null)
            vbController = FindObjectOfType<VacuumBlockController>();
    }

    public void PosMove(VacuumLevel level)
    {   
        transform.position = level == VacuumLevel.Easy ? (levelPos[0].position) : (levelPos[1].position);
    }
    #endregion
}
