using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    private void OnMouseUp()
    {
        print("정답클릭O");
        if (GetComponent<Rigidbody>()) //3번째 퀘스트용
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        //클릭된 오브젝트들 테두리에 반짝이는 광선같은거 나오게끔 하는 if문 추가 예정 //4번째 퀘스트용
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        Destroy(GetComponent<QuizTouchHandle>()); //같은 오브젝트를 두번이상 눌렀을 경우 또다시 정답처리 오브젝트 리스트에 추가되는 일을 방지하기 위한 컴포넌트 제거
    }
}
