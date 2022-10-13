using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreHandler : MonoBehaviour
{
    #region ����
    [SerializeField]
    int MaxScore;
    int curScore = 0;

    [Header("ȭ�� ���")]
    [SerializeField]
    GameObject ScreenSaver;
    
    [Header("���ھ� ��ȭ UI")]
    [SerializeField]
    Sprite scoreCase;
    [SerializeField]
    Sprite scoreFill;
    [SerializeField]
    GameObject fillParticle;

    [Header("����� Ŭ��")]
    [SerializeField]
    AudioClip[] clips;    
    AudioSource sources;

    [Header("Ŭ���� ��ƼŬ")]
    [SerializeField]
    GameObject[] particles;
    [Header("��ƼŬ�� ���� ��ġ�� ũ��")]
    public int ParticleDistance = 10;
    public int ParticleScale = 5;

    bool CompleteCheck = false;
    private Transform EndPoint;
    #endregion

    #region �̺�Ʈ
    public event System.Action SceneComplete;
    public event System.Action SceneStart;
    #endregion

    #region �Լ�
    private void Awake()
    {
        sources = GetComponent<AudioSource>();

        for (int i = 0; i < MaxScore; i++)
        {
            GameObject gg = new GameObject();
            gg.AddComponent<UnityEngine.UI.Image>();
            gg.transform.SetParent(transform);
            gg.transform.localScale = Vector3.one * 1.5f;
            gg.transform.position = transform.position;
            gg.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<UnityEngine.UI.Image>() != null)
            {
                transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = scoreCase;
            }
        }
        EndPoint = MakeFireFlowerPoint();

        SceneComplete += Comp;
        SceneStart += OnScreenSaver;
    }

    public void SetScore()
    {   
        var filled = Instantiate(fillParticle);
        filled.transform.position = transform.GetChild(curScore).position;

        transform.GetChild(curScore).GetComponent<UnityEngine.UI.Image>().sprite = scoreFill;
        transform.GetChild(curScore).GetComponent<UnityEngine.UI.Image>().DOFade(1, 4f).From(0);

        if(curScore < MaxScore - 1)
        {
            SoundPlay(0);
            curScore++;
        }
        else
        {
            SoundPlay(1);
            CompleteCheck = true;
            OnComplete();
        }
    }

    public void OnComplete()
    {
        SceneComplete?.Invoke();
    }

    IEnumerator ClearParticle()
    {
        while (true)
        {
            int num = Random.Range(4, 9);
            for (int i = 0; i < num; i++)
            {
                var ex = Instantiate(particles[Random.Range(0, particles.Length)]);
                Vector3 subPos = EndPoint.transform.position +
                    Camera.main.transform.right * Random.Range(-8, 8) +
                    Camera.main.transform.up * Random.Range(-4, 4);

                ex.transform.position = subPos;
                ex.transform.localScale = Vector3.one * ParticleScale;
                ex.transform.SetParent(EndPoint);
            }
            yield return new WaitForSeconds(1f);
        }        
    }

    // ī�޶� ���� �������� ��ġ��Ű��
    public Transform MakeFireFlowerPoint()
    {
        GameObject TestCube = new GameObject();        
        TestCube.transform.position = Camera.main.transform.position + Camera.main.transform.forward * ParticleDistance;
        TestCube.transform.rotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);        
        
        return TestCube.transform;
    }
    #endregion

    #region
    public void SoundPlay(int num)
    {
        if (num < clips.Length)
        {
            sources.PlayOneShot(clips[num]);
        }
    }

    public void Comp()
    {
        OnScreenSaver();
        StartCoroutine(ClearParticle());
    }

    public void OnScreenSaver()
    {
        if(ScreenSaver != null)
        {
            ScreenSaver?.SetActive(true);
        }        
    }

    public void OffScreenSaver()
    {
        if (ScreenSaver != null)
            ScreenSaver?.SetActive(false);
    }

    public bool CompCheck()
    {
        return CompleteCheck;
    }
        
    #endregion
}
