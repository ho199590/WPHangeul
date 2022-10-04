using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCheck : MonoBehaviour
{
    [SerializeField]
    string Answer;
    [SerializeField]
    ScoreHandler scoreCase; //미션완료시 공통 별 프리팹
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("정답");
            scoreCase.SetScore();
            scoreCase.SceneComplete += MissionComplete;
        }
        else
        {
            print("틀림");
        }
    }
    void MissionComplete()
    {

    }
}
