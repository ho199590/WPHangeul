using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Touch : MonoBehaviour
{
    #region ����
    [SerializeField]
    GameObject Animal;//���� ������Ʈ
    int BalloonTouch=0;//ǳ�� ��ġ ī��Ʈ
    public Vector3 BalloonPosition;
    SpeakerHandler speakerHandler;
    //[ǳ���� ������ �����۾����� ����]
    private float size = 0.8f; //���ϴ� ������
    private float speed = 1f; //�۾� ������ �ӵ�
    private float time = 0;
    #endregion
    #region

    private void Start()
    {
        speakerHandler = FindObjectOfType<SpeakerHandler>();
        
    }
    private void OnMouseDown()
    {
        BalloonTouch++;
        if(BalloonTouch == 3)//�ټ��� Ŭ�������� 
        {
            StartCoroutine(Down());
            TouchClear();
            ColOnOff();
            BalloonTouch = 0;
        }
    }
    protected void TouchClear()//ǳ�� ���� 
    {
        speakerHandler.SoundByNum2(0);
        Animal.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void ColOnOff()//�ٴ۰� �浹 �ϱ����� �Լ�
    {
        Animal.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Down()
    {
        while (Animal.transform.localScale.x > size)
        {
            Animal.transform.localScale = new Vector3(Animal.transform.localScale.x - 1f * speed * Time.deltaTime,
                Animal.transform.localScale.y - 1f * speed * Time.deltaTime,
                Animal.transform.localScale.z - 1f * speed * Time.deltaTime);
            time += Time.deltaTime;
            if (Animal.transform.localScale.x <= size)
            {
                time = 0;
                break;
            }
            yield return null;
        }

    }
    public void SizeReset()
    {
        Animal.transform.localScale = new Vector3(1f, 1f, 1f);
        
        print($"{Animal}����");
    }
    #endregion
}
