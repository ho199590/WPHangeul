using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    public float maxY;       //��ġ�� �ִ� y��ġ
    [SerializeField]
    public float minY;       //��ġ�� �ּ� y��ġ
    [SerializeField]
    private GameObject moleHitEffectPrefab;     //�δ��� Ÿ�� ȿ�� ������
    [SerializeField]
    private AudioClip[] audioClips;             //�δ����� Ÿ������ �� ����Ǵ� ����
    public ObjectDetector objectDetector;       //���콺 Ŭ������ ������Ʈ ������ ���� ObjectDetector 
    private Movement3D movement3D;              //��ġ �̵������� movement
    private AudioSource audioSource;            //�δ����� Ÿ������ �� �Ҹ��� ����ϴ� AudioSource


    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();
        audioSource = GetComponent<AudioSource>();  
        //OnHit �޼ҵ带 objectDetector Class�� raycast�� �̺�Ʈ�� ���
        //objectDetector�� raycastEvent.Invoke(hit.transform)�޼ҵ尡
        //ȣ��ɶ� ����Onhit �޼ҵ尡 ȣ��ȴ�.
        objectDetector.raycastEvent.AddListener(OnHit);
    }

    private void OnHit(Transform target)
    {
        if (target.CompareTag("Mole"))

        { //Ÿ�� �δ����� ������Ʈ ��������
            MoleFSM mole = target.GetComponent<MoleFSM>();

            //�δ����� ���°� Ȧ �ȿ��������� ���� �Ұ�
            if (mole.MoleState == MoleState.UnderGround)
                return;

            //��ġ�� ��ġ ����
            transform.position = new Vector3(target.position.x, minY, target.position.z);

            //�δ����� �¾����� ����׶��� ���·� ����
            mole.ChangeState(MoleState.UnderGround);

            //ī�޶� ����
            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            //�δ��� Ÿ�� ȿ�� ���� (particle�� ������ �δ��� ����� �����ϰ� ����)
            GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
            main.startColor = mole.GetComponent<MeshRenderer>().material.color;

            //���� ���� (+50)
            //gameController.Score += 50;
            //�δ��� ���� ���� ó�� (���� , �ð� , �������)
            MoleHitProcess(mole);

            //��ġ�� �ٽ� ���� �̵���Ű�� �ڷ�ƾ�Լ� ���
            StartCoroutine("MoveUp");
        }
    }

    private IEnumerator MoveUp()
    {
        //�̵������� ��
        movement3D.MoveTo(Vector3.up);

        while (true)
        {
            if (transform.position.y >= maxY)
            {
                movement3D.MoveTo(Vector3.zero);
                break;

            }

            yield return null;
        }
    }
    private void MoleHitProcess(MoleFSM mole)
    {
       //���� ��� (Normal = 0 , Red = 1 , Blue = 2)
      PlaySound((int)mole.MoleType);
    }
    private void PlaySound(int index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];   
        audioSource.Play();
    }

}