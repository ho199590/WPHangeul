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
// 청소기 컨트롤러
public class VacuumController : MonoBehaviour
{
    #region 변수
    // 청소기 이동과 이동을 위한 변수들
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
    List<GameObject> ForceTestList = new List<GameObject>();
    public VacuumState State
    {
        get { return state; }
        set
        {

            state = value;
        }
    }
    public System.Action Operate;

    [SerializeField]
    float height;
    #endregion

    #region 이벤트
    public event System.Action BeaconCheck;
    #endregion

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if(state == VacuumState.None)
            VacuumMove();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            UPPER();
        }
    }

    #region 함수
    // 알맞은 칸을 지정하였을 때 위쪽에 대기하고 있을 경우에만 이동한다.
    public void VacuumMove()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnderLayer))
        {   
            StageBlockHandler block = hit.transform.GetComponent<StageBlockHandler>();
            //block이 오프중일 경우에만
            if (block.GetBlock().Item2 == 0)
            {
                BeaconCheck?.Invoke();
                block.BeaconOn();

                vacuumTranform.position = block.GetBlock().Item1 + new Vector3(0, height, 0);
                underTransform = block.transform;
            }
        }
    }

    // 청소기 하강
    public void VacuumFall()
    {
        if(state != VacuumState.None)
        {
            return;
        }
        if(underTransform != null)
        {   
            state = VacuumState.Fall;
            liftPos = vacuumTranform.position;

            vacuumTranform.DOMove(underTransform.position + (Vector3.up * 5), 3f).From(liftPos).OnComplete(() => VacuumLift());
        }
    }

    // 청소기 승강
    public void VacuumLift()
    {
        vacuumTranform.DOMove(liftPos, 2f).From(vacuumTranform.position).OnComplete(() => state = VacuumState.None);
    }
    #endregion

    #region
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<VacuumAbsorbHandler>() != null)
        {
            other.GetComponent<Rigidbody>().freezeRotation = false;
            other.GetComponent<VacuumAbsorbHandler>().State = 1;
            Spin(other.gameObject);

            var obj = Instantiate(other, basketPos.position, Quaternion.identity, basketPos);
            obj.gameObject.layer = basketPos.gameObject.layer;
            obj.GetComponent<Rigidbody>().useGravity = true;
            obj.GetComponent<Rigidbody>().freezeRotation = false;

            ForceTestList.Add(obj.gameObject);

            
        }
        //Destroy(other.gameObject);
    }

    // 실패하였을 때 탈출을 위한 힘
    public void UPPER()
    {
        foreach(GameObject g in ForceTestList)
        {
            Vector3 force =  new Vector3(Random.Range(-10, 10), 15, Random.Range(-10, 10));
            //g.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            g.GetComponent<Rigidbody>().AddForce(Vector3.up * 15, ForceMode.Impulse);
            g.GetComponent<VacuumAbsorbHandler>().State = 3;
        }
    }

    public void Spin(GameObject g)
    {
        g.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
        Vector3 force = new Vector3(0, 1000, 0);        
        g.GetComponent<Rigidbody>().AddTorque(force, ForceMode.Impulse);

        g.transform.DOScale(Vector3.zero, 4).OnComplete(() => Destroy(g));
    }
    #endregion
}
