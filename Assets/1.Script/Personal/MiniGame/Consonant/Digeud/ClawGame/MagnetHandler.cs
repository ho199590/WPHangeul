using UnityEngine;


// �ڼ� �κ� ��ũ��Ʈ
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

