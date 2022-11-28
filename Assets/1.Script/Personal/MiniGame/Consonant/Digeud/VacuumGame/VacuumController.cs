using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum VacuumState
{
    None,
    Fall,
    End
}


public class VacuumController : MonoBehaviour
{
    #region ����
    VacuumArrayController array;
    VacuumLevelHandler level;
    VacuumLevel def;

    // û�ұ� �̵��� �̵��� ���� ������
    Ray ray;
    RaycastHit hit;
    
    public LayerMask UnderLayer;
    Transform underTransform;
    
    Vector3 liftPos;

    [SerializeField]
    Transform vacuumTranform;
    [SerializeField]
    Transform basketPos;

    [SerializeField]
    VacuumState state;

    [SerializeField]
    List<Transform> magnets = new List<Transform>();
    GameObject catchObj;

    [SerializeField]
    List<VacuumAbsorbController> absorb = new List<VacuumAbsorbController>();
    public VacuumState State
    {
        get { return state; }
        set
        {
            Operate = value switch
            {
                VacuumState.None => ProductCheck,
                VacuumState.Fall => VacuumFall,
                _ => VacuumToolTip
            };
            Operate?.Invoke();
            state = value;
        }
    }
    public System.Action Operate;

    [SerializeField]
    float height;
    #endregion

    #region �̺�Ʈ
    public event System.Action BeaconCheck;
    public event System.Action ProductEvent;
    #endregion

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (state == VacuumState.None)
                VacuumMove();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ProductCheck();
        }
    }


    #region �Լ�
    // �˸��� ĭ�� �����Ͽ��� �� ���ʿ� ����ϰ� ���� ��쿡�� �̵��Ѵ�.
    public void VacuumMove()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnderLayer))
        {
            VacuumBlockController block = hit.transform.GetComponent<VacuumBlockController>();
            //block�� �������� ��쿡��
            if (block.GetBlock().Item2 == 0)
            {
                BeaconCheck?.Invoke();
                block.BeaconOn();

                vacuumTranform.position = block.GetBlock().Item1 + new Vector3(0, height, 0);
                underTransform = block.transform;
            }
        }
    }

    public void VacuumFall()
    {
        if (state != VacuumState.None){return;}

        if (underTransform != null)
        {
            state = VacuumState.Fall;
            liftPos = vacuumTranform.position;

            vacuumTranform.DOMove(underTransform.position + (Vector3.up * 5), 3f).From(liftPos).OnComplete(() => VacuumLift());
        }
    }

    // û�ұ� �°�
    public void VacuumLift()
    {
        vacuumTranform.DOMove(liftPos, 2f).From(vacuumTranform.position).OnComplete(() => MagnetDrop());
    }

    public void VacuumToolTip()
    {

    }
    #endregion

    #region ��� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VacuumAbsorbController>() != null)
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().freezeRotation = false;
            other.GetComponent<VacuumAbsorbController>().State = 1;
            other.transform.SetParent(transform);
            
            catchObj = other.gameObject;            
        }
    }

    public void MagnetDrop()
    {   
        if(catchObj == null)
        {
            State = VacuumState.None;
            return;
        }
        Transform act = magnets[0];
        Vector3 Pos = act.position;
        

        GameObject obj = Instantiate(catchObj, magnets[2].position ,Quaternion.identity , act);
        obj.layer = act.gameObject.layer;
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.transform.localScale = Vector3.one * 0.5f;

        absorb.Add(obj.GetComponent<VacuumAbsorbController>());

        act.DOMove(magnets[1].position, 1).From(Pos).OnComplete(() =>
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
            act.DOMove(Pos, 1).From(act.position).SetDelay(0.5f).OnStart(() => State = VacuumState.None);
            obj.transform.SetParent(null);
        });

        Destroy(catchObj);
        catchObj = null;

        if(def == VacuumLevel.Normal)
        {
            array.ProductNormalSpawn();
        }
    }

    public void ProductCheck()
    {
        if(absorb.Count < 3){return;}

        foreach(VacuumAbsorbController va in absorb)
        {   
            if (va.result > 0)
            {
                va.State = 3;
            }
        }

        absorb.Clear();
    }
    #endregion
    #region
    private void Start()
    {
        level = FindObjectOfType<VacuumLevelHandler>();
        array = FindObjectOfType<VacuumArrayController>();
        def = level.Level;
    }
    #endregion
}
