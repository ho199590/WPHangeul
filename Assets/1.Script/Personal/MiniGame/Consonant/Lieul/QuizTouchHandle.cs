using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QuizTouchHandle : MonoBehaviour
{
    public GameObject x;
    public GameObject invisible;
    public NavMeshAgent ball;
    private void OnMouseDown()
    {
        print("Å¬¸¯");
        
    }
    private void OnMouseUp()
    {
        print("Å¬¸¯¶«");
        if (gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            print("O");
            gameObject.SetActive(false);
            x.SetActive(false);
            invisible.SetActive(false);
            ball.isStopped = false;
        }
        else
        {
            print("X");
        }
    }
}
