using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �̱� ��ǰ ��ġ �� ���� ��Ʈ�ѷ�
public class ClawProductController : MonoBehaviour
{
    #region ����
    [SerializeField]
    Transform basket;
    [Header("�̱� ���")]
    [SerializeField]
    List<GameObject> Products = new List<GameObject>();

    //��ǰ ����
    [SerializeField]
    int ProductNum;
    [SerializeField]
    Transform magenticRoot;

    [SerializeField]
    ClawController GameBox;

    [Header("�̱� ��ǰ ��ġ ��ġ ����")]
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
    // ��ǰ ��ġ
    public void ProductSetting()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        int index = Random.Range(0, Products.Count);

        GameObject product = Instantiate(Products[index], pos, Quaternion.identity, basket);
        product.transform.LookAt(center);


        product.name = "����" +  ProductNum.ToString();
        product.GetComponent<ClawProductHandler>().PrefabNumber = index;
        ProductNum++;
    }

    // ������ ��ǰ�� �����Ͽ� ���ο� ��ǰ���� �����ϱ�
    public void ResetDropProducts(Transform Target, int num)
    {
        Target.gameObject.GetComponent<Collider>().enabled = false;
        Target.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        GameObject go = Instantiate(Products[num], Target.transform.position, Target.transform.rotation, basket) as GameObject;
        go.GetComponent<ClawProductHandler>().PrefabNumber = num;
        go.name = Target.name;

        Destroy(Target.gameObject);
    }

    // �ٴڿ� �浹�Ͽ��� ��� �ڼ��� �ø���
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.root == magenticRoot)
        {
            GameBox.Collide = true;
        }
    }
    #endregion

    //ȭ�鿡 �׸� �׸���
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    
}
