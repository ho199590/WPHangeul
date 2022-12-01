using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TreeMakerTreeHandler : MonoBehaviour
{
    #region ����
    #region ī�޶� ���� ����
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
    #region ����Ʈ ���� ����
    [Header("����Ʈ ���� ����")]
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
    #region �Լ�
    private void Awake()
    {
        camera = Camera.main.transform;

        originRotate = transform.localRotation;
        cameraOriginPos = camera.position;
        cameraCurPos = cameraOriginPos;

        m_MovementController = FindObjectOfType<TreeMakerMovementController>();
        m_MovementController.CameraTurn += TrunCamera;
    }
    #region ī�޶� ���� �Լ�
    // ī�޶� ȸ��
    public void TrunCamera()
    {
        if (num == 1) { transform.DORotate(new Vector3(0, -180, 0), 6, RotateMode.LocalAxisAdd); }
        else { transform.DORotate(new Vector3(0, 180, 0), 6, RotateMode.LocalAxisAdd); }

        num *= -1;
    }
    // ī�޶� ���� �̵�
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
    // ī�޶� ����
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
    #region ����Ʈ ���� �Լ�
    // ���� ������ ����Ʈ
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
    #region �浹 ���� �Լ�
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

    #region Ʈ�� Ŭ�� ����

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
