using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    public void Up()
    {
        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward*100);
    }
    public void Down()
    {
        ball.GetComponent<Rigidbody>().AddForce(Vector3.back*100);
    }
    public void Right()
    {
        ball.GetComponent<Rigidbody>().AddForce(Vector3.right*100);
    }
    public void Left()
    {
        ball.GetComponent<Rigidbody>().AddForce(Vector3.left*100);
    }
}
