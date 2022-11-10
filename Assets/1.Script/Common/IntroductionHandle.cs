using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://velog.io/@cedongne/Unity-2D-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EB%B2%94%EC%9C%84-%EC%A0%9C%ED%95%9C%ED%95%98%EA%B8%B0
//https://dalbitdorong.tistory.com/14
public class IntroductionHandle : MonoBehaviour
{
    [SerializeField]
    GameObject[] intro;
    [SerializeField]
    Transform pipe;
    List<Vector3> preSize = new();
    Vector3 spot1;
    [SerializeField]
    Camera subCam;
    void Awake()
    {
        print(Camera.main.transform.position);
        subCam.depth = 2;
        spot1 = new Vector3(subCam.transform.position.x - subCam.orthographicSize, subCam.transform.position.y, subCam.transform.position.z);
        print(spot1);
        for (int i = 0; i < intro.Length; i++)
        {
            preSize.Add(intro[i].transform.localScale);
            intro[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            StartCoroutine(Intro(i));
            
        }
    }
    //private void Update()
    //{
    //    for (int i = 0; i < intro.Length; i++)
    //    {
    //        intro[i].transform.position = Vector3.MoveTowards(intro[i].transform.position, pipe.position, 0.01f);
    //    }
    //}
    IEnumerator Intro(int index)
    {
        while (Vector3.Distance(intro[index].transform.position, spot1) > 0.4f)
        {
            intro[index].transform.position = Vector3.Lerp(intro[index].transform.position, spot1, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(intro[index].transform.position, spot1) <= 0.4f)
            {
                print("µµÂø?");
                break;
            }
        }
        intro[index].transform.position = spot1;
        //transform.Rotate(0, -70, 0);
        intro[index].transform.localScale = preSize[index];
        //intro[index].transform.position = Vector3.MoveTowards(intro[index].transform.position, pipe.position, 0.1f);
        StartCoroutine(GotoPipe(index));
        yield break;
    }
    IEnumerator GotoPipe(int index)
    {
        
        while (Vector3.Distance(subCam.transform.position, pipe.transform.position) > 0.4f)
        {
            subCam.transform.position = Vector3.Lerp(subCam.transform.position, pipe.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(subCam.transform.position, pipe.transform.position) <= 0.4f)
            {
                print("µµÂø?");
                break;
            }
        }
        subCam.transform.position = pipe.transform.position;
        //transform.Rotate(0, -70, 0);
        intro[index].transform.localScale = preSize[index];
        //intro[index].transform.position = Vector3.MoveTowards(intro[index].transform.position, pipe.position, 0.1f);
        subCam.depth = 0;
        yield break;
    }
}
