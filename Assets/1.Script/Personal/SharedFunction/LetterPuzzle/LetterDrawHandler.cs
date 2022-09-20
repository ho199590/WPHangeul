using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LetterDrawHandler : MonoBehaviour, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    #region 변수        
    [SerializeField]
    List<DirectionFlag.Direction> directions;
    
    //여러 획일 경우 다음 획의 스타트 포인트용.
    [SerializeField]
    Transform[] LetterStartPosition;

    CheckDirectionScript dirCheck;
    GraphicRaycaster raycaster;
    ScoreHandler score;
    LetterArrowHandler arrow;
    SpeakerHandler speaker;

    float outTime = 0;
    int curLineIndex = 0;

    bool onDraw = false;
    bool pressMouse = false;

    [SerializeField]
    Canvas canvas;
    PointerEventData EE;
    #endregion

    #region 이벤트
    //모두 리셋용 이벤트    
    public event System.Action<PointerEventData> Ready;
    public event System.Action<PointerEventData> Draw;
    public event System.Action<PointerEventData> Erase;
    #endregion
    private void Start()
    {
        dirCheck = GetComponent<CheckDirectionScript>();
        score = FindObjectOfType<ScoreHandler>();
        speaker = FindObjectOfType<SpeakerHandler>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();

        Erase += CleanWord;

        arrow = FindObjectOfType<LetterArrowHandler>();
        arrow.SetPostion(LetterStartPosition[0].position);

        transform.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.004f;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach (var item in result)
        {
            if (item.gameObject.GetComponent<LetterArrowHandler>() != null)
            {
                Ready?.Invoke(eventData);
                onDraw = true;
                arrow.SetDraw(true);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        if (onDraw && arrow.GetDraw())
        {
            if (dirCheck.CheckDir(eventData.delta, DirectionFlag.GetDirection(directions[curLineIndex])))
            {
                if (!arrow.GetDraw()) { return; }
                print("CHECK!");
                outTime = 0;
                Draw?.Invoke(eventData);

                foreach (var item in result)
                {
                    if (item.gameObject.GetComponent<IPointer>() != null)
                    {
                        if (item.gameObject.GetComponent<IPointer>().NextLine().Item1 < directions.Count &&
                            item.gameObject.GetComponent<IPointer>().NextLine().Item2 ==
                           DirectionFlag.GetDirection(directions[item.gameObject.GetComponent<IPointer>().NextLine().Item1]))
                        {
                            item.gameObject.GetComponent<Image>().raycastTarget = false;
                            curLineIndex = item.gameObject.GetComponent<IPointer>().NextLine().Item1;


                            if (item.gameObject.GetComponent<IPointer>().NextLine().Item3)
                            {
                                onDraw = false;

                                arrow.SetPostion(LetterStartPosition[curLineIndex].position);
                                arrow.SetDraw(false);

                                eventData.pointerDrag = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressMouse)
        {
            EE = eventData;

            Erase?.Invoke(eventData);
            onDraw = false;
        }
    }

    public void CleanWord(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
                if (transform.GetChild(i).GetComponent<Image>() != null)
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressMouse = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressMouse = false;
    }
}
