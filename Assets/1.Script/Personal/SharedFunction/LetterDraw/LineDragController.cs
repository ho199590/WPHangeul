using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineDragController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    Camera _camera;

    [SerializeField]
    Transform startPoint;
    Vector3 pos;
    #endregion
    
    #region 함수
    private void Start()
    {
        pos = transform.position;
        SetStartPoint(0);
    }

    public void ShowMouse(PointerEventData eventData)
    {                
        Vector3 vec = new Vector3(
            eventData.position.x,
            eventData.position.y,
            pos.z
           );
        transform.position = _camera.ScreenToWorldPoint(vec);        
    }

    public void SetStartPoint(int num)
    {
        transform.position = startPoint.GetChild(num).position;
    }
    #endregion
}
