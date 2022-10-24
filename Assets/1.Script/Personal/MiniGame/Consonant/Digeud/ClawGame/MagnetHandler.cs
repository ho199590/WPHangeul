using UnityEngine;


// 자석 부분 스크립트
public class MagnetHandler : MonoBehaviour
{
    #region
    ClawController clawController;
    #endregion

    #region
    private void Start()
    {
        clawController = FindObjectOfType<ClawController>();
    }

    private void OnCollisionEnter(Collision collision)
    { 
        clawController.MagnetCollisionInvoke();
    }
    #endregion
}

