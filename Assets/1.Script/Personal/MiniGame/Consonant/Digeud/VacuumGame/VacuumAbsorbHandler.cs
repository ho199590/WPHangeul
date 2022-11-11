using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumAbsorbHandler : MonoBehaviour
{
    #region 변수
    // state 0 => 대기
    // state 1 => 흡입
    // state 2 => 정답
    // state 3 => 폭발
    int state;
    public int State
    {
        get { return state; }
        set
        {
            Operate = value switch
            {
                1 => AbsorbMovemont,
                2 => CompleteMovemont,
                3 => BombMovemont,
                _ => null,
            };
            state = value;
            Operate?.Invoke();
        }

    }
    public System.Action Operate;

    // 정답 => 0, 오답 => 1
    [SerializeField]
    int result;
    [SerializeField]
    GameObject[] effectParticles;
    #endregion

    #region 함수
    // 배치
    public void ProductInit(int num)
    {
        result = num;
        if (result == 0)
        {
            transform.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.25f);
        }
        else if (result == 1)
        {
            transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.25f);
        }
        State = 0;
    }

    // 빨아들여 지는 행동 => 공통 state 1
    public void AbsorbMovemont()
    {
        print("흡입");
    }

    // 정답시 행동 => state 2
    public void CompleteMovemont()
    {

    }

    // 폭발 => 오답 state 3
    public void BombMovemont()
    {
        print("폭발");
    }

    #endregion
}
