using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHandle : MonoBehaviour
{
    [SerializeField]
    GameObject mask;

    private void Start()
    {
        FindObjectOfType<LoadingTutorialManager>().action += Cover;
    }
    void Cover()
    {
        mask.SetActive(true);

    }
}
