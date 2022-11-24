using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
//https://wergia.tistory.com/59
public class LoadingTutorialManager : MonoBehaviour
{
    [SerializeField]
    TutorialObjects tutorialObjects; //튜토리얼에 등장할 오브젝트들을 가지고 있는 Scriptable Object
    Vector3 startposition; //Lerp에서 사용할 시작위치 고정용으로 첫 위치를 담아둘 변수
    [SerializeField] 
    GameObject[] objects; //없어질 예정
    [SerializeField]
    Transform pipe; //파이프 밑에 Lerp의 도착위치
    [SerializeField]
    GameObject[] objectPosi;
   
    public Transform arrive; //Lerp의 도착위치
    float lerpTime; //Lerp의 도착때까지 총 소요시간
    float currentTime; //등속운동을 위한 델타타임 저장용

    int i = 0;
    float term = 0; //오브젝트별로 일렬로 세우기 위한 Lerp의 도착위치 간격 조절용
    float plusTime = 3; //오브젝트별로 일렬로 세우기 위한 Lerp의 속도 조절용

    public static string nextScene; //씬재생 지연함수가 끝나면(튜토리얼이 끝나면) 재생할 씬의 이름 저장용
    string nameCheck;
    

    //공부용
    //Animator anim;
    /*    private void OnCollisionEnter(Collision collision)
        {
            print(collision.contacts[0].point); //충돌체 위치 포인트?
        }
    /*    private void sh()
        {
            anim.SetInteger(anim.GetComponent<Animator>().GetParameter(0).name,1); //애니메이터 파라미터 값 가져오기
        }*/

    //씬호출시 LoadingTutorialManager.LoadScene("호출할 씬의 이름");로 참조
    //객체 생성없이 바로 갖다 쓸수 있는 정적(static) 함수
    public static void LoadScene(string sceneName)
    {
        print("씬이름 체크:" + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene("Tutorial");
    }
    //씬 재생 전에 튜토리얼 먼저 재생해주는 비동기식 재생 지연 함수
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;
        //float timer = 0f;
        while (!async.isDone)
        {
            yield return null;
            //timer += Time.deltaTime;
            //if (async.progress >= 0.9f)
            //{
                //yield return new WaitForSeconds(10); //90%진행 체크라 100%진행율을 위한 추가 10초
                async.allowSceneActivation = true;
            //}
        }
    }
    private void Start()
    {
        tutorialObjects.Insert();
        //print(nextScene.Split('_')[0]); //소괄호안에 언더바를 기준으로 string값 쪼개기
        if(tutorialObjects.GetIndex().TryGetValue(nextScene.Split('_')[0], out IntroductionObjects[] check))
        {
           
            for(int i = 0; i < check.Length; i++)
            {
                print(check[i].Object);
                objectPosi[i].transform.position = check[i].Object.transform.position;
                objectPosi[i] = check[i].Object;
                startposition = objectPosi[i].transform.position; //시작위치 고정용
                StartCoroutine(Move1(i, startposition, term, plusTime));
                term += 3f; //일렬 간격 조정용
                plusTime += 5f; //동시에 움직임으로 속도 조절로 일렬 유지
            }
        }

        ////StartCoroutine(Begin());
        ////동시에 Lerp로 움직이게 하기 위해 for문으로 빠르게 돌려서 코루틴 호출하기 위한 것
        //for (int i = 0; i < objectPosi.Length; i++)
        //{
        //    startposition = objectPosi[i].transform.position; //시작위치 고정용
        //    StartCoroutine(Move1(i, startposition, term, plusTime));
        //    term += 3f; //일렬 간격 조정용
        //    plusTime += 5f; //동시에 움직임으로 속도 조절로 일렬 유지
        //}
    }
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(startposition, arrive.position, currentTime / lerpTime); //등속
    //    //transform.position = Vector3.Lerp(startposition, endposition, Mathf.SmoothStep(0,1,currentTime / lerpTime)); //감속
    //    //transform.position = Vector3.Lerp(startposition, endposition, curve.Evaluate(currentTime / lerpTime)); //애니메이션커브로 속도 조절
    //}

    //코루틴 호출을 지연(for문이랑 다르게 3초 간격으로 코루틴을 천천히 호출하기 위한 지연 함수)
    IEnumerator Begin()
    {
        while(i < objectPosi.Length) //오브젝트 갯수만큼만 반복
        {
            startposition = objectPosi[i].transform.position;
            //StartCoroutine(Move1(i, startposition, term));
            i++;
            term += 3f;
            yield return new WaitForSeconds(3); //3초 기다리기
        }
        //StartCoroutine(MoveOrdered());
    }
    IEnumerator Move1(int index, Vector3 startPosi, float term, float plusLerpTime)
    {
        lerpTime = plusLerpTime;
        currentTime = 0;
        while ((currentTime / lerpTime) <= 1) //Lerp의 등속운동
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(startPosi, new Vector3(arrive.position.x + term, arrive.position.y, arrive.position.z), currentTime / lerpTime);
            yield return null;
        }
        yield return new WaitUntil(() => index == objectPosi.Length - 1); //괄호안에 조건이 true가 될 때 밑에 줄 실행
        StartCoroutine(MoveOrdered());
    }
    //코루틴 호출을 지연
    IEnumerator MoveOrdered()
    {
        i = 0;
        while (i < objects.Length)
        {
            print(i + "번째 오브젝트 출발");
            startposition = objectPosi[i].transform.position;
            yield return StartCoroutine(Move3(i, startposition)); //Move3코루틴이 끝나야지 밑에 줄 실행
            yield return new WaitForSeconds(6); //여기서 소개멘트 (플레이되는 시간만큼 지연)
            yield return StartCoroutine(Move4(i)); //Move4코루틴이 끝나야지 밑에 줄 실행
            i++;
        }
    }
    IEnumerator Move3(int index, Vector3 startPosi)
    {
        lerpTime = 2f;
        currentTime = 0;
        while ((currentTime / lerpTime) <= 1)
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(startPosi, pipe.position, currentTime / lerpTime);
            yield return null;
        }
    }
    IEnumerator Move4(int index)
    {
        lerpTime = 0.5f;
        currentTime = 0;
        while ((currentTime / lerpTime) <= 1)
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(pipe.position, new Vector3(pipe.position.x, pipe.position.y + 5, pipe.position.z), currentTime / lerpTime);
            yield return null;
        }
        if (index == objectPosi.Length - 1) 
        {
            StopAllCoroutines();
            //StartCoroutine(LoadScene()); 
            print("코루틴끝나는지 체크");
        }
    }
}
