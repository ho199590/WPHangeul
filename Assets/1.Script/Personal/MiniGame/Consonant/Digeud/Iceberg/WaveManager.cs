using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//파도 구현
public class WaveManager : MonoBehaviour
{
    #region 변수
    [SerializeField]
    float Debug;
    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;
    #endregion

    private void Update()
    {   

        offset += Time.deltaTime * speed;
        Debug = amplitude * Mathf.Sin(1 / length + offset);
    }

    public float GetWaveHeight(float _x)
    {
        
        return amplitude * Mathf.Sin(_x / length + offset);
    }
}
