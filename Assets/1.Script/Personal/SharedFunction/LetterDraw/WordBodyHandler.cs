using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WordBodyHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerExitHandler
{
    #region 변수
    [SerializeField]
    List<DirectionFlag.Direction> directions;
    CheckDirectionScript dirCheck;

    int curLineIndex = 0;
    float time;
    bool onSprite = false;

    ScoreHandler score;
    SpeakerHandler speaker;
    LineDragController line;

    GraphicRaycaster raycaster;
    
    [Header("클릭 효과음")]
    [SerializeField]
    AudioClip clips;
    #endregion
    #region 함수
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.004f;
        
        

        
        raycaster = transform.root.GetComponent<GraphicRaycaster>();

        dirCheck = GetComponent<CheckDirectionScript>();

        score = FindObjectOfType<ScoreHandler>();
        speaker = FindObjectOfType<SpeakerHandler>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach(var hit in result)
        {
            if (hit.gameObject.GetComponent<LineDragController>())
            {
                time = 0;
                onSprite = true;

                if (clips)
                {   
                    speaker.SoundByClip(clips);
                }
                else
                {
                    print("PSP");
                }
            }
        }        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onSprite)
        {
            List<RaycastResult> result = new List<RaycastResult>();
            raycaster.Raycast(eventData, result);
            foreach (var hit in result)
            {
                if (dirCheck.CheckDir(eventData.delta, DirectionFlag.GetDirection(directions[curLineIndex])))
                {
                    if (hit.gameObject.GetComponent<LineDragController>())
                    {
                        line = hit.gameObject.GetComponent<LineDragController>();                        
                        line.ShowMouse(eventData);
                        time = 0;
                    }
                }
                else
                {
                    time += Time.deltaTime;
                    if (time > 1)
                    {
                        onSprite = false;
                    }
                }

                if (hit.gameObject.GetComponent<IPointer>() != null)
                {
                    if (hit.gameObject.GetComponent<IPointer>().NextLine().Item1 < directions.Count &&
                    hit.gameObject.GetComponent<IPointer>().NextLine().Item2 ==
                    DirectionFlag.GetDirection(directions[hit.gameObject.GetComponent<IPointer>().NextLine().Item1]))
                    {
                        hit.gameObject.GetComponent<Image>().raycastTarget = false;
                        curLineIndex = hit.gameObject.GetComponent<IPointer>().NextLine().Item1;

                        if (hit.gameObject.GetComponent<IPointer>().NextLine().Item3)
                        {
                            line.SetStartPoint(curLineIndex);
                            score.SetScore();
                        }
                    }
                    else if(hit.gameObject.GetComponent<IPointer>().NextLine().Item2 == Vector2.zero
                            && hit.gameObject.GetComponent<IPointer>().NextLine().Item1 == curLineIndex + 1)
                    {
                        hit.gameObject.GetComponent<Image>().raycastTarget = false;
                        score.SetScore();
                    }
                }
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onSprite)
        {

        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach (var hit in result)
        {
            if (hit.gameObject.GetComponent<LineDragController>())
            {
                onSprite = true;

                if (clips)
                {
                    speaker.SoundByClip(clips);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onSprite = false;
    }
    #endregion
}
