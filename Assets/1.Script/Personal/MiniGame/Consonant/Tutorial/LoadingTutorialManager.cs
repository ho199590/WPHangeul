using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
//https://wergia.tistory.com/59

//��ȣ��� LoadingTutorialManager.LoadScene("ȣ���� ���� �̸�"); ���� �߰�

public class LoadingTutorialManager : MonoBehaviour
{
    [SerializeField]
    TutorialObjects tutorialObjects;
    Vector3 endposition = new Vector3(5, -2, 0);
    Vector3 startposition;
    [SerializeField]
    GameObject[] objects;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    Transform pipe;
   
    public Transform arrive;
    float lerpTime;
    float currentTime;
    float move;

    int i = 0;
    float term = 0;
    bool check = false;

    public static string nextScene;
    
    //Animator anim;
    public static void LoadScene(string sceneName)
    {
        print("���̸� üũ:" + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene("Tutorial");
    }
    //�� ��� ���� Ʃ�丮�� ���� ������ִ� �񵿱�� ��� ���� �Լ�
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
                yield return new WaitForSeconds(10); //90%���� üũ�� 100%�������� ���� �߰� 10��
                async.allowSceneActivation = true;
            }
        }
    }
    private void Start()
    {
        //for(i = 0 ; i < tutorialObjects.SaveObjects.Length; i++)
        print(tutorialObjects.GiyeokObjects[0].Object);
        print(tutorialObjects.GiyeokObjects[0].perAudio);
        print(tutorialObjects.GiyeokObjects.Length);
        print(tutorialObjects.GiyeokObjects.GetValue(0));
        //print(IntroductionObjects.);

        StartCoroutine(Begin());
    }

    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(startposition, arrive.position, currentTime / lerpTime);
    //    //transform.position = Vector3.Lerp(startposition, endposition, Mathf.SmoothStep(0,1,currentTime / lerpTime));
    //    //transform.position = Vector3.Lerp(startposition, endposition, curve.Evaluate(currentTime / lerpTime));
    //}

    /*    private void OnCollisionEnter(Collision collision)
        {
            print(collision.contacts[0].point);
        }
    /*    private void sh()
        {
            anim.SetInteger(anim.GetComponent<Animator>().GetParameter(0).name,1);
        }*/

    //IEnumerator Delay()
    //{
    //    while(i < objects.Length)
    //    {
    //        startposition = objects[i].transform.position;
    //        StartCoroutine(Move1(i, startposition, term));
    //        i++;
    //        term += 3f;
    //        yield return new WaitForSeconds(3); //������Ʈ�� ���� ����
    //    }
    //}

    //�ڷ�ƾ ȣ���� ����
    IEnumerator Begin()
    {
        while(i < objects.Length) //������
        {
            startposition = objects[i].transform.position;
            StartCoroutine(Move1(i, startposition, term, check));
            i++;
            if(i == objects.Length) check = true;
            term += 3f;
            yield return new WaitForSeconds(3);
        }
        StartCoroutine(MoveOrdered());
    }
    IEnumerator Move1(int index, Vector3 startPosi, float term, bool check)
    {
        lerpTime = 2f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            objects[index].transform.position = Vector3.Lerp(startPosi, new Vector3(arrive.position.x + term, arrive.position.y, arrive.position.z), currentTime / lerpTime);
            yield return null;
        }
    }
    //�ڷ�ƾ ȣ���� ����
    IEnumerator MoveOrdered()
    {
        i = 0;
        while (i < objects.Length) //������
        {
            print(i + "��° ������Ʈ ���");
            startposition = objects[i].transform.position;
            StartCoroutine(Move3(i, startposition));
            yield return new WaitForSeconds(4); //���⼭ �Ұ���Ʈ (�÷��̵Ǵ� �ð���ŭ ����)
            StartCoroutine(Move4(i));
            i++;
        }
    }
    IEnumerator Move3(int index, Vector3 startPosi)
    {
        lerpTime = 3f;
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
        //StartCoroutine(LoadScene()); 
    }
}
