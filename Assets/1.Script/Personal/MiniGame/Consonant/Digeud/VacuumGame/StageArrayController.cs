using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState{
    Stage, // 바닥
    Roof,  // 천장
    Wall
}

// 스테이지 만들기
public class StageArrayController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    BlockState state;

    [SerializeField]
    int count;
    [SerializeField]
    float size;

    [SerializeField]
    GameObject[] block;

    #endregion

    #region 테스트 함수
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MakeStage();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetStage();
        }
    }
    #endregion
    #region 함수

    public void MakeStage()
    {
        ResetStage();
        GameObject obj = state switch
        {
            BlockState.Stage => block[0],
            BlockState.Roof => block[1],
            BlockState.Wall => block[2],
            _ => null
        };

        float alpha = (state == BlockState.Roof) ? 0.01f : 1f;        
        for (int i = 0; i < Mathf.Pow(count, 2); i++)
        {
            Vector3 Pos = new Vector3((i % count) * size, 0, i / count * size);
            var stage = Instantiate(obj, transform.position + Pos, Quaternion.identity, transform);            
            stage.transform.GetChild(0).localScale *= size;
            stage.name = i.ToString();

            
            stage.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), alpha);
        }
    }

    public void ResetStage()
    {
        if (transform.childCount < 1) { return; }
        Transform[] childList = GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }
    #endregion
}
