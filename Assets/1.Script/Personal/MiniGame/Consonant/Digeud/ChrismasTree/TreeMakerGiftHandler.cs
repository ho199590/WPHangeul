using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리에 메달리기 위한 오브젝트
public class TreeMakerGiftHandler : MonoBehaviour
{
    #region 변수
    TreeMakerTreeHandler tree;
    TreeMakerMovementController train;
    Transform sied;
    #endregion
    #region 프로퍼티
    // 0 정답 , 1 오답, 2 폭탄
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
    #region 함수
    // 정답 => 회전하며 달리기 + 1층 상승
    public void GiftForAnswer()
    {
        print("Answer");
        tree.CameraLift(1);

    }
    // 오답 => 오답 1층 하강
    public void GiftForWrong()
    {
        print("Wrong");
        tree.CameraLift(-1);

        //tree.GiftBear(gameObject);
    }
    // 폭탄 => 2층 하강
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
    #region 디버그
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            OperateInvoke();
        }
    }
    #endregion
}
