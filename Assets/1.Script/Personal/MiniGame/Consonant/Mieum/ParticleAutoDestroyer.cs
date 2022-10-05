using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>(); //particleSystem�� �����´�
    }
    private void Update()
    {
        //��ƼŬ�� ������� �ƴϸ� ����
        if(particle.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
/*
  ���� ���ӿ����� ������Ʈ�� �ϳ��� ��ƼŬ�� �����������
  �������� ��ƼŬ�� �ϳ��� �׷����� ��� ����Ҷ��� �ð��� ���� �� ��ƼŬ�� ���
  ���θ� �˻��ϰų� ��ƼŬ ����� ����Ǿ ��ƼŬ�� �����Ǹ� �ȵ� ��쿡�� �ð��� �������� 
  ����⵵ �Ѵ�
*/