using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAniHandle : MonoBehaviour
{
    Animator arrowAni;
    [SerializeField]
    GameObject arrowPosi;
    private void Start()
    {
        arrowAni = GetComponent<Animator>();
        StartCoroutine(Speed_forArrow());
        //��⼭ �����Ҹ� ����ϴ� �̺�Ʈ ����
    }
    //�ַο찡 �����̴� ����� ������Ű�鼭 �ִϸ��̼� �ٲ��ִ� �Լ�
    IEnumerator Speed_forArrow()
    {
        yield return new WaitForSeconds(5.5f);
        arrowAni.SetInteger("ArrowAction", 1);
        while (Vector3.Distance(transform.position, arrowPosi.transform.position) > 0.2f) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            transform.position = Vector3.Lerp(transform.position, arrowPosi.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(transform.position, arrowPosi.transform.position) <= 0.2f)
            {
                break;
            }
        }
        transform.position = arrowPosi.transform.position;
        arrowAni.SetInteger("ArrowAction", 2);
        yield break;
    }
}
