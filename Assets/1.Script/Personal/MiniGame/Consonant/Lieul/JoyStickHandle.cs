using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ArrowDirection
{
    None,
    Left,
    Right,
    Up,
    Down,
}

public class JoyStickHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    ArrowDirection arrowDirection;

    Vector3 currentDirection = Vector3.zero;

    Rigidbody ballRigi;

    ArrowDirection KeyDirection
    {
        set
        {
            currentDirection = value switch
            {
                //ArrowDirection.Up => ball.transform.forward,
                //ArrowDirection.Down => -ball.transform.forward,
                //ArrowDirection.Left => -ball.transform.right,
                //ArrowDirection.Right => ball.transform.right,
                //_ => Vector3.zero
                ArrowDirection.Up => Vector3.forward,
                ArrowDirection.Down => Vector3.back,
                ArrowDirection.Left => Vector3.left,
                ArrowDirection.Right => Vector3.right,
                _ => Vector3.zero
            };
        }
    }

    void OnEnable() 
    {
        KeyDirection = arrowDirection;
    }
    private void Start()
    {
        ballRigi = ball.GetComponent<Rigidbody>(); 
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        print("엔터");
        //ballRigi.AddForce(currentDirection * 100);
        ballRigi.AddRelativeForce(currentDirection * 100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("스탑");
        //ballRigi.AddForce(-ballRigi.velocity);
        //ballRigi.AddTorque(-ballRigi.angularVelocity);
        ballRigi.velocity = Vector3.zero;
        ballRigi.angularVelocity = Vector3.zero;
    }
}
