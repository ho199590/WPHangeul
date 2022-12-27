using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    #region 변수
    [SerializeField]
    GameObject explosion;//풍선 폭팔 파티클 
    [SerializeField]
    GameObject Animal;//동물 오브젝트
    public Vector3 BalloonPosition;
    SpeakerHandler speakerHandler;
    private float size = 0.8f; //원하는 사이즈
    #endregion
    #region
    private void Start()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>(); 
    }
    private void OnMouseDown()
    {
        if(transform.root.GetComponent<Canvas>())
        {
            var te = Instantiate(explosion);
            te.transform.localScale = Vector3.one * 5f;
            Vector3 vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            te.transform.position = Camera.main.ScreenToWorldPoint(vec);
            StartCoroutine(Down());
            TouchClear();
            ColOnOff();
        }
    }
    protected void TouchClear()
    {
        speakerHandler.SoundByNum2(0);
        Animal.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.SetActive(false);
    }
    private void ColOnOff()
    {
        Animal.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Down()
    {
        while (Animal.transform.localScale.x >= size)
        {
            Animal.transform.localScale = new Vector3(Animal.transform.localScale.x - 0.01f ,
            Animal.transform.localScale.y - 0.01f , Animal.transform.localScale.z - 0.01f);
        }
        yield break;

    }
    public void SizeReset()
    {
        Animal.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}

