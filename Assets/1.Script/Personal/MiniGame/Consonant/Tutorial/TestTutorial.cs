using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//씬 로드시 튜토리얼씬 먼저 재생
public class TestTutorial : MonoBehaviour
{
    void Start()
    {
        LoadingTutorialManager.LoadScene("Giyeok_Mission");
    }
}
