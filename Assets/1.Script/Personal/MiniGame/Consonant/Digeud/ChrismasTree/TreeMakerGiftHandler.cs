using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ʈ���� �޴޸��� ���� ������Ʈ
public class TreeMakerGiftHandler : MonoBehaviour
{
    #region ����
    TreeMakerTreeHandler tree;
    TreeMakerMovementController train;
    Transform sied;
    #endregion
    #region ������Ƽ
    // 0 ���� , 1 ����, 2 ��ź
    public int giftProperty;
    public int GiftProperty
    {
        set
        {
            Operate = value switch
            {
                0 => GiftForAnswer,
                1 => GiftForWrong,
                2 => GiftForBomb,
                _ => null
            };
            giftProperty = value;
        }
    }
    public System.Action Operate;
    #endregion
    #region �Լ�
    // ���� => ȸ���ϸ� �޸��� + 1�� ���
    public void GiftForAnswer()
    {
        print("Answer");
        tree.CameraLift(1);

    }
    // ���� => ���� 1�� �ϰ�
    public void GiftForWrong()
    {
        print("Wrong");
        tree.CameraLift(-1);

        //tree.GiftBear(gameObject);
    }
    // ��ź => 2�� �ϰ�
    public void GiftForBomb()
    {
        print("BOMB");
        tree.CameraLift(-2);
    }

    public void OperateInvoke()
    {
        Operate?.Invoke();
    }

    public void OnEnable()
    {
        tree = FindObjectOfType<TreeMakerTreeHandler>();
        train = FindObjectOfType<TreeMakerMovementController>();
    }
    #endregion
    #region �����
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            OperateInvoke();
        }
    }
    #endregion
}
