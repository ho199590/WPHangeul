using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BlockState{
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
// 스테이지 만들기
public class StageArrayController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    BlockState state;

    [SerializeField]
    int count;
    [SerializeField]
    float size;
    int[] jumpIndex;

    [SerializeField]
    GameObject[] block;

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
    List<Transform> movemontList = new List<Transform>();
    [SerializeField]
    int[] indexs;

    #endregion

    #region 테스트용 함수
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MakeStage();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RemoveStage();
        }
    }
    #endregion
    #region 함수

    // count에 맞춰서 count * count 스테이지 생성
    public void MakeStage()
    {
        RemoveStage();        
        GameObject obj = state switch
        {
            BlockState.Stage => block[0],
            BlockState.Roof => block[1],
            BlockState.Wall => block[2],
            _ => null
        };

        if (state == BlockState.Wall)
        {
            var wall = Instantiate(obj, transform);
            wall.transform.localScale = new Vector3(size * count, 20, 1f);
            wall.transform.position = new Vector3(size, transform.parent.position.y + 10, count * size);
            return;
        }

        float alpha = (state == BlockState.Roof) ? 0.5f : 0.5f;        
        for (int i = 0; i < Mathf.Pow(count, 2); i++)
        {
            Vector3 Pos = new Vector3((i % count) * size, 0, i / count * size);
            
            var stage = Instantiate(obj, transform.position + Pos, Quaternion.identity, transform);
            //stage.transform.GetChild(0).localScale *= size;
            Transform t = stage.transform;

            //t = stage.transform.GetChild(0);
            t.localScale = new Vector3(t.localScale.x * size, t.localScale.y, t.localScale.z * size);
            stage.name = i.ToString();

            //stage.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), alpha);
            t.GetComponent<StageBlockHandler>().block.GetComponent<Renderer>().material.color = Color.blue;
        }
        if(state == BlockState.Stage)
        {
            ProductSpawn();
        }
    }

    // 오브젝트 스폰
    public void ProductSpawn()
    {
        RemoveProduct();
        list.Clear();
        list.AddRange(products[product_num].productList);
        List<int> jump = new List<int>();
        jump.Add(product_num);

        // 정답 번호를 제외한 오브젝트를 리스트에 추가하여 모든 칸에 채울 만큼의 숫자가 준비되면 break;
        while (true)
        {
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
        for(int i = 0; i < indexs.Length; i++)
        {
            StageBlockHandler block = transform.GetChild(indexs[i]).GetComponentInChildren<StageBlockHandler>();
            var pro =  Instantiate(sub[i], block.GetBlock().Item1, Quaternion.identity, basket);
            if(pro.GetComponent<VacuumAbsorbHandler>() != null)
            {
                pro.GetComponent<VacuumAbsorbHandler>().ProductInit(i < products[product_num].productList.Count ? 0 : 1);
            }
        }
    }

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
        RemoveProduct();
    }

    public void RemoveProduct()
    {   
        if (basket.childCount < 1 || state != BlockState.Stage) { return; }

        Transform[] childList = basket.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != basket)
                    Destroy(childList[i].gameObject);
            }
        }
    }
    #endregion
}
