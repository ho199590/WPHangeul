using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//퀘스트별로 정답처리용 OnMouse관련 기능
public class QuizTouchHandle : MonoBehaviour
{
    [Tooltip ("퀘스트별 퀴즈시 중력으로 떨어뜨려줘야할 오브젝트를 모두 넣어주세요")]
    [SerializeField]
    Rigidbody[] drop; //퀘스트별 퀴즈 시작시 중력으로 떨어뜨려줘야 하는 오브젝트
    [Tooltip ("퀘스트별 퀴즈시 활성화 해줘야 할 오브젝트 모두 넣어주세요")]
    [SerializeField]
    GameObject[] active; //퀘스트별 퀴즈 시작시 활성화해줘야할 오브젝트
    [Tooltip("퀘스트별 퀴즈 종료후 진로에 방해될 장애물들 모두 넣어주세요")]
    [SerializeField]
    GameObject[] obstacles; //퀴즈맞추면 비활성화할 진로방해물
    
    [Tooltip ("퀘스트별 퀴즈의 정답처리 갯수를 입력해주세요")]
    [SerializeField]
    int num;
    private void Start()
    {
        //퀴즈 맞출시에 필요한 오브젝트 떨어뜨려주기
        if (drop != null)
        {
            for (int j = 0; j < drop.Length; j++)
                drop[j].useGravity = true;
        }
        //퀴즈 맞출시에 필요한 오브젝트 활성화해주기 
        if (active != null)
        {
            for (int k = 0; k < active.Length; k++)
                active[k].SetActive(true);
        }
        FindObjectOfType<NaviMoveManager>().QuizCheck += RemoveObstacles; //이 스크립트가 들어있는 오브젝트가 활성화 되자마자 이벤트에 함수 추가

    }
    void RemoveObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++) obstacles[i].gameObject.SetActive(false);
    }
    //정답오브젝트를 클릭시 이벤트 호출해서 실행되게끔 파라미터값을 변경
    private void OnMouseUp()
    {
        print("정답클릭O");
        if (GetComponent<Rigidbody>()) 
        {
            print("자기자신 체크");
            GetComponent<Rigidbody>().useGravity = true; //두번째 퀴즈때 클릭떼면 떨어뜨려야하는 오브젝트용
        }

        //gameObject.SetActive(false); //자기자신도 진로방해의 장애물임으로 비활성화
        FindObjectOfType<NaviMoveManager>().QuizNum = num; //파라미터값을 변경함으로써 이벤트 호출(파라미터내용 안에 이벤트 호출이 존재함)
    }
}
