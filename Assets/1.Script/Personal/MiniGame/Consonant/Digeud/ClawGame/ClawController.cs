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


// ���ӹڽ��� �����Ͽ� ��ü ������ �����ϱ� ���� ��Ʈ�ѷ�
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
            Operate = value switch
            {
                ClawState.None => DefaultSetting,
                ClawState.Move when state == ClawState.None => ClawMoveStart,
                ClawState.Fall when state == ClawState.Move => MagnetFall,
                _ => null,
            };

            state = value;
            Operate?.Invoke();
        }
    }
    //�ε��� ���..?
    public bool Collide
    {
        set
        {
            MagnetCollision?.Invoke();
        }
    }

    public System.Action Operate;

    List<Transform> magnetParts = new List<Transform>();

    // Ŭ�� ���� ���� | �ڼ� ���� ���Ʒ� ���� ���� 
    Ray moRay;
    RaycastHit RoofHit;
    RaycastHit UnderHit;

    // �ڼ� ������Ʈ | �ϴ� ������Ʈ | õ�� ������Ʈ
    public Transform magnetTransform;
    public Transform Cable;
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

    Rigidbody rig;
    #endregion

    #region �̺�Ʈ
    // �ڼ� �浹�� �߻��� �̺�Ʈ
    public event System.Action MagnetCollision;
    // �ڼ����� ��ü�� ����Ʈ�� �� �߻��� �̺�Ʈ
    public event System.Action MagnetPutDown;
    #endregion


    #region
    private void Awake()
    {
        rig = magnetTransform.GetComponent<Rigidbody>();

        State = ClawState.Move;

        MagnetCollision += MagnetLift;
        MagnetCollision += DefaultSetting;
        MagnetCollision += RelaxProduct;

        MagnetPutDown += RemovePart;
    }


    private void Update()
    {
        moRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            State = ClawState.Fall;
            //
            //MagnetFall();
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            MagnetLift();
        }

        if (Input.GetMouseButton(0))
        {
            ClawMove();
        }

        if (Input.GetMouseButtonDown(1))
        {
            MagnetPutDown?.Invoke();
        }
    }


    #region Catch & Release
    public void AddBodyPart(GameObject obj)
    {
        Transform newpart = obj.transform;
        magnetParts.Add(newpart);
    }

    public void RemovePart()
    {
        magnetParts.Clear();
    }
    #endregion

    #region None
    public void DefaultSetting()
    {
        mousePosMarker.gameObject.SetActive(false);
        RelaxProduct();
    }
    #endregion

    #region Move
    void ClawMove()
    {
        if (state == ClawState.None || state == ClawState.Move)
        {
            if (Physics.Raycast(moRay, out mousePosHit, Mathf.Infinity, UnderLayer))
            {
                State = ClawState.Move;
                mousePos = mousePosHit.point;

                Vector3 vec = new Vector3(mousePos.x, magnetTransform.position.y, mousePos.z);

                magnetTransform.position = vec;
                //Cable.position = new Vector3(mousePos.x, Cable.position.y, mousePos.z);
                mousePosMarker.position = new Vector3(mousePos.x, mousePos.y + mouseposOffsetFromGround, mousePos.z);
            }
        }
    }

    void ClawMoveStart()
    {
        mousePosMarker.gameObject.SetActive(true);
    }
    #endregion

    #region Fall
    void MagnetFall()
    {
        if (Physics.Raycast(magnetTransform.position, Vector3.up, out RoofHit, Mathf.Infinity, roofLayer))
        {
            roofTransform = RoofHit.transform;
        }


        rig.isKinematic = false;
        underTransform = null;
    }

    public void MagnetLift()
    {
        rig.isKinematic = true;
        if (roofTransform != null)
        {
            Vector3 vec3 = new Vector3(Cable.position.x, roofTransform.position.y - offsetY, Cable.position.z);
            magnetTransform.DOMove(vec3, 2f).OnComplete(() => state = ClawState.None);
        }

        roofTransform = null;
    }

    public void RelaxProduct()
    {
        foreach (Transform t in magnetParts)
        {
            t.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
    }
    #endregion
    #endregion
}
