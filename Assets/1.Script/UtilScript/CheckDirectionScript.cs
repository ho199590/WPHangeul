using UnityEngine;


//�Էµ� ���͸� �̿��Ͽ� ���⼺���� üũ
public class CheckDirectionScript : MonoBehaviour
{
    #region �̺�Ʈ
    public System.Func<Vector2, Vector2, bool> CheckDir;
    #endregion

    private void Awake()
    {
        CheckDir += (Vector2 a, Vector2 b) =>
        {
            return Vector2.Dot(a.normalized, b.normalized) > 0;
        };
    }
}
