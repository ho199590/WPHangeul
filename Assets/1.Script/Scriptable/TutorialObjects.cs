using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NarrationSetContainer.cs 참고
//튜토리얼에서 사용될 Scriptable Object
[CreateAssetMenu(menuName = "Scriptable/Tutorial", fileName = "TutorialObjects")]
[System.Serializable]
public class IntroductionObjects
{
    public GameObject Object;
    public AudioClip perAudio;
}
public class TutorialObjects : ScriptableObject
{
    [SerializeField]
    IntroductionObjects[] Giyeok, Nieun, Digeud, Lieul, Mieum, Bieub, Sioat, Ieong, Jieuj, Chioat, Kieuk, Tigeut, Pieup, Hieuh;
    Dictionary<string, IntroductionObjects[]> Index = new Dictionary<string, IntroductionObjects[]>();

    [SerializeField]
    GameObject[] eviroments;
    public GameObject[] Eviroments { get => eviroments; }
    public Dictionary<string, IntroductionObjects[]> GetIndex()
    {
        return Index;
    }
    public void Insert()
    {
        Index.Clear();
        Index.Add("Giyeok", Giyeok);
        Index.Add("Nieun", Nieun);
        Index.Add("Digeud", Digeud);
        Index.Add("Lieul", Lieul);
        Index.Add("Mieum", Mieum);
        Index.Add("Bieub", Bieub);
        Index.Add("Sioat", Sioat);
        Index.Add("Ieong", Ieong);
        Index.Add("Jieuj", Jieuj);
        Index.Add("Chioat", Chioat);
        Index.Add("Kieuk", Kieuk);
        Index.Add("Tigeut", Tigeut);
        Index.Add("Pieup", Pieup);
        Index.Add("Hieuh", Hieuh);
    }
}
