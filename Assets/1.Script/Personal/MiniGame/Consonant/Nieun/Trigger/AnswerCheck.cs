using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnswerCheck : MonoBehaviour
{
    [SerializeField]
    string Answer;//��� ��Ҵ��� Ȯ���ϱ� ���� ����
    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������
    /* public event System.Action */
    public UnityEvent rePosition;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)//Ʈ���ſ� ������� �߻��Ǵ� �̺�Ʈ
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("����");
            scoreCase.SetScore();
            scoreCase.SceneComplete += MissionComplete;
        }
        else//Ʋ������ 
        {
            print("Ʋ��");
            /*�̺�Ʈ ó������*/
            rePosition.Invoke();
            print("reposion.Invoke����");
        }
    }
    void MissionComplete()//�� 5���� �� Ŭ���� ������ ��Ÿ���� �̺�Ʈ
    {

    }
}
