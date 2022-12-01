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


    Ray ray;
    RaycastHit hit;

    public bool SiedLock;

    #endregion
    #region 함수
    private void Awake()
    {
        camera = Camera.main.transform;

        originRotate = transform.localRotation;
        cameraOriginPos = camera.position;
        cameraCurPos = cameraOriginPos;

        m_MovementController = FindObjectOfType<TreeMakerMovementController>();
        m_MovementController.CameraTurn += TrunCamera;
    }
    #region 카메라 관련 함수
    // 카메라 회전
    public void TrunCamera()
    {
        if (num == 1) { transform.DORotate(new Vector3(0, -180, 0), 6, RotateMode.LocalAxisAdd); }
        else { transform.DORotate(new Vector3(0, 180, 0), 6, RotateMode.LocalAxisAdd); }

        num *= -1;
    }
    // 카메라 상하 이동
    public void CameraLift(int num)
    {
        if((floorCount + num) < 0){
            num = floorCount + num;
            floorCount = 0;            
        }
        Vector3 Target = new Vector3(camera.position.x, camera.position.y + num, camera.position.z);
        camera.DOKill();
        camera.DOMove(Target, 1f).From(camera.position);
        cameraCurPos = Target;

        if (floorCount + num < floors.Length && floorCount + num >= 0)
        {
            floorCount += num;
        }
    }
    // 카메라 리셋
    public void CameraReset()
    {
        print("TEST!");
        camera.DOKill();
        transform.DOKill();
        camera.DOMove(cameraOriginPos, 2);
        transform.DORotateQuaternion(originRotate, 2f);
        cameraCurPos = cameraOriginPos;

        floorCount = 0;
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
            if(Physics.Raycast(ray, out hit))
            {   
                if(hit.transform == transform)
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
