using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTouchHandle : MonoBehaviour
{
    public GameObject x;
    public GameObject invisible;
    private void OnMouseDown()
    {
        print("Å¬¸¯");
        
    }
    private void OnMouseUp()
    {
        print("Å¬¸¯¶«");
        if (gameObject.transform.GetChild(1).name.Contains("O"))
        {
            print("O");
            gameObject.SetActive(false);
            x.SetActive(false);
            invisible.SetActive(false);
        }
        else
        {
            print("X");
        }
    }
}
