using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ClawState
{   
    // None => Fall , None => Move , Move => None , Fall => None 만 가능하게 만들기
    None,  // 통상상태      =>  천장으로 자석이 올라오기
    Move,  // 움직이는 경우 =>  하강을 막으며 천장에 붙어있기
    Fall,  // 잡기 시도     =>  상하로 움직이며 움직임을 막기
    End
}

public class ClawController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    ClawState state;
    public ClawState State
    {
        get => state;
        set
        {
            
        }
    }
    public System.Action Operate;

    // 클릭 지점 레이 | 자석 기준 위아래 방향 레이 
    Ray moRay;
    RaycastHit RoofHit;
    RaycastHit UnderHit;

    // 자석 오브젝트 | 하단 오브젝트 | 천장 오브젝트
    public Transform magnetTransform;
    Transform roofTransform;
    Transform underTransform;

    // 시작지점
    public Vector3 StartPoint;

    // 레이어 마스크
    public LayerMask roofLayer;
    public LayerMask UnderLayer;

    // 마우스 마커 관련 설정들
    public Transform mousePosMarker;
    RaycastHit mousePosHit;
    // 자석의 높이
    public float offsetY = 0;
    // 마커의 높이
    public float mouseposOffsetFromGround = 0;
    public Vector3 mousePos;

    Rigidbody rigidbody;    
    #endregion


    #region

    private void Start()
    {   
        //magnetTransform.localPosition = StartPoint;
        rigidbody = magnetTransform.GetComponent<Rigidbody>();        
    }

    private void Update()
    {
        moRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            MagnetFall();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MagnetLift();
        }
    }

    #region None

    #endregion

    #region Move
    void ClawMove()
    {
        if (Physics.Raycast(moRay, out mousePosHit, Mathf.Infinity, UnderLayer))
        {
            mousePos = mousePosHit.point;

            Vector3 vec = new Vector3(mousePos.x, magnetTransform.position.y, mousePos.z);

            mousePosMarker.position = new Vector3(mousePos.x, mousePos.y + mouseposOffsetFromGround, mousePos.z);
        }
    }
    #endregion

    #region Fall
    void MagnetFall()
    {
        if (Physics.Raycast(magnetTransform.position, Vector3.up, out RoofHit, Mathf.Infinity, roofLayer))
        {
            roofTransform = RoofHit.transform;
        }

        rigidbody.isKinematic = false;
        underTransform = null;
    }

    void MagnetLift()
    {
        rigidbody.isKinematic = true;
        magnetTransform.DOMoveY(roofTransform.position.y - offsetY, 1f).OnComplete(()=> state = ClawState.None);

        roofTransform = null;
    }
    #endregion

    #endregion
}
