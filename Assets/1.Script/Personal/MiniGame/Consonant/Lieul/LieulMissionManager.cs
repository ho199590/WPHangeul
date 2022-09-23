using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//애로우 도착시 라디오의 랜덤 재생 시작!
public class LieulMissionManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] lieulWords;
    AudioSource audio;
    public event System.Action<int> CallObject;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        FindObjectOfType<ArrowAniHandle>().ButtonActive += RandomPlay;
    }
    private void OnMouseDown()
    {
        print("클릭감지");
        RandomPlay();
        //애로우가 라디오를 터치하는 애니메이션과 함께 라디오 소리를 랜덤으로 재생되게 하는 이벤트 연결하기
    }
    void RandomPlay()
    {
        int i = Random.Range(0, lieulWords.Length);
        audio.clip = lieulWords[i];
        audio.Play();
        CallObject?.Invoke(i);
    }
}
