using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리에 메달리기 위한 오브젝트
public class TreeMakerGiftHandler : MonoBehaviour
{
    #region 변수
    
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

    }
    // 오답 => 오답 1층 하강
    public void GiftForWrong()
    {

    }
    // 폭탄 => 2층 하강
    public void GiftForBomb()
    {

    }
    #endregion

    #region 충돌 관련
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TreeMakerGiftHandler>() != null) { return; }

    }
    #endregion
    
}
