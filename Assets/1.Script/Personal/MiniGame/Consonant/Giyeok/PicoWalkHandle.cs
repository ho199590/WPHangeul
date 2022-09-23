using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PicoWalkHandle : MonoBehaviour
{
    [Tooltip("���ڰ� �̵��Ұ��� ������Ʈ�� �־��ּ���")]
    [SerializeField]
    GameObject picoPosition; //���ڰ� �̵��� ��
    private Animator animator;
    int[] clickArray = { 6, 7, 8, 9, 10 }; //����ڰ� ���ڸ� Ŭ���� ������ �ٲ�� �� �ִϸ��̼��� ���� �ε��� ������ �迭
    System.Random random = new System.Random();

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("PicoAction", 5);
        StartCoroutine(Speed_forPico());
    }
    //�� ���۽ÿ� ���ڰ� �ɾ ���� õõ�� �����Բ� ���ִ� ���� �Լ�
    IEnumerator Speed_forPico()
    {
        while (Vector3.Distance(transform.position, picoPosition.transform.position) > 0.2f) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            transform.position = Vector3.Lerp(transform.position, picoPosition.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(transform.position, picoPosition.transform.position) <= 0.2f)
            {
                break;
            }
        }
        transform.position = picoPosition.transform.position;
        transform.Rotate(0, -70, 0);
        animator.SetInteger("PicoAction", 4); //���ھִϸ��̼� �߿� 4�� �θ����θ����ѱ�
        yield break;
    }
    //���ڸ� Ŭ���Ҷ����� �������� �ִϸ��̼��� �ٲ��ִ� �Լ�
    private void OnMouseDown()
    {
        int[] shuffle = clickArray.OrderBy(x => random.Next()).ToArray();
        animator.SetInteger("PicoAction", shuffle[0]);
    }
}
