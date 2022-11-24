using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
//https://wergia.tistory.com/59
public class LoadingTutorialManager : MonoBehaviour
{
    [SerializeField]
    TutorialObjects tutorialObjects; //Ʃ�丮�� ������ ������Ʈ���� ������ �ִ� Scriptable Object
    Vector3 startposition; //Lerp���� ����� ������ġ ���������� ù ��ġ�� ��Ƶ� ����
    [SerializeField] 
    GameObject[] objects; //������ ����
    [SerializeField]
    Transform pipe; //������ �ؿ� Lerp�� ������ġ
    [SerializeField]
    GameObject[] objectPosi;
   
    public Transform arrive; //Lerp�� ������ġ
    float lerpTime; //Lerp�� ���������� �� �ҿ�ð�
    float currentTime; //��ӿ�� ���� ��ŸŸ�� �����

    int i = 0;
    float term = 0; //������Ʈ���� �Ϸķ� ����� ���� Lerp�� ������ġ ���� ������
    float plusTime = 3; //������Ʈ���� �Ϸķ� ����� ���� Lerp�� �ӵ� ������

    public static string nextScene; //����� �����Լ��� ������(Ʃ�丮���� ������) ����� ���� �̸� �����
    string nameCheck;
    

    //���ο�
    //Animator anim;
    /*    private void OnCollisionEnter(Collision collision)
        {
            print(collision.contacts[0].point); //�浹ü ��ġ ����Ʈ?
        }
    /*    private void sh()
        {
            anim.SetInteger(anim.GetComponent<Animator>().GetParameter(0).name,1); //�ִϸ����� �Ķ���� �� ��������
        }*/

    //��ȣ��� LoadingTutorialManager.LoadScene("ȣ���� ���� �̸�");�� ����
    //��ü �������� �ٷ� ���� ���� �ִ� ����(static) �Լ�
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
        //float timer = 0f;
        while (!async.isDone)
        {
            yield return null;
            //timer += Time.deltaTime;
            //if (async.progress >= 0.9f)
            //{
                //yield return new WaitForSeconds(10); //90%���� üũ�� 100%�������� ���� �߰� 10��
                async.allowSceneActivation = true;
            //}
        }
    }
    private void Start()
    {
        tutorialObjects.Insert();
        //print(nextScene.Split('_')[0]); //�Ұ�ȣ�ȿ� ����ٸ� �������� string�� �ɰ���
        if(tutorialObjects.GetIndex().TryGetValue(nextScene.Split('_')[0], out IntroductionObjects[] check))
        {
           
            for(int i = 0; i < check.Length; i++)
            {
                print(check[i].Object);
                objectPosi[i].transform.position = check[i].Object.transform.position;
                objectPosi[i] = check[i].Object;
                startposition = objectPosi[i].transform.position; //������ġ ������
                StartCoroutine(Move1(i, startposition, term, plusTime));
                term += 3f; //�Ϸ� ���� ������
                plusTime += 5f; //���ÿ� ���������� �ӵ� ������ �Ϸ� ����
            }
        }

        ////StartCoroutine(Begin());
        ////���ÿ� Lerp�� �����̰� �ϱ� ���� for������ ������ ������ �ڷ�ƾ ȣ���ϱ� ���� ��
        //for (int i = 0; i < objectPosi.Length; i++)
        //{
        //    startposition = objectPosi[i].transform.position; //������ġ ������
        //    StartCoroutine(Move1(i, startposition, term, plusTime));
        //    term += 3f; //�Ϸ� ���� ������
        //    plusTime += 5f; //���ÿ� ���������� �ӵ� ������ �Ϸ� ����
        //}
    }
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(startposition, arrive.position, currentTime / lerpTime); //���
    //    //transform.position = Vector3.Lerp(startposition, endposition, Mathf.SmoothStep(0,1,currentTime / lerpTime)); //����
    //    //transform.position = Vector3.Lerp(startposition, endposition, curve.Evaluate(currentTime / lerpTime)); //�ִϸ��̼�Ŀ��� �ӵ� ����
    //}

    //�ڷ�ƾ ȣ���� ����(for���̶� �ٸ��� 3�� �������� �ڷ�ƾ�� õõ�� ȣ���ϱ� ���� ���� �Լ�)
    IEnumerator Begin()
    {
        while(i < objectPosi.Length) //������Ʈ ������ŭ�� �ݺ�
        {
            startposition = objectPosi[i].transform.position;
            //StartCoroutine(Move1(i, startposition, term));
            i++;
            term += 3f;
            yield return new WaitForSeconds(3); //3�� ��ٸ���
        }
        //StartCoroutine(MoveOrdered());
    }
    IEnumerator Move1(int index, Vector3 startPosi, float term, float plusLerpTime)
    {
        lerpTime = plusLerpTime;
        currentTime = 0;
        while ((currentTime / lerpTime) <= 1) //Lerp�� ��ӿ
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(startPosi, new Vector3(arrive.position.x + term, arrive.position.y, arrive.position.z), currentTime / lerpTime);
            yield return null;
        }
        yield return new WaitUntil(() => index == objectPosi.Length - 1); //��ȣ�ȿ� ������ true�� �� �� �ؿ� �� ����
        StartCoroutine(MoveOrdered());
    }
    //�ڷ�ƾ ȣ���� ����
    IEnumerator MoveOrdered()
    {
        i = 0;
        while (i < objects.Length)
        {
            print(i + "��° ������Ʈ ���");
            startposition = objectPosi[i].transform.position;
            yield return StartCoroutine(Move3(i, startposition)); //Move3�ڷ�ƾ�� �������� �ؿ� �� ����
            yield return new WaitForSeconds(6); //���⼭ �Ұ���Ʈ (�÷��̵Ǵ� �ð���ŭ ����)
            yield return StartCoroutine(Move4(i)); //Move4�ڷ�ƾ�� �������� �ؿ� �� ����
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
            print("�ڷ�ƾ�������� üũ");
        }
    }
}
