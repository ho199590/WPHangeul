using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCheck : MonoBehaviour
{
    [SerializeField]
    string Answer;
    [SerializeField]
    ScoreHandler scoreCase; //�̼ǿϷ�� ���� �� ������
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("����");
            scoreCase.SetScore();
            scoreCase.SceneComplete += MissionComplete;
        }
        else
        {
            print("Ʋ��");
        }
    }
    void MissionComplete()
    {

    }
}
