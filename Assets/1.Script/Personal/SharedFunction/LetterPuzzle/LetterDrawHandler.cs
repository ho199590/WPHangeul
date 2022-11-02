using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 그리기 제어자
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
    PUzzleMoveController moveCon;

    float outTime = 0;
    int curLineIndex = 0;

    bool onDraw = false;
    bool pressMouse = false;
    bool Success = false;

    [SerializeField]
    Canvas canvas;
    PointerEventData ED;
    PointerEventData PD;

    [SerializeField]
    AudioClip[] ClickSound;
    #endregion

    #region 이벤트
    //모두 리셋용 이벤트    
    public event System.Action<PointerEventData> Ready;
    public event System.Action<PointerEventData> Draw;
    public event System.Action<PointerEventData> Erase;
    public event System.Action<PointerEventData> Comp;
    public event System.Action<PointerEventData> Reset;
    #endregion
    private void Start()
    {
        dirCheck = GetComponent<CheckDirectionScript>();
        score = FindObjectOfType<ScoreHandler>();
        speaker = FindObjectOfType<SpeakerHandler>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        moveCon = FindObjectOfType<PUzzleMoveController>();

        Erase += CleanWord;
        Reset += ResetPlayer;


        moveCon.Next += CleanWord;
        moveCon.Next += ResetPlayer;
        

        arrow = FindObjectOfType<LetterArrowHandler>();
        DrawReady();

        transform.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.004f;
    }

    public void DrawReady()
    {
        arrow.SetPostion(LetterStartPosition[0].position);
        arrow.ArrowSpriteSetting();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach (var item in result)
        {
            if (item.gameObject.GetComponent<LetterArrowHandler>() != null)
            {
                PD = eventData;

                speaker.SoundByClip(ClickSound[0]);

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
        if (onDraw)
        {
            // 이벤트 데이터의 델타값과 현재 라인 플래그 값으로 방향성 체크
            if (dirCheck.CheckDir(eventData.delta, DirectionFlag.GetDirection(directions[curLineIndex])))
            {
                if (!arrow.GetDraw()) { return; }
                outTime = 0;
                Draw?.Invoke(eventData);
                if (!speaker.GetComponent<AudioSource>().isPlaying)
                {   
                    speaker.SoundByClip(ClickSound[1]);
                }

                foreach (var item in result)
                {
                    // 인터페이스가 상속된 경우에만 실행
                    if (item.gameObject.GetComponent<IPointer>() != null)
                    {
                        // 디렉션 카운터보다 작고, 디렉션이 알맞을 경우
                        if (item.gameObject.GetComponent<IPointer>().NextLine().Item1 < directions.Count &&
                            item.gameObject.GetComponent<IPointer>().NextLine().Item2 ==
                           DirectionFlag.GetDirection(directions[item.gameObject.GetComponent<IPointer>().NextLine().Item1]))
                        {
                            if (transform.GetChild(curLineIndex) != null)
                            {
                                transform.GetChild(curLineIndex).GetComponent<Image>().fillAmount = 1;
                            }

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
                        // 디렉션 백터가 0에 인덱스가 현재치 +1이 대상 => 끝라인에 도착하였을 경우
                        else if (item.gameObject.GetComponent<IPointer>().NextLine().Item2 == Vector2.zero
                                    && item.gameObject.GetComponent<IPointer>().NextLine().Item1 == curLineIndex + 1)
                        {
                            item.gameObject.GetComponent<Image>().raycastTarget = false;
                            Success = true;                            
                            score.SetScore();
                            moveCon.SucessSeqMaker();
                            if (transform.GetChild(curLineIndex) != null)
                            {   
                                transform.GetChild(curLineIndex).GetComponent<Image>().fillAmount = 1;
                                Reset(eventData);
                            }

                        }
                    }
                }
            }
            else // 일정시간 틀렸을 경우 리셋
            {
                outTime += Time.deltaTime * 10;
                if (outTime > 0.5f)
                {             
                    Reset?.Invoke(eventData);
                    Erase?.Invoke(eventData);
                    outTime = 0;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Success)
        {
            arrow.HiddenView();
        }

        ED = eventData;

        pressMouse = false;        
        onDraw = false;

        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);

        bool Check = false;
        foreach(RaycastResult r in result)
        {
            if (r.gameObject.GetComponent<PointHandler>())
            {
                Check = true;
                break;
            }
        }
        if (!Check && !Success)
        {
            Reset?.Invoke(eventData);
            Erase?.Invoke(eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressMouse)
        {
            ED = eventData;

            if (!Success)
            {
                Erase?.Invoke(eventData);
                Reset?.Invoke(eventData);
            }
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
        Success = false;
        ResurrectPoint();
    }

    public void ResetPlayer(PointerEventData eventData)
    {
        curLineIndex = 0;
        onDraw = false;
        pressMouse = false;
        arrow.SetDraw(false);
        arrow.SetPostion(LetterStartPosition[0].position);
    }


    public PointerEventData GetED()
    {
        return PD;
    }
    public void ResurrectPoint()
    {
        var ar =  FindObjectsOfType<PointHandler>();
        foreach(PointHandler p in ar)
        {
            p.Resurrect();
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
