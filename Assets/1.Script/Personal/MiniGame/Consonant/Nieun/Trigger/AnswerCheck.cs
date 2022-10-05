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
    public event System.Action <bool> Check;    
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
        }
        else//Ʋ������ 
        {
            print("Ʋ��");
            /*�̺�Ʈ ó������*/
            rePosition.Invoke();
        }
    }
}
