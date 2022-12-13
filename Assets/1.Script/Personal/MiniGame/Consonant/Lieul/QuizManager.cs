using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Tooltip("���������� ��ֹ����� �־��ּ���")]
[System.Serializable]
public class Obstacles
{
    public GameObject[] roadObstacles; //�迭�ȿ� �迭: ��� ��ֹ�
}
public class QuizManager : MonoBehaviour //������ ����ó�� ���� �Ѱ� �Ŵ���
{
    //new()�� �ʱ�ȭ�����ָ� null������(public���� �ϸ� �ν�����â�� �������� �ϱ� ������ �ʱ�ȭ�� �ѹ� �Ͼ�µ� �����ָ� ���� ����־ null�����߻�)
    public List<GameObject> quizObjects = new(); //Ȱ��ȭ�� <QuizTouchHandle>��ũ��Ʈ�� ����ִ� ������Ʈ�� �־��� ���� �� ����Ʈ
    [SerializeField]
    Obstacles[] perQuizObstacles; //��� ��ֹ�
    int count; //���° �������� üũ��

    public event System.Action<GameObject> QuizCheck; //���� ������ �� �߻��� �̺�Ʈ

    private void Start()
    {
        QuizCheck += RemoveObject;
    }
    //���� ������Ʈ�� Ȱ��ȭ�Ǹ� ����Ʈ�� ������Ʈ �߰� & OnMouseUp�� ������ ����ó���� ���� ������Ʈ�� ����Ʈ���� �����ϰ� �ϴ� ������Ƽ
    public GameObject AddNRemove
    {
        set
        {
            if (!quizObjects.Contains(value))
            {
                quizObjects.Add(value);
                print("���� ó�� ī��Ʈ ��" + quizObjects.Count);

            }
            else
                QuizCheck?.Invoke(value);
            if(quizObjects.Count == 1) //����ó�� ������Ʈ�� �Ѱ����� ��쿡 ����Ϸ� �����ϱ�
            {
                var finds = FindObjectsOfType<QuizTouchHandle>();
                foreach (var find in finds) find.CompletedQuizOrder = count;
            }
        }
    }
    //���° �������� ī��Ʈ�� ������Ƽ //��� ��ֹ� ���ſ� �ε����� ���
    public int QuizOrder
    {
        set
        {
            count++;
            print(count + "��° ���� ");
        }
    }
    //<QuizTouchHandle>��ũ��Ʈ���� OnMouseUp�϶� ����Ʈ���� ������Ʈ �������ִ� �Լ�
    //����Ʈ�� �� ������Ʈ�� Ȱ��ȭ�ɶ� �� �Լ��� �̺�Ʈ�� �߰����ֱ�
    void RemoveObject(GameObject minus)
    {
        if (quizObjects.Count == 1) //����ó�� ������Ʈ �����ϴٰ� ������ �Ѱ��� ���� ��쵵 ����Ϸ� �����ϱ�
        {
            print("���������Ʈ 1��");
            //QuizTouchHandle ��ũ��Ʈ�� ���� ������Ʈ�� ����ֱ� ������ ������ ��� ��ũ��Ʈ�� ������Ƽ�� �Ȱ��� ȣ������� ��
            var finds = FindObjectsOfType<QuizTouchHandle>();
            foreach (var find in finds) find.CompletedQuizOrder = count;
        }
        quizObjects.Remove(minus);
        if (quizObjects.Count == 0) //���� ������Ʈ�� �� �����Ǹ� ��ֹ��� �� ġ��� �׺�޽� �ٽ� ���
        {
            for (int i = 0; i < perQuizObstacles[count-1].roadObstacles.Length; i++)
            {
                perQuizObstacles[count - 1].roadObstacles[i].gameObject.SetActive(false);
            }
            FindObjectOfType<NaviMoveManager>().Check = true; //�ٽ� �׺�޽� �����̰� bool�� �����ֱ�
        }
        print("������� ������Ʈ ���ſϷ�");
    }
}

