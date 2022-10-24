using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Tooltip ("���������� ��ֹ����� �־��ּ���")]
[System.Serializable]
public class obstacles
{
    public GameObject[] roadObstacles; //�迭�ȿ� �迭: ��� ��ֹ�
}
//������ ����ó�� �Ŵ���
public class QuizManager : MonoBehaviour
{
    public List<GameObject> quizObjects; //Ȱ��ȭ�� <QuizTouchHandle>��ũ��Ʈ�� ����ִ� ������Ʈ�� �־��� ���� �� ����Ʈ
    [SerializeField]
    List<GameObject> obstacles; //���� ���� �� �������� ��ֹ�
    [SerializeField]
    obstacles[] perQuizObstacles;
    int count;
    public event System.Action<GameObject> QuizCheck; //���� ������ �� �߻��� �̺�Ʈ

    private void Start()
    {
        QuizCheck += RemoveObject;
    }
    //<QuizTouchHandle>��ũ��Ʈ�� ����ִ� ������Ʈ�� Ȱ��ȭ�Ǹ� �������Ե� ������Ƽ
    public GameObject ActiveObject
    {
        set
        {
            quizObjects.Add(value);
            print("���� ó�� ī��Ʈ ��" + quizObjects.Count);
        }
    }
    //<QuizTouchHandle>��ũ��Ʈ���� OnMouseUp�� ������Ʈ �����༭ ����Ʈ���� �����ϰ� �ϴ� ������Ƽ
    public GameObject MouseUpCheck
    {
        set
        {
            QuizCheck?.Invoke(value);
        }
    }
    //���° �������� ī��Ʈ�� ������Ƽ //��� ��ֹ� ���ſ� �ε����� ���
    public int QuizOrder
    {
        set
        {
            print(count++ + "��° ���� ");
        }
    }
    //<QuizTouchHandle>��ũ��Ʈ���� OnMouseUp�϶� ����Ʈ���� ������Ʈ �������ִ� �Լ�
    //����Ʈ�� �� ������Ʈ�� Ȱ��ȭ�ɶ� �� �Լ��� �̺�Ʈ�� �߰����ֱ�
    void RemoveObject(GameObject minus)
    {
        print("������� ������Ʈ ����");
        quizObjects.Remove(minus);
        if (quizObjects.Count == 0) //���� ������Ʈ�� �� �����Ǹ� ��ֹ��� �� ġ��� �׺�޽� �ٽ� ���
        {
            for(int i = 0; i < perQuizObstacles[count-1].roadObstacles.Length; i++)
            {
                perQuizObstacles[count - 1].roadObstacles[i].gameObject.SetActive(false);
            }
            FindObjectOfType<NaviMoveManager>().Check = true; //�ٽ� �׺�޽� �����̰� bool�� �����ֱ�
        }
    }
}

