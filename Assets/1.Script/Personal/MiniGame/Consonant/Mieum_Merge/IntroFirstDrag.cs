using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IntroFirstDrag : MonoBehaviour
{
    [SerializeField]
    GameObject ob, answerOb,particle , ob2;
    private void Start()
    {
        ob.transform.DOLocalMoveX(-503, 3f).SetLoops(-1, LoopType.Restart);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (transform.name == other.gameObject.name)
        {
            answerOb.SetActive(true);
            transform.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            ParticleOn();
            Invoke("OnAnimal", 1f);     //지연 호출
        }
        if(other.gameObject.name =="GameObject")
        {
            answerOb.SetActive(false);
        }
    }
    private void ParticleOn()
    {
        GameObject par = Instantiate(particle);
        par.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        par.transform.position = new Vector3(answerOb.transform.position.x, answerOb.transform.position.y + 0.5f, answerOb.transform.position.z);
        Destroy(par, 1.5f);
    }
    private void OnAnimal()
    {
        transform.gameObject.SetActive(true);
        ob2.SetActive(true);
    }
}
