using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnswerCheck : MonoBehaviour
{
    [SerializeField]
    string Answer;//어디에 닿았는지 확인하기 위한 변수
    [SerializeField]
    ScoreHandler scoreCase; //미션완료시 공통 별 프리팹
    /* public event System.Action */
    public UnityEvent rePosition;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)//트리거에 닿았을때 발생되는 이벤트
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Answer))
        {
            print("정답");
            scoreCase.SetScore();
            scoreCase.SceneComplete += MissionComplete;
        }
        else//틀렸을때 
        {
            print("틀림");
            /*이벤트 처리하자*/
            rePosition.Invoke();
            print("reposion.Invoke실행");
        }
    }
    void MissionComplete()//별 5개를 다 클리어 했을때 나타나는 이벤트
    {

    }
}
