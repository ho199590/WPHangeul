using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//https://luv-n-interest.tistory.com/687
//����Ű�� ������ �������ִ� enum
public enum ArrowDirection
{
    None,
    Left,
    Right,
    Go,
    Back,
}
//����Ű ��ũ��Ʈ
public class JoyStickHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip ("����Ű�� ������ ����� �־��ּ���")]
    [SerializeField]
    GameObject ball;
    [Tooltip ("�� ����Ű�� ������ ������ �������ּ���")]
    [SerializeField]
    ArrowDirection arrowDirection;

    Vector3 currentDirection = Vector3.zero;

    Rigidbody ballRigi;
    //����Ű enum���� �ְ�޴� ������Ƽ
    ArrowDirection KeyDirection
    {
        set
        {
            currentDirection = value switch
            {
                ArrowDirection.Go => Vector3.forward,
                ArrowDirection.Back => Vector3.back,
                ArrowDirection.Left => Vector3.left,
                ArrowDirection.Right => Vector3.right,
                _ => Vector3.zero //ArrowDirection.None => Vector3.zero
                //ArrowDirection.Up => ball.transform.forward,
                //ArrowDirection.Down => -ball.transform.forward,
                //ArrowDirection.Left => -ball.transform.right,
                //ArrowDirection.Right => ball.transform.right,
                //_ => Vector3.zero
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
        print("����"+currentDirection);
        //ballRigi.AddForce(currentDirection * 100); //world��ǥ�������� ��
        ballRigi.AddRelativeForce(currentDirection * 100); //ball�������� ��
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("��ž");
        //�ڿ������� ������ �޴� ����
        //ballRigi.AddForce(-ballRigi.velocity);
        //ballRigi.AddTorque(-ballRigi.angularVelocity);
        //�ӵ����� ����
        ballRigi.velocity = Vector3.zero;
        ballRigi.angularVelocity = Vector3.zero;
    }
}
