using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//씬 로드시 인트로씬 먼저 재생
public class TestIntroduction : MonoBehaviour
{
    void Start()
    {
        LoadingIntroductionManager.LoadScene("Giyeok_Mission"); //StageParam_minigame스크립트에서 다음씬 이름을 변수로 변경해서 사용
    }
}
