using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeMakerMovementController : MonoBehaviour
{
    #region ����
    [SerializeField]
    List<Transform> movePoints = new List<Transform>();
    //�Ÿ�, �ӵ�, ȸ���ӵ�
    [SerializeField]
    float mindistance, speed, rotationSpeed;
    float timer;
    // ����
    [SerializeField]
    int beginSize;

    // �ټ����
    private float dis;
    private Transform curBodyParts;
    private Transform prevBodyParts;
    


    [Header("Ʈ���� ���")]
    // ��ϵ� ���빰
    [SerializeField]    
    GameObject bomb;

    [Header("����׿�")]
    [SerializeField]
    List<Transform> TrainParts = new List<Transform>();

    [SerializeField]
    int check;
    #endregion

    #region ������Ƽ
    public int rootNum;
    int RootNum
    {
        get { return rootNum; }
        set
        {
            timer = (value % 3) switch
            {
                0 => 15f,
                6 => 3,
                _ => 3
            };
            
            TrainRailSet(value);
            rootNum = value;
        }
    }
    #endregion
    #region �Լ�
    #region �����
    private void Update()
    {
        //TrainMove();
        FollowTest();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RootNum = check;
        }
    }
    #endregion

    #region ��� ����
    public void TrainRailSet(int num)
    {
        Transform tr = TrainParts[0];
        int set = num % movePoints.Count;
        int target = (num + 1) % movePoints.Count;

        tr.DOMove(movePoints[target].position, timer).From(movePoints[set].position);

    }
    #endregion
    #region ���� ����
    public void TrainMove()
    {
        float curSpeed = speed;
        for (int i = 1; i < TrainParts.Count; i++)
        {
            curBodyParts = TrainParts[i];
            prevBodyParts = TrainParts[i - 1];

            dis = Vector3.Distance(prevBodyParts.position, curBodyParts.position);

            Vector3 newpos = prevBodyParts.position;

            newpos.y = TrainParts[0].position.y;

            float T = Time.deltaTime * dis / mindistance * curSpeed;
            if (T > 0.5f) { T = 0.5f; }

            curBodyParts.position = Vector3.Slerp(curBodyParts.position, newpos, T);
            curBodyParts.rotation = Quaternion.Slerp(curBodyParts.rotation, prevBodyParts.rotation, T);
        }
    }

    public void FollowTest()
    {
        float curSpeed = speed;

        for (int i = 1; i < TrainParts.Count; i++)
        {
            curBodyParts = TrainParts[i];
            prevBodyParts = TrainParts[i - 1];

            var heading = prevBodyParts.position - curBodyParts.transform.position;


            if (heading.sqrMagnitude > mindistance)
            {
                curBodyParts.transform.position = Vector3.Lerp(curBodyParts.transform.position, prevBodyParts.position, Time.deltaTime * curSpeed);

                prevBodyParts.transform.LookAt(curBodyParts);
            }
        }
    }
        #endregion
        #region ���� �߰� ����
        public void AddBodyPart(GameObject obj)
    {
        Transform newpart = ((Instantiate(obj, TrainParts[TrainParts.Count - 1].position, TrainParts[TrainParts.Count - 1].rotation)) as GameObject).transform;
        newpart.SetParent(transform);

        TrainParts.Add(newpart);
    }

    #endregion
    #region ���� ����
    public void RemoveBodyPart()
    {
        if (TrainParts.Count > 2)
        {
            Transform tt = TrainParts[TrainParts.Count - 1];
            TrainParts.RemoveAt(TrainParts.Count - 1);

            tt.SetParent(null);
            tt.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        }
    }
    #endregion

    #endregion
}
