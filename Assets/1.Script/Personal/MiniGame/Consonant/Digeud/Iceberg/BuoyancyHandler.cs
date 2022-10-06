using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BuoyancyHandler : MonoBehaviour
{
    #region ����
    public Transform[] floaters;

    public float underWaterDrag = 3;
    public float underWaterAngularDrag = 1;
    public float airDrag = 0;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;

    public float waterHeight = 0f;

    Rigidbody m_rigidbody;

    bool underwater;

    int floatersUnderwater; 
    #endregion

    #region �Լ�
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        floatersUnderwater = 0;
        for(int i = 0; i < floaters.Length; i++)
        {
            float difference = floaters[i].position.y - waterHeight;
            if (difference < 0)
            {
                m_rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderwater += 1;

                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }
       if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(false );
        }
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            m_rigidbody.drag = underWaterDrag;
            m_rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_rigidbody.drag = airDrag;
            m_rigidbody.angularDrag = airAngularDrag;
        }
    }

    #endregion
}
