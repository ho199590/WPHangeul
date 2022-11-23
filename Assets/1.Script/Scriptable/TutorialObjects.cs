using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Tutorial", fileName = "TutorialObjects")]

[System.Serializable]
public class IntroductionObjects
{
    public GameObject Object;
    public AudioClip perAudio;
}
public class TutorialObjects : ScriptableObject
{
    //[SerializeField]
    public IntroductionObjects[] Giyeok, Nieun, Digeud, Lieul, Mieum, Bieub, Sioat, Ieong, Jieuj, Chioat, Kieuk, Tigeut, Pieup, Hieuh;
    public IntroductionObjects[] GiyeokObjects { get => Giyeok; }
    public IntroductionObjects[] NieunObjects { get => Nieun; }
    public IntroductionObjects[] DigeudObjects { get => Digeud; }
    public IntroductionObjects[] LieulObjects { get => Lieul; }
    public IntroductionObjects[] MieumObjects { get => Mieum; }
    public IntroductionObjects[] BieubObjects { get => Bieub; }
    public IntroductionObjects[] SioatObjects { get => Sioat; }
    public IntroductionObjects[] IeongObjects { get => Ieong; }
    public IntroductionObjects[] JieujObjects { get => Jieuj; }
    public IntroductionObjects[] KieukObjects { get => Kieuk; }
    public IntroductionObjects[] TigeutObjects { get => Tigeut; }
    public IntroductionObjects[] PieupObjects { get => Pieup; }
    public IntroductionObjects[] HieuhObjects { get => Hieuh; }
}
