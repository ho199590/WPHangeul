using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//https://velog.io/@cedongne/Unity-2D-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EB%B2%94%EC%9C%84-%EC%A0%9C%ED%95%9C%ED%95%98%EA%B8%B0
//https://dalbitdorong.tistory.com/14
public class IntroductionHandle : MonoBehaviour
{
    [SerializeField]
    GameObject[] intro;
    [SerializeField]
    Transform pipe;
    [SerializeField]
    Camera subCam;
    List<Vector3> preSize = new();
    List<Vector3> prePosi = new();
    Vector3 viewPosi;
    float term = 0;
    void OnEnable()
    {
        //Time.timeScale = 0;
        //EditorApplication.isPaused = true;
        subCam.depth = 2;
        for (int i = 0; i < intro.Length; i++)
        {
            viewPosi = new Vector3(subCam.transform.position.x -subCam.orthographicSize + term, subCam.transform.position.y, subCam.transform.position.z);
            term = term + 0.4f;
            preSize.Add(intro[i].transform.localScale);
            prePosi.Add(intro[i].transform.position);
            StartCoroutine(Path1(i, viewPosi));
        }
    }
    IEnumerator Path1(int index, Vector3 view)
    {
        while (Vector3.Distance(intro[index].transform.position, view) > 0.4f)
        {
            intro[index].transform.position = Vector3.Lerp(intro[index].transform.position, view, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(intro[index].transform.position, view) <= 0.4f)
            {
                break;
            }
        }
        intro[index].transform.position = view;
        //transform.Rotate(0, -70, 0);
        StartCoroutine(Path2(index));
        yield break;
    }
    IEnumerator Path2(int index)
    {
        while (Vector3.Distance(intro[index].transform.position, pipe.transform.GetChild(1).transform.position) > 0.4f)
        {
            intro[index].transform.position = Vector3.Lerp(intro[index].transform.position, pipe.transform.GetChild(1).transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(intro[index].transform.position, pipe.transform.GetChild(1).transform.position) <= 0.4f)
            {
                break;
            }
            subCam.transform.position = Vector3.MoveTowards(subCam.transform.position, pipe.GetChild(0).transform.position, 0.01f);
            intro[index].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        intro[index].transform.position = pipe.transform.GetChild(1).transform.position;
        pipe.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        print("자기소개맨트 차례");
        StartCoroutine(Path3(index));
        //Time.timeScale = 1.0f;
        //EditorApplication.isPaused = false;
        yield break;
    }
   
    IEnumerator Path3(int index)
    {
        while (Vector3.Distance(intro[index].transform.position, pipe.transform.position) > 0.4f)
        {
            intro[index].transform.position = Vector3.Lerp(intro[index].transform.position, pipe.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(intro[index].transform.position, pipe.transform.position) <= 0.4f)
            {
                break;
            }
            intro[index].transform.localScale = preSize[index];
        }
        intro[index].transform.position = pipe.transform.position;
        pipe.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(Path4(index));
        yield break;
    }
    IEnumerator Path4(int index)
    {
        while (Vector3.Distance(subCam.transform.position, Camera.main.transform.position) > 0.4f)
        {
            subCam.transform.position = Vector3.Lerp(subCam.transform.position, Camera.main.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(subCam.transform.position, Camera.main.transform.position) <= 0.4f)
            {
                break;
            }
        }
        intro[index].transform.position = prePosi[index];
        subCam.transform.position = Camera.main.transform.position;
        subCam.depth = 0;
        yield break;
    }
}
