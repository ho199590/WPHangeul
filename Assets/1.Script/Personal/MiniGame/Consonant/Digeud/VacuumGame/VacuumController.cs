using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VacuumState
{
    None,  
    Fall,  
    End
}


public class VacuumController : MonoBehaviour
{
    #region ����
    Ray ray;
    RaycastHit hit;
    public LayerMask UnderLayer;

    [SerializeField]
    Transform vacuumTranform;
    [SerializeField]
    VacuumState state;
    public VacuumState State
    {
        get { return state; }
        set { 

            state = value; 
        }
    }
    public System.Action Operate;

    [SerializeField]
    float height;
    #endregion

    #region �̺�Ʈ
    public event System.Action BeaconCheck;
    #endregion

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0)){State = VacuumState.Fall;}
        if (Input.GetMouseButton(0))
        {
            VacuumMove();
        }
    }

    #region �Լ�
    public void VacuumMove()
    {   
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnderLayer))
        {
            StageBlockHandler block = hit.transform.GetComponent<StageBlockHandler>();
            //block�� �������� ��쿡��
            if(block.GetBlock().Item2 == 0)
            {
                BeaconCheck?.Invoke();
                block.BeaconOn();

                vacuumTranform.position = block.GetBlock().Item1 + new Vector3(0, height, 0);
            }
        }
    }
    #endregion
}
