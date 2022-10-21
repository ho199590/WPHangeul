using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀴즈의 정답처리 매니저
public class QuizManager : MonoBehaviour
{
    public List<GameObject> quizObjects; //활성화된 <QuizTouchHandle>스크립트가 들어있는 오브젝트를 넣었다 뺐다 할 리스트
    [SerializeField]
    List<GameObject> obstacles; //퀴즈 종료 후 제거해줄 장애물

    //<QuizTouchHandle>스크립트가 들어있는 오브젝트가 활성화되면 가져오게될 프로퍼티
    public GameObject ActiveObject
    {
        set
        {
            quizObjects.Add(value);
            print("정답 처리 카운트 값" + quizObjects.Count);
            //제거함수 이벤트에 추가해주기
            var quiztouch = FindObjectsOfType<QuizTouchHandle>();
            foreach(var item in quiztouch)
            {
                item.QuizCheck += RemoveObject;
            }
        }
    }
    //<QuizTouchHandle>스크립트에서 OnMouseUp일때 리스트에서 오브젝트 제거해주는 함수
    //리스트에 들어갈 오브젝트가 활성화될때 이 함수를 이벤트에 추가해주기
    void RemoveObject(GameObject minus)
    {
        print("정답맞춘 오브젝트 제거");
        quizObjects.Remove(minus);
        if (quizObjects.Count == 0) 
        {
            foreach (var item in obstacles) item.SetActive(false);
            FindObjectOfType<NaviMoveManager>().Check = true;
        }
    }
}

