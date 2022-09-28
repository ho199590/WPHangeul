using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepherdController : MonoBehaviour
{
    #region º¯¼ö
    Vector3 originRoation;

    private Vector3 screenSpace;
    private Vector3 offset;
    #endregion
    
    private void Start()
    {
        originRoation = transform.rotation.eulerAngles;
    }

    private void OnMouseDown()
    {
        
    }
    private void OnMouseDrag()
    {
        /*
        var curScreenSpace = new Vector3(Input.mousePosition.x, screenSpace.y, Input.mousePosition.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = curPosition;
        */


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "Cube")
            transform.position = hit.point;
        }
        
    }
}
