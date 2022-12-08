using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://www.youtube.com/watch?v=MyVY-y_jK1I&t=346s
//https://wergia.tistory.com/59
public class LoadingIntroductionManager : MonoBehaviour
{
    #region ����
    [SerializeField]
    TutorialObjects tutorialObjects; //Ʃ�丮�� ������ ������Ʈ���� ������ �ִ� Scriptable Object
    [SerializeField]
    GameObject[] objectPosi; //������Ʈ���� ��ġ�� ������
    [SerializeField]
    GameObject[] backPosi; //������ ��� ��ġ�� ������
    [SerializeField]
    Transform underPipe; //������ �ؿ� Lerp�� ������ġ
    [SerializeField]
    Transform arrive1; //Lerp�� ������ġ

    Vector3 startposition; //Lerp���� ����� ������ġ ���������� ù ��ġ�� ��Ƶ� ����
    float lerpTime; //Lerp�� ���������� �� �ҿ�ð�
    float currentTime; //��ӿ�� ���� ��ŸŸ�� �����

    int i = 0;
    float term = 0; //������Ʈ���� �Ϸķ� ����� ���� Lerp�� ������ġ ���� ������
    float plusTime = 3; //������Ʈ���� �Ϸķ� ����� ���� Lerp�� �ӵ� ������
    int objectsLength; //scriptable object���� ������ ������ ������Ʈ�� ����(����) �����
    AudioSource perAudio; //������Ʈ�� �ڱ�Ұ��� 
    Animator anim;

    public static string nextScene; //����� �����Լ��� ������(Ʃ�丮���� ������) ����� ���� �̸� �����

    public System.Action actionMask; //�������ؿ� ������(Ư������)�� ����ũ(������)�� ���ֱ� ���� �̺�Ʈ
    #endregion
    #region ���ο�
    //���ο�
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(startposition, arrive.position, currentTime / lerpTime); //���
    //    //transform.position = Vector3.Lerp(startposition, endposition, Mathf.SmoothStep(0,1,currentTime / lerpTime)); //����
    //    //transform.position = Vector3.Lerp(startposition, endposition, curve.Evaluate(currentTime / lerpTime)); //�ִϸ��̼�Ŀ��� �ӵ� ����
    //}
    //IEnumerator Begin() //�ڷ�ƾ ȣ���� ����(for���̶� �ٸ��� 3�� �������� �ڷ�ƾ�� õõ�� ȣ���ϱ� ���� ���� �Լ�)
    //{
    //    while(i < objectsLength) //������Ʈ ������ŭ�� �ݺ�
    //    {
    //        startposition = objectPosi[i].transform.position;
    //        //StartCoroutine(Move1(i, startposition, term));
    //        i++;
    //        term += 3f;
    //        yield return new WaitForSeconds(3); //3�� ��ٸ���
    //    }
    //    //StartCoroutine(MoveOrdered());
    //}

