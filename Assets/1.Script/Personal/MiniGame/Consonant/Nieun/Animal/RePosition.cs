using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Balloon_Move isMove;//BalloonMove true����
    Vector3 animal;//animal positon
    [SerializeField]
    GameObject bollonPosition;

    Vector3 bollonPosi;
    private void Start()
    {
        isMove = GetComponentInParent<Balloon_Move>();
        bollonPosi = bollonPosition.transform.position;//����ǳ�� ��ġ�� ����
        animal = this.transform.position;
    }
    public void ReMove()
    {  
        isMove.enabled = true;
        this.transform.position = animal;//���� ��ġ�� ��ȯ
        bollonPosition.transform.position = bollonPosi;//ǳ�� ����ġ ��ȯ
    }
}/*    [SerializeField]
    GameObject AnimalOb;
    [SerializeField]
    GameObject balloonOb;*/
/* Animal2.GetComponent<Collider>().isTrigger = true;
        AnimalOb.GetComponent<Rigidbody>().useGravity = false;
        AnimalOb.GetComponent<Rigidbody>().isKinematic = false;
        AnimalOb.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);*/
