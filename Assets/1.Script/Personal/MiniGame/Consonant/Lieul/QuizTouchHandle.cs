using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    int completedQuizOrder;
    private void Start()
    {
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
    }
    //퀘스트의 마지막 오브젝트에서 몇번째 퀘스트인지 알려주는 프로퍼티
    public int CompletedQuizOrder
    {
        set
        {
            completedQuizOrder = value;
        }
    }
    private void OnMouseUp()
    {
        print("정답클릭!");
        if (GetComponent<Rigidbody>()) //2번째 퀘스트에서 오브젝트 떨어뜨리기용
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (completedQuizOrder == 2 || completedQuizOrder == 4) //두번째, 네번째 퀘스트일 경우 퀘스트 완료 지연
        {
            print("마지막 한개에서 완료 지연하는 퀘스트");
            StartCoroutine(DelayComplete());
        }
        else //나머지 퀘스트들은 "바로" 완료처리
        {
            //클릭된 오브젝트들 테두리에 반짝이는 광선같은거 나오게끔 하는 if문 추가 예정 //4번째 퀘스트용
            FindObjectOfType<QuizManager>().AddNRemove = gameObject;
            Destroy(GetComponent<QuizTouchHandle>()); //같은 오브젝트를 두번이상 눌렀을 경우 또다시 정답처리 오브젝트 리스트에 추가되는 일을 방지하기 위한 컴포넌트 제거
        }
        if (GetComponent<Outline>() && GetComponent<Animator>())
        {
            GetComponent<Outline>().enabled = true; //4번째 퀘스트에서 정답클릭시 아웃라인 이펙트 켜주기
            GetComponent<Animator>().enabled = false;
        }
    }
    //2번 퀘스트에서 마지막 오브젝트가 바구니에 떨어지는 모습까지 보여지고 퀘스트 완료되도록 지연시켜주는 함수
    IEnumerator DelayComplete()
    {
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<QuizManager>().AddNRemove = gameObject;
        Destroy(GetComponent<QuizTouchHandle>());
    }
}
