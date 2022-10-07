using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true����
    [SerializeField]
    string Answer;//��� ��Ҵ��� Ȯ���ϱ� ���� ����
    [SerializeField]
    GameObject bollon;//bollon ������Ʈ

    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������ 
    Vector3 bollonPosi;
    Vector3 animal;//animal positon
    Vector3 perentsPosition; //ReMove()�Ҷ� ���������� ����� tranceform
    SpeakerHandler speakerHandler;
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;//����ǳ�� ��ġ�� ����
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;//�θ�tranceform ����
        speakerHandler = FindObjectOfType<SpeakerHandler>();
    }
    public void ReMove()//Ʋ���ָ� ReMove ���Ѿ��� 
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
        PositionReset();
        isMove.enabled = true;
        GetComponent<Collider>().enabled = false;
        bollon.transform.position = bollonPosi;//ǳ�� ����ġ ��ȯ
        bollon.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("����");
            scoreCase.SetScore();
        }
        else
        {
            print("Ʋ��");
            speakerHandler.SoundByNum2(1);
            ReMove();
        }
    }
    private void PositionReset()
    {
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        transform.position = animal;//���� ��ġ�� ��ȯ
    }
}

