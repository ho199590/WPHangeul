using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandler : MonoBehaviour , IPointer
{
    [SerializeField]
    int nextIndex;
    [SerializeField]
    DirectionFlag.Direction dir;
    [SerializeField]
    bool ChangeLine = false;

    Vector2 nextDir;

    private void Start()
    {
        nextDir = DirectionFlag.GetDirection(dir);
    }
    public (int, Vector2, bool) NextLine()
    {
        return (nextIndex, nextDir, ChangeLine);
    }

    public void RestoreRaycast()
    {
        transform.GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
    }
}
