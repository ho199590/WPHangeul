using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TreeMakerTreeHandler : MonoBehaviour
{
    #region 변수
    #region 카메라 관련 변수
    [SerializeField]
    Transform camera;
    [SerializeField]
    int sig = 1;

    Quaternion originRotate;
    Vector3 cameraOriginPos;
    Vector3 railOriginPos;
    Vector3 cameraCurPos;
    int num = 1;

    TreeMakerMovementController m_MovementController;
    #endregion
    #region 기프트 관련 변수
    [Header("기프트 관련 변수")]
    [SerializeField]
    Transform[] floors;
    [SerializeField]
    Transform floorLevel;
    public Transform FloorLevel
    {
        get { return floorLevel; }
        set
        {
            if (value != null)
            {
                floorSpot = value.GetComponentsInChildren<Transform>();
                floorLevel = value;
            }
        }
    }
    public Transform[] floorSpot;
    public int floorCount;

    [SerializeField]
    Transform catchGift;
    #endregion

    Tween CameraRotate;
    Tween CameraLiftTween;

    Ray ray;
    RaycastHit hit;
    [HideInInspector]
    public bool SiedLock;

    [SerializeField]
    Transform starObj;

    [SerializeField]
    Transform railTransform;


    public int count;
    #endregion
    #region 함수
    private void Awake()
    {
        camera = Camera.main.transform;

        originRotate = transform.localRotation;
        cameraOriginPos = camera.position;
        railOriginPos = railTransform.position;
        cameraCurPos = cameraOriginPos;

        m_MovementController = FindObjectOfType<TreeMakerMovementController>();
        m_MovementController.CameraTurn += TrunCamera;

        count = 0;
    }
    #region 카메라 관련 함수
    // 카메라 회전
    public void TrunCamera()
    {
        //CameraLiftTween.Complete();
        if (num == 1) { CameraRotate = transform.DORotate(new Vector3(0, -180, 0), 6, RotateMode.LocalAxisAdd).SetAutoKill(false); }
        else { CameraRotate = transform.DORotate(new Vector3(0, 180, 0), 6, RotateMode.LocalAxisAdd).SetAutoKill(false); }

        num *= -1;
    }
    // 카메라 상하 이동
    public void CameraLift(int num)
    {
        CameraRotate.Complete();

        Vector3 CameraTarget = new Vector3(camera.position.x, camera.position.y + (num * 3), camera.position.z);
        Vector3 RailPos = new Vector3(railTransform.position.x, railTransform.position.y + (num * 3), railTransform.position.z);


        if (count + num < 0)
        {
            CameraTarget = cameraOriginPos;
            RailPos = railOriginPos;

            count = 0;
        }
        else
        {
            count += num;
        }

        CameraLiftTween = camera.DOMove(CameraTarget, 1).From(camera.position).OnComplete(() => SiedLock = false) ;
        railTransform.DOMove(RailPos, 1).From(railTransform.position);
    }
    // 카메라 리셋
    public void CameraReset()
    {
        camera.DOKill();
        transform.DOKill();
        camera.DOMove(cameraOriginPos, 2);
        transform.DORotateQuaternion(originRotate, 2f);
        cameraCurPos = cameraOriginPos;

        floorCount = 0;
        count = 0;
    }
    #endregion
    #region 기프트 관련 함수
    // 최종 목적지 포인트
    public Transform GetFloorSpotPoint()
    {
        int ran = RandomNumberPicker.GetRandomNumberByNum(0, floorSpot.Length);
        Transform targetPoint = floorSpot[ran];

        return targetPoint;
    }

    public void GiftBear(GameObject g)
    {
        Vector3 ori = g.transform.localScale;
        Transform point = GetFloorSpotPoint();
        GameObject gg = Instantiate(g, point.position, Quaternion.identity, null);

        g.transform.DOScale(Vector3.zero, 1f).From(ori);
        gg.transform.DOScale(ori, 1f).From(Vector3.zero);

        g.GetComponent<TreeMakerGiftHandler>().Operate?.Invoke();

    }
    #endregion
    #endregion
    #region 충돌 관련 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TreeMakerGiftHandler>())
        {
            catchGift = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TreeMakerGiftHandler>())
        {
            catchGift = null;
        }
    }
    #endregion

    #region 트리 클릭 관련

    #endregion
    #region DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //TrunCamera();
            CameraLift(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CameraLift(-1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CameraReset();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (catchGift != null)
                    {
                        if (!SiedLock)
                        {
                            m_MovementController.RemoveBodyPart(catchGift);
                        }
                    }
                }
            }
        }

        FloorLevel = floors[floorCount];
    }
    #endregion

}