    //Animator anim;
    /*    private void OnCollisionEnter(Collision collision)
        {
            print(collision.contacts[0].point); //�浹ü ��ġ ����Ʈ?
        }
    /*    private void sh()
        {
            anim.SetInteger(anim.GetComponent<Animator>().GetParameter(0).name,1); //�ִϸ����� �Ķ���� �� ��������
        }*/
    #endregion
    #region �Լ�
    //�ÿ������� Intro���� �ʹ� �� �ѱ�� ���� �� �����̽��� ������ �ٷ� ���� ������ ��ȯ
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(nextScene);
    }
    //��ȣ��� LoadingTutorialManager.LoadScene("ȣ���� ���� �̸�");�� ����
    public static void LoadScene(string sceneName) //��ü �������� �ٷ� ���� ���� �ִ� ����(static) �Լ�
    {
        print("�������̸� üũ:" + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene("Introduction");
    }
    IEnumerator LoadScene() //�� ��� ���� Ʃ�丮�� ���� ������ִ� �񵿱�� ��� ���� �Լ�
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
    private void Start() //���� ���� Scriptable Object���� ������ �����յ��� ���� �� ������ġ�� ��ġ��Ű�� �̵��� �����ϴ� �Լ� Go()�� ȣ��
    {
        tutorialObjects.Insert();
        for (i = 0; i < backPosi.Length; i++)
        {
            if (backPosi[i].name.Contains(nextScene.Split('_')[0]))
            {
                tutorialObjects.Eviroments[i].transform.localScale = backPosi[i].transform.localScale;
                backPosi[i] = Instantiate(tutorialObjects.Eviroments[i], backPosi[i].transform.position, backPosi[i].transform.rotation);
                backPosi[i].SetActive(true);
            }
        }
        if (tutorialObjects.GetIndex().TryGetValue(nextScene.Split('_')[0], out IntroductionObjects[] objects)) //nextScene�̸��� �����(_)�տ� ���ڰ� key�� Dictionary�� ������ �ִ� IntroductionObjects[]�� IntroductionObjects[]���·� ����� objects������ ��������(out)  
        {
            objectsLength = objects.Length;
            for (i = 0 ; i <  objectsLength; i++)
            {
                print(objects[i].Object);
                objects[i].Object.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f); 
                objectPosi[i] = Instantiate(objects[i].Object, objectPosi[i].transform.position, Quaternion.LookRotation(Vector3.left)); //�ı��� ���ֱ�? //objects[i].Object.transform.position = objectPosi[i].transform.position;
                if (objectPosi[i].GetComponent<Animator>())
                {
                    anim = objectPosi[i].GetComponent<Animator>();
                    anim.SetInteger("Introduction", 1); // anim.SetInteger(anim.GetParameter(1).name, 1);
                }
                objectPosi[i].AddComponent<AudioSource>();
                perAudio = objectPosi[i].GetComponent<AudioSource>();
                perAudio.clip = objects[i].perAudio;
            }
        }
        Go();
    }
    void Go() //Lerp�� ������ �̵� ���� �Լ�
    {
        //print(nextScene.Split('_')[0]); //�Ұ�ȣ�ȿ� ����ٸ� �������� string�� �ɰ���
        for ( i = 0; i < objectsLength; i++)
        {
            startposition = objectPosi[i].transform.position; //������ġ ������
            StartCoroutine(Move1(i, startposition, term, plusTime));
            term += 3f; //�Ϸ� ���� ������
            plusTime += 5f; //���ÿ� ���������� �ӵ� ������ �Ϸ� ����
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
    IEnumerator Move1(int index, Vector3 startPosi, float term, float plusLerpTime)
    {
        lerpTime = plusLerpTime;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1) //Lerp�� ���
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(startPosi, new Vector3(arrive1.position.x + term, arrive1.position.y, arrive1.position.z), currentTime / lerpTime);
            yield return null;
        }
        yield return new WaitUntil(() => index == objectsLength - 1); //��ȣ�ȿ� ������ true�� �� �� �ؿ� �� ����
        StartCoroutine(MoveOrdered());
    }
    IEnumerator MoveOrdered() //�ڷ�ƾ ȣ���� ����
    {
        i = 0;
        while (i < objectsLength)
        {
            print(i + "��° ������Ʈ ���");
            startposition = objectPosi[i].transform.position;
            yield return StartCoroutine(Move3(i, startposition)); //Move3�ڷ�ƾ�� �������� �ؿ� �� ����
            yield return new WaitForSeconds(5); //���⼭ �Ұ���Ʈ (�÷��̵Ǵ� �ð���ŭ ����)
            yield return StartCoroutine(Move4(i)); //Move4�ڷ�ƾ�� �������� �ؿ� �� ����
            i++;
        }
    }
    IEnumerator Move3(int index, Vector3 startPosi)
    {
        lerpTime = 2f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(startPosi, underPipe.position, currentTime / lerpTime);
            yield return null;
        }
        if(objectPosi[index].GetComponent<Animator>())
        {
            anim = objectPosi[index].GetComponent<Animator>();
            anim.SetInteger("Introduction", 2); /* anim.SetInteger(anim.GetParameter(1).name, 2);*/
        }
        objectPosi[index].transform.rotation = Quaternion.LookRotation(Vector3.back+Vector3.right); //�������� �밢�� ���� �����ϱ�
        actionMask?.Invoke(); //����ũ(������) ���ֱ� ���� �̺�Ʈ ����
        objectPosi[i].GetComponent<AudioSource>().Play(); //�Ұ���Ʈ �÷���
    }
    IEnumerator Move4(int index)
    {
        lerpTime = 0.5f;
        currentTime = 0;
        while ((currentTime / lerpTime) < 1)
        {
            currentTime += Time.deltaTime;
            objectPosi[index].transform.position = Vector3.Lerp(underPipe.position, new Vector3(underPipe.position.x, underPipe.position.y + 10, underPipe.position.z), currentTime / lerpTime);
            yield return null;
        }
        objectPosi[index].SetActive(false);
        if (index == objectsLength - 1) 
        {
            StopAllCoroutines();
            StartCoroutine(LoadScene()); //�� ���
            print("������ �ڷ�ƾ��");
        }
    }
    #endregion
}
