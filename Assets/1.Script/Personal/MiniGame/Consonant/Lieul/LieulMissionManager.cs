using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//�ַο� ������ ������ ���� ��� ����!
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
        print("Ŭ������");
        RandomPlay();
    }
    void RandomPlay()
    {
        int i = Random.Range(0, lieulWords.Length);
        audio.clip = lieulWords[i];
        audio.Play();
        CallObject?.Invoke(i);
    }
}
