using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject ball;

    Rigidbody ballRigi;
    private void Start()
    {
        ballRigi = ball.GetComponent<Rigidbody>(); 
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        print("엔터");
        ballRigi.AddForce(Vector3.forward*100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("스탑");
        ballRigi.AddForce(Vector3.back*100);
    }
}
