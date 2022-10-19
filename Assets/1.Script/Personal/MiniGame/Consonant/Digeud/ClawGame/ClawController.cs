using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ClawState
{   
    // None => Fall , None => Move , Move => None , Fall => None �� �����ϰ� �����
    None,  // ������      =>  õ������ �ڼ��� �ö����
    Move,  // �����̴� ��� =>  �ϰ��� ������ õ�忡 �پ��ֱ�
    Fall,  // ��� �õ�     =>  ���Ϸ� �����̸� �������� ����
    End
}

public class ClawController : MonoBehaviour
{
    #region ����
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

    // Ŭ�� ���� ���� | �ڼ� ���� ���Ʒ� ���� ���� 
    Ray moRay;
    RaycastHit RoofHit;
    RaycastHit UnderHit;

    // �ڼ� ������Ʈ | �ϴ� ������Ʈ | õ�� ������Ʈ
    public Transform magnetTransform;
    Transform roofTransform;
    Transform underTransform;

    // ��������
    public Vector3 StartPoint;

    // ���̾� ����ũ
    public LayerMask roofLayer;
    public LayerMask UnderLayer;

    // ���콺 ��Ŀ ���� ������
    public Transform mousePosMarker;
    RaycastHit mousePosHit;
    // �ڼ��� ����
    public float offsetY = 0;
    // ��Ŀ�� ����
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
