using System.Linq;
using UnityEngine;


public class DRunFlowerBloomController : MonoBehaviour
{
    enum Constant
    {
        ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��
    }

    #region ����    
    int[] indexArray;

    [SerializeField]
    GameObject dotoriPre;

    [Header("������ �ٱ��� / ������")]
    [SerializeField]
    Transform pot;
    [SerializeField]
    GameObject flowerPrefab;

    [Header("���丮 ��� / ����")]
    [SerializeField]
    Constant targetNum;
    [SerializeField]
    int dCount;
    #endregion


    private void Awake()
    {
        indexArray = new int[transform.childCount];
        var arr = System.Linq.Enumerable.Range(0, transform.childCount);
        indexArray = arr.OrderBy(x => Random.value).ToArray();
        FlowerInit();
    }


    public void FlowerInit()
    {
        for (int i = 0; i < indexArray.Length; i++)
        {
            BloomingFlower(i);
        }
    }

    public void BloomingFlower(int i)
    {
        var flower = Instantiate(flowerPrefab, pot);
        if (i < dCount)
        {
            flower.GetComponent<DRunObjectController>().SettingParam((int)targetNum);
            var dotori = Instantiate(dotoriPre, flower.transform);

        }
        else
        {
            flower.GetComponent<DRunObjectController>().SettingParam(RandomNumberPicker.GetRandomNumberByNum((int)targetNum, 14));
        }
        flower.transform.position = transform.GetChild(indexArray[i]).position + new Vector3(0, 0, -0.15f);
        flower.name = "Flower" + indexArray[i];
        flower.GetComponent<DRunObjectController>().SetPointINdex(i);
    }

    #region �Ķ���� �Լ�
    public int GetDotoriCount()
    {
        return dCount;
    }
    public int GetTargetNum()
    {
        return (int)targetNum;
    }

    public int[] GetIndexArray()
    {
        return indexArray;
    }
    #endregion

}