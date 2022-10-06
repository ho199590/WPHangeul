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
    GameObject AnimalOb;//animal ������Ʈ
    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������ 
    Vector3 bollonPosi;
    Vector3 animal;//animal positon
    Vector3 perentsPosition;
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollon.transform.position;//����ǳ�� ��ġ�� ����
        animal = this.transform.position;
        perentsPosition = gameObject.transform.parent.gameObject.transform.position;
    }
    public void ReMove()//Ʋ���ָ� ReMove ���Ѿ��� 
    {
        AnimalOb.gameObject.GetComponent<Collider>().isTrigger = true;
        AnimalOb.gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.parent.gameObject.transform.position = perentsPosition;
        AnimalOb.transform.position = animal;//���� ��ġ�� ��ȯ
        isMove.enabled = true;
        AnimalOb.GetComponent<Collider>().enabled = false;
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
            ReMove();
        }
    }
}
/*    [SerializeField]
    GameObject AnimalOb;
    [SerializeField]
    GameObject balloonOb;*/
/* Animal2.GetComponent<Collider>().isTrigger = true;
        AnimalOb.GetComponent<Rigidbody>().useGravity = false;
        AnimalOb.GetComponent<Rigidbody>().isKinematic = false;
        AnimalOb.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);*/
