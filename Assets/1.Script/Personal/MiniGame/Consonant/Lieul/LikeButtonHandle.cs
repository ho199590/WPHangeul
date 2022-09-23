using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeButtonHandle : MonoBehaviour
{
    
    private void OnMouseEnter()
    {
        print("OnMouseEnter");
        GetComponent<MeshRenderer>().material.color = new Color(1, 0.8f, 0, 1);
    }
    private void OnMouseDown()
    {
        print("OnMouseDown");
        GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0.8f, 0, 1);
    }
    private void OnMouseUp()
    {
        print("OnMouseUp");
        GetComponent<MeshRenderer>().material.color = new Color(1, 0.8f, 0, 1);
    }
    //private void OnMouseUpAsButton()
    //{
    //    print("OnMouseUpAsButton?");
    //}
    private void OnMouseExit()
    {
        print("OnMouseExit");
        GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.8f, 0, 0.5f);
    }
}
