using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IntroDrag : MonoBehaviour
{
    [SerializeField]
    Camera m_Camera;       //Canvas Camera변수
    [SerializeField]
    GameObject answerOb;   //정답 동물 
    Vector3 dePosition;    //원래 위치 저장 변수
    [SerializeField]
    GameObject particle;    //파티클 오브젝트
    //현재 위치값 저장 
    private void Start()
    {
        dePosition  = transform.position;
    }

    //Canvas 기준으로 카메라 드래그 , 높이 안맞아서 y축 - 20f 해버림 ㅜ
    void OnMouseDrag()
    {
        float distance = m_Camera.ScreenToWorldPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 20f, distance);
        Vector3 objPos = m_Camera.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
    }
    //마우스 업하는 순간 원위치
    private void OnMouseUp()
    {
        transform.position = dePosition;
    }
    //같은 오브젝트끼리 맞으면 정답 동물 활성화 
    private void OnTriggerEnter(Collider other)
    {
        if(transform.name == other.gameObject.name)
        {
            answerOb.SetActive(true);
            this.transform.GetComponent<Collider>().enabled = false;
            GameObject par = Instantiate(particle);
            par.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            par.transform.position = new Vector3(answerOb.transform.position.x,answerOb.transform.position.y + 0.5f,answerOb.transform.position.z);
            Destroy(par, 1.5f);
            IntroText.PlusIndex();
        }
    }
}
