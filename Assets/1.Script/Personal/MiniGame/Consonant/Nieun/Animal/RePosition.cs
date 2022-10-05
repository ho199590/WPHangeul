using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RePosition : MonoBehaviour
{
    [SerializeField]
    Vector3 balloonPosition;//ǳ�� ��ġ��
    [SerializeField]
    Vector3 animalPosition;//������Ʈ ��ġ��
    [SerializeField]
    GameObject balloonOb;
    Rigidbody rb;//iskinematic
    Collider col_Animal;//IsTrigger
    Balloon_Move isMove;//BalloonMove true����

    private void Start()
    {
        animalPosition = this.transform.position;//���� ��ġ ����
        balloonPosition = this.transform.position;//ǳ�� ��ġ ����
        isMove = GetComponentInParent<Balloon_Move>();
        rb = GetComponent<Rigidbody>();
        col_Animal = GetComponent<Collider>();
    }
    public void ReMove()
    {
        col_Animal.isTrigger = true;
        this.transform.position = animalPosition;
        balloonOb.transform.position = balloonPosition;
        balloonOb.SetActive(true);
        isMove.enabled = true;
        rb.useGravity = false;
        rb.isKinematic = false;
        print("ReMove����");
    }
}
