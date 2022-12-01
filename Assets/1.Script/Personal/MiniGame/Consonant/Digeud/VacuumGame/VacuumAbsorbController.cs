using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VacuumAbsorbController : MonoBehaviour
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
    public int result;
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
        Vector3 force = new Vector3(0, 1000, 0);
        Vector3 oriScale = transform.localScale;
        GetComponent<Rigidbody>().AddTorque(force, ForceMode.Impulse);


    }

    // 정답시 행동 => state 2
    public void CompleteMovemont()
    {
        
    }

    // 폭발 => 오답 state 3
    public void BombMovemont()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 20, ForceMode.Impulse);
        if (effectParticles.Length > 0)
        {   
            var effect = Instantiate(effectParticles[0], transform.position, Quaternion.identity, null);
            effect.gameObject.layer = gameObject.layer;            
        }
    }

    #endregion
}
