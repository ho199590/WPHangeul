using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
//https://wergia.tistory.com/59
public class MoveHandle : MonoBehaviour
{
    Vector3 endposition = new Vector3(5, -2, 0);
    Vector3 startposition;
    [SerializeField]
    GameObject[] objects;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    Transform pipe;
    [SerializeField]
    GameObject mask;
    public Transform arrive;
    public Transform camPosi;

    float lerpTime;
    float currentTime;
    float move;

    int i = 0;
    float term = 0;
    [SerializeField]
    Camera cam;

    bool check = true;

    public static string nextScene;
    private void Start()
    {
        StartCoroutine(Begin());
    }
    IEnumerator Begin()
    {
        while(i < objects.Length)
        {
            print(i + "번째 오브젝트 출발");
            startposition = objects[i].transform.position;
            StartCoroutine(Move1(i, startposition, term));
            //if (check) 
            //{
            //    print("한번만나오는지 체크");
            //    check = false;
            //    StartCoroutine(Move2());
            //}
            i++;
            term += 3f;
            yield return new WaitForSeconds(3);
        }
        StartCoroutine(Move2());
    }
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(startposition, arrive.position, currentTime / lerpTime);
    //    //transform.position = Vector3.Lerp(startposition, endposition, Mathf.SmoothStep(0,1,currentTime / lerpTime));
    //    //transform.position = Vector3.Lerp(startposition, endposition, curve.Evaluate(currentTime / lerpTime));
    //}

    IEnumerator Move1(int index, Vector3 startPosi, float term)
    {
        lerpTime = 2f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            print(objects[index].gameObject.name + "이동");
            objects[index].transform.position = Vector3.Lerp(startPosi, new Vector3(arrive.position.x + term, arrive.position.y, arrive.position.z), currentTime / lerpTime);
            yield return null;
        }
        print("코루틴 끝");
    }
    IEnumerator Move2()
    {
        cam.depth = 2;
        startposition = cam.transform.position;
        lerpTime = 3f;
        currentTime = 0;
        while (move < 1)
        {
            currentTime += Time.deltaTime;
            move = currentTime / lerpTime;
            cam.transform.position = Vector3.Lerp(startposition, camPosi.position, move);
            yield return null;
        }
        StartCoroutine(MoveOrdered());
    }
    IEnumerator MoveOrdered()
    {
        i = 0;
        while (i < objects.Length)
        {
            print(i + "번째 오브젝트 출발");
            startposition = objects[i].transform.position;
            StartCoroutine(Move3(i, startposition));
            yield return new WaitForSeconds(4); //여기서 소개멘트 (플레이되는 시간만큼 지연)
            StartCoroutine(Move4(i));
            i++;
        }
    }
    IEnumerator Move3(int index, Vector3 startPosi)
    {
        mask.SetActive(true);
        lerpTime = 2f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            objects[index].transform.position = Vector3.Lerp(startPosi, pipe.position, currentTime / lerpTime);
            yield return null;
        }
    }
    IEnumerator Move4(int index)
    {
        lerpTime = 5f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            objects[index].transform.position = Vector3.Lerp(pipe.position, new Vector3(pipe.position.x, pipe.position.y + 5, pipe.position.z), currentTime / lerpTime);
            yield return null;
        }
        StartCoroutine(LoadScene());
    }
    //-----------------------
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Tutorial");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;
        float timer = 0f;
        while (!async.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (async.progress >= 0.9f)
            {
                yield return new WaitForSeconds(10); //90%진행 체크라 100%진행율을 위한 추가 10초
                async.allowSceneActivation = true;
            }
        }
    }
}
