using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���ڷ��̼� ����
public class DecorationHandler : MonoBehaviour
{
    private void Start()
    {
        ClearDeco();
    }

    // ���� �ʱ�ȭ
    public void ClearDeco()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    // ���� ����
    public void SetLine(int num)
    {
        ClearDeco();
        transform.GetChild(num).gameObject.SetActive(true);
    }
}
