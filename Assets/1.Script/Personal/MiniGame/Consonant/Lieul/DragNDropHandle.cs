using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���콺����Ʈ���� ������Ʈ �����̱�
public class DragNDropHandle : MonoBehaviour
{
    private float z_saved; //z�� �ο���
    private Vector3 posi;
    [SerializeField]
    int num;
    [Tooltip("����Ʈ�� ����� �߷����� ����߷������ ������Ʈ�� ��� �־��ּ���")]
    [SerializeField]
    Rigidbody[] drop; //����Ʈ�� ���� ���۽� �߷����� ����߷���� �ϴ� ������Ʈ
    [Tooltip("����Ʈ�� ����� Ȱ��ȭ ����� �� ������Ʈ ��� �־��ּ���")]
    [SerializeField]
    GameObject[] active; //����Ʈ�� ���� ���۽� Ȱ��ȭ������� ������Ʈ
    [Tooltip("����Ʈ�� ���� ������ ���ο� ���ص� ��ֹ��� ��� �־��ּ���")]
    [SerializeField]
    GameObject[] obstacles; //������߸� ��Ȱ��ȭ�� ���ι��ع�
    private void Start()
    {
        //���� ����ÿ� �ʿ��� ������Ʈ ����߷��ֱ�
        if (drop != null)
        {
            for (int j = 0; j < drop.Length; j++)
                drop[j].useGravity = true;
        }
        //���� ����ÿ� �ʿ��� ������Ʈ Ȱ��ȭ���ֱ� 
        if (active != null)
        {
            for (int k = 0; k < active.Length; k++)
                active[k].SetActive(true);
        }
        FindObjectOfType<NaviMoveManager>().QuizCheck += RemoveObstacles; //�� ��ũ��Ʈ�� ����ִ� ������Ʈ�� Ȱ��ȭ ���ڸ��� �̺�Ʈ�� �Լ� �߰�

    }
    Vector3 GetMouseWorldPosition() //���콺����Ʈ�� ������ǥ�� �ο���
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = z_saved;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDown()
    {
        z_saved = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        posi = gameObject.transform.position - GetMouseWorldPosition();
        if(GetComponent<Rigidbody>() == true) GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + posi;
    }
   
    void RemoveObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++) obstacles[i].gameObject.SetActive(false);
    }
    private void OnMouseUp()
    {
        gameObject.SetActive(false);
        FindObjectOfType<NaviMoveManager>().QuizNum = num; //�Ķ���Ͱ��� ���������ν� �̺�Ʈ ȣ��(�Ķ���ͳ��� �ȿ� �̺�Ʈ ȣ���� ������)
    }
}
