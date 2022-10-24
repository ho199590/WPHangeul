using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawProductController : MonoBehaviour
{
    #region 변수
    [SerializeField]
    Transform basket;
    [Header("뽑기 목록")]
    [SerializeField]
    List<GameObject> Products = new List<GameObject>();

    //상품 갯수
    [SerializeField]
    int ProductNum;

    [Header("뽑기 상품 배치 위치 조정")]
    [SerializeField]
    Vector3 center;
    [SerializeField]
    Vector3 size;
    #endregion

    #region

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ProductSetting();
        }
    }
    public void ProductSetting()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        int index = Random.Range(0, Products.Count);

        GameObject product = Instantiate(Products[index], pos, Quaternion.identity, basket);
        product.transform.LookAt(center);


        product.name = "생성" +  ProductNum.ToString();
        product.GetComponent<ClawProductHandler>().PrefabNumber = index;
        ProductNum++;
    }


    public void ResetDropProducts(Transform Target, int num)
    {
        Target.gameObject.GetComponent<Collider>().enabled = false;
        Target.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        GameObject go = Instantiate(Products[num], Target.transform.position, Target.transform.rotation, basket) as GameObject;
        go.GetComponent<ClawProductHandler>().PrefabNumber = num;

        Destroy(Target.gameObject);
    }
    #endregion







    //화면에 그림 그리기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
