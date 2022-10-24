using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Tooltip ("퀴즈순서대로 장애물들을 넣어주세요")]
[System.Serializable]
public class obstacles
{
    public GameObject[] roadObstacles; //배열안에 배열: 퀴즈별 장애물
}
//퀴즈의 정답처리 매니저
public class QuizManager : MonoBehaviour
{
    public List<GameObject> quizObjects; //활성화된 <QuizTouchHandle>스크립트가 들어있는 오브젝트를 넣었다 뺐다 할 리스트
    [SerializeField]
    List<GameObject> obstacles; //퀴즈 종료 후 제거해줄 장애물
    [SerializeField]
    obstacles[] perQuizObstacles;
    int count;
    public event System.Action<GameObject> QuizCheck; //퀴즈 맞췄을 때 발생할 이벤트

    private void Start()
    {
        QuizCheck += RemoveObject;
    }
    //<QuizTouchHandle>스크립트가 들어있는 오브젝트가 활성화되면 가져오게될 프로퍼티
    public GameObject ActiveObject
    {
        set
        {
            quizObjects.Add(value);
            print("정답 처리 카운트 값" + quizObjects.Count);
        }
    }
    //<QuizTouchHandle>스크립트에서 OnMouseUp된 오브젝트 보내줘서 리스트에서 제거하게 하는 프로퍼티
    public GameObject MouseUpCheck
    {
        set
        {
            QuizCheck?.Invoke(value);
        }
    }
    //몇번째 퀴즈인지 카운트용 프로퍼티 //퀴즈별 장애물 제거용 인덱스로 사용
    public int QuizOrder
    {
        set
        {
            print(count++ + "번째 퀴즈 ");
        }
    }
    //<QuizTouchHandle>스크립트에서 OnMouseUp일때 리스트에서 오브젝트 제거해주는 함수
    //리스트에 들어갈 오브젝트가 활성화될때 이 함수를 이벤트에 추가해주기
    void RemoveObject(GameObject minus)
    {
        print("정답맞춘 오브젝트 제거");
        quizObjects.Remove(minus);
        if (quizObjects.Count == 0) //지울 오브젝트가 다 소진되면 장애물들 다 치우고 네브메쉬 다시 출발
        {
            for(int i = 0; i < perQuizObstacles[count-1].roadObstacles.Length; i++)
            {
                perQuizObstacles[count - 1].roadObstacles[i].gameObject.SetActive(false);
            }
            FindObjectOfType<NaviMoveManager>().Check = true; //다시 네브메쉬 움직이게 bool값 보내주기
        }
    }
}

