using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//마우스포인트따라 오브젝트 움직이기
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z값 부여용
    private Vector3 posi;
    [SerializeField]
    int num;
    [Tooltip("퀘스트별 퀴즈시 중력으로 떨어뜨려줘야할 오브젝트를 모두 넣어주세요")]
    [SerializeField]
    Rigidbody[] drop; //퀘스트별 퀴즈 시작시 중력으로 떨어뜨려줘야 하는 오브젝트
    [Tooltip("퀘스트별 퀴즈시 활성화 해줘야 할 오브젝트 모두 넣어주세요")]
    [SerializeField]
    GameObject[] active; //퀘스트별 퀴즈 시작시 활성화해줘야할 오브젝트
    [Tooltip("퀘스트별 퀴즈 종료후 진로에 방해될 장애물들 모두 넣어주세요")]
    [SerializeField]
    GameObject[] obstacles; //퀴즈맞추면 비활성화할 진로방해물
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
    Vector3 GetMouseWorldPosition() //마우스포인트의 월드좌표값 부여용
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = z_saved;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDown()
    {
        z_saved = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        posi = gameObject.transform.position - GetMouseWorldPosition();
        if(GetComponent<Rigidbody>() == true) GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
    }
   
    void RemoveObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++) obstacles[i].gameObject.SetActive(false);
    }
    private void OnMouseUp()
    {
        gameObject.SetActive(false);
        FindObjectOfType<NaviMoveManager>().QuizNum = num; //파라미터값을 변경함으로써 이벤트 호출(파라미터내용 안에 이벤트 호출이 존재함)
    }
}
