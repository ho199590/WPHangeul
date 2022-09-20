using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterArrowHandler : MonoBehaviour
{
    [SerializeField]
    Transform ArrowView;
    [SerializeField]
    Camera UICamera;

    bool onDraw = false;


    LetterDrawHandler letter;

    private void Start()
    {
        letter = FindObjectOfType<LetterDrawHandler>();

        letter.Draw += OnDraw;
    }

    public void OnDraw(PointerEventData eventData)
    {

        Vector3 vec = new Vector3(
              eventData.position.x,
              eventData.position.y,
              UICamera.nearClipPlane
              );
        transform.position = UICamera.ScreenToWorldPoint(vec);
        ArrowView.position = transform.position;
    }

    public void SetPostion(Vector3 Pos)
    {
        transform.position = Pos;
        ArrowView.position = transform.position;
    }

    public void SetDraw(bool set)
    {
        onDraw = set;
    }

    public bool GetDraw()
    {
        return onDraw;
    }
}
