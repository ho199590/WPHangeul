using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreHandler : MonoBehaviour
{
    #region 변수
    [SerializeField]
    int MaxScore;
    int curScore = 0;

    [Header("화면 잠금")]
    [SerializeField]
    GameObject ScreenSaver;
    
    [Header("스코어 변화 UI")]
    [SerializeField]
    Sprite scoreCase;
    [SerializeField]
    Sprite scoreFill;
    [SerializeField]
    GameObject fillParticle;

    [Header("오디오 클립")]
    [SerializeField]
    AudioClip[] clips;    
    AudioSource sources;

    [Header("클리어 파티클")]
    [SerializeField]
    GameObject[] particles;
    #endregion

    #region 이벤트
    public event System.Action SceneComplete;
    public event System.Action SceneStart;
    #endregion

    #region 함수
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
                if (!transform.root.GetComponent<Canvas>())
                {
                    ex.transform.SetParent(transform.root);
                }
                ex.transform.localPosition = new Vector3(Random.Range(-8, 8), Random.Range(-5, 5), 0);
                ex.transform.localScale = Vector3.one * 1.5f;
            }
            yield return new WaitForSeconds(1f);
        }
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
        ScreenSaver?.SetActive(true);
    }

    public void OffScreenSaver()
    {
        ScreenSaver?.SetActive(false);
    }
    #endregion
}
