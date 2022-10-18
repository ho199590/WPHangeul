using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    public event System.Action QuizCheck1; //퀴즈 맞췄을 때 발생할 이벤트

    private void OnMouseUp()
    {
        if (gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            print("정답클릭O");
            QuizCheck1?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
