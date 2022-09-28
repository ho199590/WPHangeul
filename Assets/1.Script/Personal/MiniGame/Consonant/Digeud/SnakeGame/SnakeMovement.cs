using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeMovement : MonoBehaviour
{
    #region 변수    
    [SerializeField]
    List<Transform> BodyParts = new List<Transform>();

    [SerializeField]
    float mindistance, speed, rotationSpeed;

    [SerializeField]
    GameObject bodyPrefab;

    [SerializeField]
    int beginSize;

    [SerializeField]
    Transform StartPoint, Target;

    private float dis;
    private Transform curBodyParts;
    private Transform prevBodyParts;

    Tweener snakeMove;
    Tweener snakeRotation;
    #endregion

    #region 함수

    private void Start()
    {
        StartLevel();
        snakeMove = BodyParts[0].DOMove(Target.position, 3).SetAutoKill(false);
        snakeRotation = BodyParts[0].DOLookAt(Target.position, 1).SetAutoKill(false);
    }

    public void StartLevel()
    {
        for (int i = 0; i < beginSize; i++)
        {
            AddBodyPart();
        }

        BodyParts[0].position = StartPoint.position;
    }

    private void Update()
    {
        SnakeMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBodyPart();
        }
    }

    public void SnakeMove()
    {
        float curSpeed = speed;

        /*
        BodyParts[0].Translate(BodyParts[0].forward * curSpeed * Time.smoothDeltaTime, Space.World);

        if(Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Input.GetAxis("Horizontal"));
        }
        */


        //BodyParts[0].LookAt(new Vector3(BodyParts[0].position.x, Target.position.y, Target.position.z));
        //snakeRotation.ChangeEndValue(Target.position, 1).Restart();
        
        snakeMove.ChangeEndValue(new Vector3(Target.position.x, BodyParts[0].position.y, Target.transform.position.z), .3f, true).Restart();
        BodyParts[0].DOLookAt(Target.position, 1);


        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyParts = BodyParts[i];
            prevBodyParts = BodyParts[i-1];

            dis = Vector3.Distance(prevBodyParts.position, curBodyParts.position);

            Vector3 newpos = prevBodyParts.position;

            newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis / mindistance * curSpeed;
            if (T > 0.5f){T = 0.5f;}            

            curBodyParts.position = Vector3.Slerp(curBodyParts.position, newpos, T);
            curBodyParts.rotation = Quaternion.Slerp(curBodyParts.rotation, prevBodyParts.rotation, T);
        }
    }
    public void AddBodyPart()
    {
        Transform newpart = ((Instantiate(bodyPrefab, BodyParts[BodyParts.Count-1].position, BodyParts[BodyParts.Count - 1].rotation)) as GameObject).transform;
        newpart.SetParent(transform);

        BodyParts.Add(newpart);
    }

    public void AddBodyPart(GameObject obj)
    {
        Transform newpart = ((Instantiate(obj, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation)) as GameObject).transform;
        newpart.SetParent(transform);

        BodyParts.Add(newpart);
    }


    #endregion
}
