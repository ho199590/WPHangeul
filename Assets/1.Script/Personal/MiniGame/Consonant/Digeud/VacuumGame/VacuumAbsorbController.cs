using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VacuumAbsorbController : MonoBehaviour
{
    #region ����
    // state 0 => ���
    // state 1 => ����
    // state 2 => ����
    // state 3 => ����
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

    // ���� => 0, ���� => 1
    [SerializeField]
    public int result;
    [SerializeField]
    GameObject[] effectParticles;
    #endregion

    #region �Լ�
    // ��ġ
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

    // ���Ƶ鿩 ���� �ൿ => ���� state 1
    public void AbsorbMovemont()
    {
        Vector3 force = new Vector3(0, 1000, 0);
        Vector3 oriScale = transform.localScale;
        GetComponent<Rigidbody>().AddTorque(force, ForceMode.Impulse);


    }

    // ����� �ൿ => state 2
    public void CompleteMovemont()
    {
        
    }

    // ���� => ���� state 3
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
