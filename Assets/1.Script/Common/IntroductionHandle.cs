using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//https://velog.io/@cedongne/Unity-2D-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EB%B2%94%EC%9C%84-%EC%A0%9C%ED%95%9C%ED%95%98%EA%B8%B0
//https://dalbitdorong.tistory.com/14
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
public class IntroductionHandle : MonoBehaviour
{
    [SerializeField]
    GameObject[] intro;
    [SerializeField]
    Transform pipe;
    [SerializeField]
    Camera subCam;

    Vector3 startPosition;
    List<Vector3> preSize = new();
    List<Vector3> prePosi = new();
    Vector3 viewPosi;
    float term = 0;
    int i = 0;
    float lerpTime = 3f; //도착까지 소요시간(총 진행시간)
    float currentTime = 0;
    float move;
    private void Awake()
    {
        subCam.depth = 2;
    }
    //void OnEnable()
    //{
    //    for (int i = 0; i < intro.Length; i++)
    //    {
    //        preSize.Add(intro[i].transform.localScale);
    //        prePosi.Add(intro[i].transform.position);
    //    }
    //}
    IEnumerator Start()
    {
        while(i < intro.Length)
        {
            print(i + "번째 오브젝트 출발");
            startPosition = intro[i].transform.position;
            viewPosi = new Vector3(subCam.transform.position.x - subCam.orthographicSize + term, subCam.transform.position.y, subCam.transform.position.z);
            term = term + 0.3f;
            StartCoroutine(Path1(i, startPosition ,viewPosi));
            i++;
            yield return new WaitForSeconds(2);
        }      
    }
    //void OnEnable()
    //{
    //    //Time.timeScale = 0;
    //    //EditorApplication.isPaused = true;
    //    subCam.depth = 2;
    //    for (int i = 0; i < intro.Length; i++)
    //    {
    //        viewPosi = new Vector3(subCam.transform.position.x - subCam.orthographicSize + term, subCam.transform.position.y, subCam.transform.position.z);
    //        term = term + 0.3f;
    //        preSize.Add(intro[i].transform.localScale);
    //        prePosi.Add(intro[i].transform.position);
    //        StartCoroutine(Path1(i, viewPosi));
    //    }
    //}

    IEnumerator Path1(int index, Vector3 start,Vector3 view)
    {
        print("path1코루틴");
        lerpTime = 3f;
        currentTime = 0;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            intro[index].transform.position = Vector3.Lerp(start, view, move);
            yield return null;
        }
        //transform.Rotate(0, -70, 0);
        //yield return Path2(index);
        //StartCoroutine(Path2(index));
    }
    IEnumerator Path2(int index)
    {
        startPosition = intro[index].transform.position;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            intro[index].transform.position = Vector3.Lerp(startPosition, pipe.transform.GetChild(1).transform.position, move);
            yield return null;
            subCam.transform.position = Vector3.MoveTowards(subCam.transform.position, pipe.GetChild(0).transform.position, 0.01f);
            intro[index].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        pipe.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        print("자기소개맨트 차례");
        //yield return Path3(index);
        //StartCoroutine(Path3(index));
        //Time.timeScale = 1.0f;
        //EditorApplication.isPaused = false;
    }
   
    IEnumerator Path3(int index)
    {
        startPosition = intro[index].transform.position;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            intro[index].transform.position = Vector3.Lerp(startPosition, pipe.transform.position, move);
            yield return null;
            intro[index].transform.localScale = preSize[index];
        }
        pipe.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
       // yield return Path4(index);
        //StartCoroutine(Path4(index));
    }
    IEnumerator Path4(int index)
    {
        startPosition = subCam.transform.position;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            subCam.transform.position = Vector3.Lerp(startPosition, Camera.main.transform.position, move);
            yield return null ;
        }
        intro[index].transform.position = prePosi[index];
        subCam.depth = 0;
    }
}
