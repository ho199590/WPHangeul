using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    Transform Word, WordCase ,TutoHand, TutoPoint, ObjPos , Cloud;
    [SerializeField]
    Transform[] Object;

    [SerializeField]
    AudioClip[] clips;
 

    ScoreHandler scoreHandler;
    SpeakerHandler speaker;
    #endregion

    #region 함수
    private void Start()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();      
        speaker = FindObjectOfType<SpeakerHandler>();

        SceneReady();
    }

    public void SceneReady()
    {
        WordCase.DOScale(Vector3.one, 2f).From(Vector3.zero).SetEase(Ease.OutQuad)
            .OnComplete(() => {
                Word.position = WordCase.position;
                speaker.SoundByClip(clips[0]);
                StartCoroutine(SceneWaiting());
                });
        foreach (var c in Object)
        {
            int r = Random.Range(-10, 10);
            c.DOMoveX(ObjPos.position.x + r, 3200f);
            c.DOMoveY(c.transform.position.y + r, 8).SetLoops(40, LoopType.Yoyo);
        }
        for(int i = 0; i < Cloud.childCount; i++)
        {
            int r = Random.Range(-10, 10);
            Cloud.GetChild(i).DOMoveX(Cloud.GetChild(i).transform.position.x + r, 16).SetLoops(40, LoopType.Yoyo);
            Cloud.GetChild(i).DOMoveY(Cloud.GetChild(i).transform.position.y + r, 8).SetLoops(40, LoopType.Yoyo);
        }
    }

    IEnumerator SceneWaiting()
    {
        yield return new WaitWhile(() => speaker.GetComponent<AudioSource>().isPlaying);
        speaker.SoundByClip(clips[1]);
        yield return new WaitWhile(() => speaker.GetComponent<AudioSource>().isPlaying);
        speaker.SoundByClip(clips[2]);
        TutoHand.GetComponent<UnityEngine.UI.Image>().enabled = true;
        TutoHand.DOMove(TutoPoint.position , 1f).SetLoops(-1, LoopType.Restart);
        scoreHandler.OffScreenSaver();
    }

    #endregion
}
