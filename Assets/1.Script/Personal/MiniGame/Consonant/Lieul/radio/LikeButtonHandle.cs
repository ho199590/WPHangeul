using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ʈ�� ��ưó�� ���콺�� �����ӿ� ���� �����ϰ� ����� ��ũ��Ʈ
public class LikeButtonHandle : MonoBehaviour
{
    [Tooltip("�ܾ ������� �ε����� �Է����ּ���")]
    [SerializeField]
    int connectAudio;
    [SerializeField]
    ScoreHandler scoreCase;
    private void Start()
    {
        FindObjectOfType<LieulMissionManager>().CallObject += ColliderActive;
    }
    private void OnMouseEnter()
    {
        print("OnMouseEnter");
        GetComponent<MeshRenderer>().material.color = new Color(1, 0.8f, 0, 1);
    }
    private void OnMouseDown()
    {
        print("OnMouseDown");
        GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0.8f, 0, 1);
    }
    private void OnMouseUp() //cf)OnMouseUpAsButton
    {
        print("OnMouseUp");
        GetComponent<MeshRenderer>().material.color = new Color(1, 0.8f, 0, 1);
        scoreCase.SetScore(); //ScoreCase������ ����
    }
    private void OnMouseExit()
    {
        print("OnMouseExit");
        GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.8f, 0, 0.5f);
    }
    //�ݶ��̴� Ȱ��ȭ �Լ�(�̺�Ʈ�� �־��ִ¿�)
    public void ColliderActive(int num)
    {
        if(num == connectAudio) //����� �Ҹ��� �´� ������Ʈ���� üũ
        {
            if(GetComponent<Collider>())
            {
                GetComponent<Collider>().enabled = true;
                GetComponent<MeshRenderer>().material.color = new Color(1, 0.8f, 0, 1);
            }
        }
    }
}
