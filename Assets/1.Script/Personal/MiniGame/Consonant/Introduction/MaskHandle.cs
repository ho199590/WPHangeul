using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ī�޶� �����ÿ� ����ũ ������Ʈ ���ֱ����� �̺�Ʈ ����
public class MaskHandle : MonoBehaviour
{
    [SerializeField]
    GameObject mask;

    private void Start()
    {
        FindObjectOfType<LoadingIntroductionManager>().actionMask += Cover;
    }
    void Cover()
    {
        mask.SetActive(true);
    }
}
