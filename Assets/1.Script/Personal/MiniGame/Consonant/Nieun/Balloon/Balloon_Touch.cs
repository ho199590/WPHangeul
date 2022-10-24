using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    #region ����
    [SerializeField]
    GameObject explosion;//ǳ�� ���� ��ƼŬ 
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    //int BalloonTouch=0;//ǳ�� ��ġ ī��Ʈ
    public Vector3 BalloonPosition;
    SpeakerHandler speakerHandler;
    //[ǳ���� ������ �����۾����� ����]
    private float size = 0.8f; //���ϴ� ������
    #endregion
    #region
    private void Start()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>(); 
    }
    //���콺�� ǳ���� Ŭ�������� �߻� 
    private void OnMouseDown()
    {
        /*����Ŭ��������
          BalloonTouch++;
                if(BalloonTouch == 3)
                {
                    StartCoroutine(Down());
                    TouchClear();
                    ColOnOff();
                    BalloonTouch = 0;
                }*/
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
    protected void TouchClear()//ǳ�� ���� 
    {
        speakerHandler.SoundByNum2(0);
        Animal.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.SetActive(false);
    }
    private void ColOnOff()//�ٴ۰� �浹 �ϱ����� �Լ�
    {
        Animal.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Down()//���� ���� �۾��� 
    {
        while (Animal.transform.localScale.x >= size)
        {
            Animal.transform.localScale = new Vector3(Animal.transform.localScale.x - 0.01f ,
            Animal.transform.localScale.y - 0.01f , Animal.transform.localScale.z - 0.01f);
        }
        yield break;

    }
    public void SizeReset()//���н� ���� ũ�� ����
    {
        Animal.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}

