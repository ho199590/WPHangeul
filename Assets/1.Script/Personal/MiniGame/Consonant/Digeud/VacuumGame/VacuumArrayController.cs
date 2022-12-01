using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections;

public enum BlockState
{
    Stage, // 바닥
    Roof,  // 천장
    Wall
}

[System.Serializable]
public class ProductParam
{
    public string name;
    public List<GameObject> productList;
}

public class VacuumArrayController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    BlockState state;

    [SerializeField]
    VacuumLevelHandler Level;
    VacuumLevel def;

    [SerializeField]
    int count;
    [SerializeField]
    float size;

    Vector3 originPos;
    int[] jumpIndex;

    [SerializeField]
    GameObject[] block;
    [SerializeField]
    Transform moventBox;

    [Header("상품 오브젝트")]
    [SerializeField]
    int product_num;
    [Tooltip("클래스 명과 이름을 확실하게")]
    [SerializeField]
    List<ProductParam> products;
    [SerializeField]
    Transform basket;

    [Header("디버그용 인스펙터 창 표기")]
    [SerializeField]
    List<GameObject> list = new List<GameObject>();
    [SerializeField]
    List<GameObject> sub = new List<GameObject>();
    [SerializeField]
    int[] indexs;
    [SerializeField]
    List<int> ban = new List<int>();
    List<VacuumBlockController> tt = new List<VacuumBlockController>();
    List<Transform> blockTransform = new List<Transform>();
    [SerializeField]
    List<Vector3> blockPosList = new List<Vector3>();

    [SerializeField]
    [Range(0, 1)]
    float offset;

    public bool MovementAngine
    {
        set
        {   
            if(value)
            Dir = Random.Range(1, 5);
        }
    }
    #endregion

    private void Awake()
    {
        Level = FindObjectOfType<VacuumLevelHandler>();
        originPos = transform.position;
    }
    #region 테스트용 함수
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            def = Level.Level;
            if (def == VacuumLevel.Normal)
            {
                MakeNormalStage();
            }
            if (def == VacuumLevel.Easy)
            {
                MakeEasyStage();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovemontTest();
        }
    }
    #endregion

    #region Easy
    public void MakeEasyStage()
    {
        RemoveStage();
        GameObject obj = state switch
        {
            BlockState.Stage => block[0],
            BlockState.Roof => block[1],
            BlockState.Wall => block[2],
            _ => null
        };

        for (int i = 0; i < Mathf.Pow(count, 2); i++)
        {
            Vector3 Pos = new Vector3((i % count) * size * transform.localScale.x, 0, i / count * size * transform.localScale.z);

            var stage = Instantiate(obj, transform.position + Pos, Quaternion.identity, transform);
            stage.transform.localScale = new Vector3(stage.transform.localScale.x * size, stage.transform.localScale.y, stage.transform.localScale.z * size);
            stage.name = i.ToString();
        }
        ProductEasySpawn();
    }


    public void ProductEasySpawn()
    {
        RemoveProduct();
        list.Clear();
        list.AddRange(products[product_num].productList);
        List<int> jump = new List<int>();
        jump.Add(product_num);

        // 정답 번호를 제외한 오브젝트를 리스트에 추가하여 모든 칸에 채울 만큼의 숫자가 준비되면 break;
        while (true)
        {
            if (products.Count < 1) { break; }
            int num = RandomNumberPicker.GetRandomNumberByArray(jump.ToArray(), products.Count);
            list.AddRange(products[num].productList);
            jump.Add(num);
            if (jump.Count >= products.Count)
            {
                jump.Clear();
                jump.Add(product_num);
            }
            if (list.Count > (int)Mathf.Pow(count, 2)) { break; }
        }
        // 완성된 배열을 필요한 만큼만 남기고 잘라낸 이후 랜덤한 번호 배정
        sub = list.Take((int)Mathf.Pow(count, 2)).ToList();
        var array = Enumerable.Range(0, sub.Count);
        indexs = array.OrderBy(x => Random.value).ToArray();
        // 알맞은 위치에 배정
        for (int i = 0; i < indexs.Length; i++)
        {
            VacuumBlockController block = transform.GetChild(indexs[i]).GetComponentInChildren<VacuumBlockController>();
            var pro = Instantiate(sub[i], block.GetBlock().Item1, Quaternion.identity, basket);
            if (pro.GetComponent<VacuumAbsorbController>() != null)
            {
                pro.GetComponent<VacuumAbsorbController>().ProductInit(i < products[product_num].productList.Count ? 0 : 1);
            }
        }
    }
    #endregion

    #region Normal
    public void MakeNormalStage()
    {
        int nCount = count + 2;
        moventBox.position = Vector3.zero;

        //필요 기능 => 9칸을 제외한 라인 끄기 , 움직일 수 있도록 등록하기
        transform.DOKill();
        RemoveStage();
        GameObject obj = state switch
        {
            BlockState.Stage => block[0],
            BlockState.Roof => block[1],
            BlockState.Wall => block[2],
            _ => null
        };
        for (int i = 0; i < Mathf.Pow(nCount, 2); i++)
        {
            Vector3 Pos = new Vector3((i % nCount) * size * transform.localScale.x, 0, i / nCount * size * transform.localScale.z);

            var stage = Instantiate(obj, transform.position + Pos, Quaternion.identity, transform);
            stage.transform.localScale = new Vector3(stage.transform.localScale.x * size, stage.transform.localScale.y, stage.transform.localScale.z * size);
            stage.name = i.ToString();

            blockTransform.Add(stage.GetComponent<VacuumBlockController>().block.transform);
            if (i < nCount || (i % nCount == 0) || (i % nCount == nCount - 1) || i >= nCount * (nCount - 1))
            {
                stage.GetComponent<VacuumBlockController>().OffBlock(offset);
            }
            else
            {
                tt.Add(stage.GetComponent<VacuumBlockController>());
            }
            foreach (Transform t in blockTransform)
            {
                t.SetParent(moventBox);
            }

            blockPosList.Add(stage.transform.position);
        }

        ProductNormalSpawn();

        MovementAngine = true;
    }

    public void ProductNormalSpawn()
    {   
        RemoveProduct();
        def = VacuumLevel.Normal;

        list.Clear();
        ban.Clear();
        while (true)
        {
            list.Add(products[product_num].productList[Random.Range(0, products[product_num].productList.Count)]);

            int num = RandomNumberPicker.GetRandomNumberByNum(product_num, products.Count);
            list.Add(products[num].productList[Random.Range(0, products[num].productList.Count)]);

            num = RandomNumberPicker.GetRandomNumberByNum(product_num, products.Count);
            list.Add(products[num].productList[Random.Range(0, products[num].productList.Count)]);

            if (list.Count > (int)Mathf.Pow(count, 2)) { break; }
        }

        for (int i = 0; i < count; i++)
        {
            int num = RandomNumberPicker.GetRandomNumberByArray(ban.ToArray(), (int)Mathf.Pow(count, 2));

            //VacuumBlockController block = transform.GetChild(num).GetComponentInChildren<VacuumBlockController>();
            VacuumBlockController block = tt[num];
            var pro = Instantiate(list[i], block.GetBlock().Item1 + Vector3.up * size, Quaternion.identity, basket);
            if (pro.GetComponent<VacuumAbsorbController>() != null)
            {
                pro.GetComponent<VacuumAbsorbController>().ProductInit(i % count == 0 ? 0 : 1);
                pro.transform.SetParent(moventBox.GetChild(0).transform);
            }

            AddLineBan(num);
        }
    }

    public void AddLineBan(int num)
    {
        int ver = num / count;
        int hor = num % count;

        for (int i = 0; i < Mathf.Pow(count, 2); i++)
        {
            if (i / count == ver || i % count == hor)
            {
                ban.Add(i);
            }
        }
        ban = ban.Distinct().ToList();
    }

    public void MovemontTest()
    {
        Dir = Random.Range(0, 4);
    }

    public void MovementHandle(Vector3 vec)
    {
        moventBox.DOKill();
        moventBox.DOComplete();
        moventBox.DOMove(vec, 2).From(moventBox.position).OnComplete(() => moventBox.DOMove(Vector3.zero, 2)
            .OnComplete(() => MovementAngine = true)
        );        
    }


    public void StopStage()
    {
        if(def == VacuumLevel.Normal)
        {
            moventBox.DOKill();
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {   
        yield return new WaitForSeconds(4);
        Dir = Random.Range(0, 3);
        yield break;
    }

    int direction = 0;
    public int Dir
    {
        get { return direction; }
        set
        {
            Vector3 target = value switch
            {
                1 => new Vector3(-(size * (count + 2)) / 2, 0, 0),
                2 => new Vector3((size * (count + 2)) / 2, 0, 0),
                3 => new Vector3(0, 0, -(size * (count + 2)) / 2),
                4 => new Vector3(0, 0, (size * (count + 2)) / 2),
                _ => Vector3.zero
            };
            MovementHandle(target);
            direction = value;
        }
    }
    public System.Action DirOperation;

    #endregion

    #region 제거용
    public void RemoveStage()
    {
        if (transform.childCount < 1) { return; }

        Transform[] childList = GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }

        Transform[] childList2 = moventBox.transform.GetComponentsInChildren<Transform>();

        if (childList2 != null)
        {
            for (int i = 2; i < childList2.Length; i++)
            {
                if (childList2[i] != basket)
                    Destroy(childList2[i].gameObject);
            }
        }

        blockTransform.Clear();
        blockPosList.Clear();
        RemoveProduct();
    }

    public void RemoveProduct()
    {
        Transform[] childList = basket.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != basket)
                    Destroy(childList[i].gameObject);
            }
        }

        Transform[] childList2 = moventBox.GetChild(0).transform.GetComponentsInChildren<Transform>();

        if (childList2 != null)
        {
            for (int i = 1; i < childList2.Length; i++)
            {
                if (childList2[i] != basket)
                    Destroy(childList2[i].gameObject);
            }
        }
    }

    #endregion
}
