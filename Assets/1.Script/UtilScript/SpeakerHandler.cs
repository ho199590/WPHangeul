using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerHandler : MonoBehaviour
{
    #region ����
    AudioSource audioSource;
    [Header("���� ���� ����")]
    [SerializeField]
    AudioClip[] clips;
    #endregion


    #region �Լ�
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    
    public void SoundByNum(int num)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clips[num]);
        }
    }
    public void SoundByNum2(int num)
    {
      audioSource.PlayOneShot(clips[num]);   
    }

    public void SoundByClip(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float num)
    {
        audioSource.volume = num;
    }
    #endregion
}
