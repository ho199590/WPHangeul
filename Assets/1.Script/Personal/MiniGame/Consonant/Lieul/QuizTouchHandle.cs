using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    public event System.Action<GameObject> QuizCheck; //퀴즈 맞췄을 때 발생할 이벤트
    private void Start()
    {
        FindObjectOfType<QuizManager>().ActiveObject = gameObject;
    }
    private void OnMouseUp()
    {
        print("정답클릭O");
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        QuizCheck?.Invoke(gameObject); //제거함수 실행을 위한 이벤트 호출
    }
}
