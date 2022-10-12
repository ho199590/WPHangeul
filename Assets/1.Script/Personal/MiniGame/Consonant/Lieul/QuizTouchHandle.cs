using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QuizTouchHandle : MonoBehaviour
{
    public GameObject x;
    public GameObject invisible;
    public NavMeshAgent ball;

    [SerializeField]
    GameObject plane;
    private void OnMouseDown()
    {
        print("Å¬¸¯");
        
    }
    private void OnMouseUp()
    {
        print("Å¬¸¯¶«");
        if (gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            print("O");
            gameObject.SetActive(false);
            x.SetActive(false);
            invisible.SetActive(false);
            ball.isStopped = false;
            plane.SetActive(false);
        }
        else
        {
            print("X");
        }
    }
}
