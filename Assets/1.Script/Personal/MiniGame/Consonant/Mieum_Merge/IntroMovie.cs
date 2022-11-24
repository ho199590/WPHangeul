using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IntroMovie : MonoBehaviour
{
    [SerializeField]
    GameObject hand;
    [SerializeField]
    GameObject animal;
    private void OnEnable()
    {
        hand.transform.DOLocalMoveX(-0.2f, 2f).SetLoops(-1, LoopType.Restart);
        animal.transform.DOLocalMoveX(0.332f, 2f).SetLoops(-1, LoopType.Restart);
    }
}
