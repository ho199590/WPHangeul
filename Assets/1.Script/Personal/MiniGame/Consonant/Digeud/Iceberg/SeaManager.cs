using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메쉬 출렁거림
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SeaManager : MonoBehaviour
{
    #region 변수
    WaveManager wave;
    MeshFilter meshFilter;
    #endregion

    private void Awake()
    {
        wave = GetComponent<WaveManager>();
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = wave.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }

}
