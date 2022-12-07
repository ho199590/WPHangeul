using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

[System.Serializable]
class GiftParam
{
    public string name;
    public List<GameObject> gifts = new List<GameObject>();
}

enum TreeMakerLevel
{
    Easy,
    Normal,
    Hard
}

public class TreeMakerMovementController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    TreeMakerLevel Level;

    [SerializeField]
    List<Transform> movePoints = new List<Transform>();
    //거리, 속도, 회전속도
    [SerializeField]
    float mindistance, speed, rotationSpeed;
    float timer;

    [SerializeField]
    Transform walker;

    // 줄세우기
    private float dis;
    private Transform curBodyParts;
    private Transform prevBodyParts;

    [Header("트레인 등록")]
    [SerializeField]
    int giftListNum;
    [SerializeField]
    List<GiftParam> giftParams;
    // 등록될 내용물
    [SerializeField]
    GameObject bomb;

    [Header("디버그용")]
    [SerializeField]
    List<Transform> TrainParts = new List<Transform>();

    [SerializeField]
    int check;
    #endregion
    #region 이벤트
    public event System.Action CameraTurn;    
    #endregion
    #region 프로퍼티
    public int rootNum;
    int RootNum
    {
        get { return rootNum; }
        set
        {
            timer = (value % 3) switch
            {
                0 => 8f,
                6 => 3,
                _ => 3

            };

            if (value % 3 == 1) CameraTurn?.Invoke();

            TrainRailSet(value);
            rootNum = value;
        }
    }
    #endregion
    #region 함수
    #region 디버그
    private void Start()
    {
        AddBodyPart(InitTrain());

        RootNum = check;
    }

    private void Update()
    {
        FollowTheHead();
        if (Input.GetKeyDown(KeyCode.P))
        {
            //TestTrain = InitTrain();
            AddBodyPart(InitTrain());
        }
    }
    #endregion
    #region 오브젝트 장식 관련
    public void WalkerSled(GameObject obj)
    {
        SplineWalker sp = walker.GetComponent<SplineWalker>();

        Transform ty = Instantiate(obj, walker.transform.position, Quaternion.identity, null).transform;
        ty.SetParent(walker);

        sp.passenger = ty;
        sp.gameObject.SetActive(true);
    }
    #endregion
    #region 경로 구현
    public void TrainRailSet(int num)
    {
        Transform tr = TrainParts[0];
        int set = num % movePoints.Count;
        int target = (num + 1) % movePoints.Count;

        tr.DOMove(movePoints[target].position, timer).From(movePoints[set].position).SetEase(Ease.Linear).OnComplete(() => RootNum++);
    }
    #endregion
    #region 기차 구현
    public void TrainMove()
    {
        float curSpeed = speed;
        for (int i = 1; i < TrainParts.Count; i++)
        {
            curBodyParts = TrainParts[i];
            prevBodyParts = TrainParts[i - 1];

            dis = Vector3.Distance(prevBodyParts.position, curBodyParts.position);

            Vector3 newpos = prevBodyParts.position;

            newpos.y = TrainParts[0].position.y;

            float T = Time.deltaTime * dis / mindistance * curSpeed;
            if (T > 0.5f) { T = 0.5f; }

            curBodyParts.position = Vector3.Slerp(curBodyParts.position, newpos, T);
            curBodyParts.rotation = Quaternion.Slerp(curBodyParts.rotation, prevBodyParts.rotation, T);
        }
    }

    public void FollowTheHead()
    {
        float curSpeed = speed;
        float dos = mindistance;

        if (TrainParts.Count > 1) 
        //TrainParts[1].position = TrainParts[0].position;

        for (int i = 1; i < TrainParts.Count; i++)
        {
            curBodyParts = TrainParts[i];
            prevBodyParts = TrainParts[i - 1];

            var heading = prevBodyParts.position - curBodyParts.transform.position;

            if (heading.sqrMagnitude > dos)
            {
                curBodyParts.transform.position = Vector3.Lerp(curBodyParts.transform.position, prevBodyParts.position, Time.deltaTime * curSpeed);
                prevBodyParts.transform.LookAt(curBodyParts);
            }
        }
    }
    #endregion
    #region 파츠 추가 관련
    public void AddBodyPart(GameObject obj)
    {
        Transform newpart = ((Instantiate(obj, TrainParts[TrainParts.Count - 1].position, TrainParts[TrainParts.Count - 1].rotation)) as GameObject).transform;
        newpart.SetParent(transform);

        TrainParts.Add(newpart);
    }
    public void AddBodyPart(List<GameObject> list)
    {
        RemoveBodyPartAll();

        var array = Enumerable.Range(0, list.Count);
        int[] indexs = array.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < list.Count; i++)
        {
            GameObject gift = Instantiate(list[indexs[i]], TrainParts[0].position, Quaternion.identity, transform);
            TrainParts.Add(gift.transform);
            gift.name = i.ToString();
        }
    }
    // 트레인을 위한 오브젝트 리스트 생성
    public List<GameObject> InitTrain()
    {
        List<GameObject> list = new List<GameObject>();
        List<GameObject> anotherList = new List<GameObject>();
        anotherList.AddRange(giftParams[RandomNumberPicker.GetRandomNumberByNum(giftListNum, giftParams.Count)].gifts);
        
        int num = Random.Range(0, giftParams[giftListNum].gifts.Count);
        
        GameObject answerObj = giftParams[giftListNum].gifts[num];
        GameObject wrongObj = anotherList[Random.Range(0, anotherList.Count)];

        if(wrongObj.GetComponent<TreeMakerGiftHandler>() != null)
        wrongObj.GetComponent<TreeMakerGiftHandler>().giftProperty = 1;

        if (Level == TreeMakerLevel.Easy)
        {
            List<int> index = new List<int>();
            index.Add(num);

            if (giftParams[giftListNum].gifts.Count > 2)
            {
                int sub = RandomNumberPicker.GetRandomNumberByNum(num, giftParams[giftListNum].gifts.Count);
                index.Add(sub);

                int last = RandomNumberPicker.GetRandomNumberByArray(index.ToArray(), giftParams[giftListNum].gifts.Count);
                list.Add(answerObj);
                list.Add(giftParams[giftListNum].gifts[sub]);
                list.Add(giftParams[giftListNum].gifts[last]);

                foreach(GameObject g in list)
                {
                    if(g.GetComponent<TreeMakerGiftHandler>() != null)
                    g.GetComponent<TreeMakerGiftHandler>().giftProperty = 0;
                }
            }
            else
            {
                list.Add(answerObj);
                list.Add(answerObj);
                list.Add(answerObj);
            }
        }
        if (Level == TreeMakerLevel.Normal)
        {
            list.Add(answerObj);
            GameObject obj2 = giftParams[giftListNum].gifts[Random.Range(0, giftParams[giftListNum].gifts.Count)];
            list.Add(obj2);
            list.Add(wrongObj);
        }
        if (Level == TreeMakerLevel.Hard)
        {
            list.Add(answerObj);
            list.Add(wrongObj);
            GameObject giftBomb = bomb;
            if (giftBomb.GetComponent<TreeMakerGiftHandler>() != null) giftBomb.GetComponent<TreeMakerGiftHandler>().giftProperty = 2;
            list.Add(giftBomb);
        }
        return list;
    }
    #endregion
    #region 제거 관련
    public void RemoveBodyPart()
    {
        if (TrainParts.Count > 1)
        {
            Transform tt = TrainParts[TrainParts.Count - 1];
            TrainParts.RemoveAt(TrainParts.Count - 1);
        }
    }

    public void RemoveBodyPart(int num)
    {
        if (TrainParts.Count > 1)
        {
            Transform tt = TrainParts[num+1];
            TrainParts.RemoveAt(num+1);
            WalkerSled(tt.gameObject);
            Destroy(tt.gameObject);
        }
    }

    public void RemoveBodyPart(Transform t)
    {   
        int num = t.GetSiblingIndex();        
        if (TrainParts.Count > 1)
        {
            Transform tt = TrainParts[num + 1];
            WalkerSled(tt.gameObject);
            TrainParts.RemoveAt(num + 1);            
            Destroy(tt.gameObject);

            if(TrainParts.Count <= 1)
            {
                AddBodyPart(InitTrain());
            }
        }
    }

    public void RemoveBodyPartAll()
    {
        Transform eng = TrainParts[0];
        for(int i = 1; i < TrainParts.Count; i++)
        {
            Destroy(TrainParts[i].gameObject);            
        }

        TrainParts.Clear();
        TrainParts.Add(eng);
    }
    #endregion
    #endregion
}
