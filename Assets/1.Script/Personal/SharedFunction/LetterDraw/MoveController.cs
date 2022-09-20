using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    Transform Word, WordCase ,TutoHand, TutoPoint;

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
