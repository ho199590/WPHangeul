using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//�Ⱦ��� ��ũ��Ʈ
public class AI_AnimalMove2 : MonoBehaviour
{
    [SerializeField]
    Transform targetPos; //�i�ư��� target ��ġ
    NavMeshAgent nav;
    float range = 5; //���� ����
    Vector3 point; 
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //target�� �Ÿ��� 3�����̸� point ��ȯ
        if (Vector3.Distance(transform.position, targetPos.position) < 8f)
        {
            if (RandomPoint(targetPos.position, range, out point))
            {
                targetPos.position = point;
            }
        }
        nav.SetDestination(targetPos.position);
    }
    //�������� Point ����
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
