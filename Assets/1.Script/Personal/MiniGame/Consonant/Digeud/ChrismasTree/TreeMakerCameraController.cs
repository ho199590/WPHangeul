using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeMakerCameraController : MonoBehaviour
{
    #region
    [SerializeField]
    Transform camera;

    [SerializeField]
    int sig;

    Quaternion originRotate;
    Vector3 cameraOriginPos;
    Vector3 cameraCurPos;
    int num = 1;

    TreeMakerMovementController m_MovementController;
    #endregion

    #region

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
    }
    private void Awake()
    {
        camera = Camera.main.transform;

        originRotate = transform.localRotation;
        cameraOriginPos = camera.position;
        cameraCurPos = cameraOriginPos;

        m_MovementController = FindObjectOfType<TreeMakerMovementController>();
        m_MovementController.CameraTurn += TrunCamera;

    }

    public void TrunCamera()
    {
        if (num == 1) { transform.DORotate(new Vector3(0, -180, 0), 6, RotateMode.LocalAxisAdd); }
        else { transform.DORotate(new Vector3(0, 180, 0), 6, RotateMode.LocalAxisAdd); }

        num *= -1;
    }

    public void CameraLift(int num)
    {
        Vector3 Target = new Vector3(camera.position.x, camera.position.y + num, camera.position.z);
        camera.DOKill();
        camera.DOMove(Target, 1f).From(camera.position);
        cameraCurPos = Target;
    }

    public void CameraReset()
    {
        camera.DOKill();
        transform.DOKill();
        camera.DOMove(cameraOriginPos, 2);
        transform.DORotateQuaternion(originRotate, 2f);
        cameraCurPos = cameraOriginPos;
    }
    #endregion
}
