using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    public float maxY;       //��ġ�� �ִ� y��ġ
    public float minY;       //��ġ�� �ּ� y��ġ


    public ObjectDetector objectDetector;       //���콺 Ŭ������ ������Ʈ ������ ���� ObjectDetector 
    private Movement3D movement3D;             //��ġ �̵������� movement



    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();

        //OnHit �޼ҵ带 objectDetector Class�� raycast�� �̺�Ʈ�� ���
        //objectDetector�� raycastEvent.Invoke(hit.transform)�޼ҵ尡
        //ȣ��ɶ� ����Onhit �޼ҵ尡 ȣ��ȴ�.
        objectDetector.raycastEvent.AddListener(OnHit);
    }

    private void OnHit(Transform target)
    {
        if (target.CompareTag("Mole"))

        { //Ÿ�� �δ����� ������Ʈ ��������
            MoleFSM mole = target.GetComponent<MoleFSM>();

            //�δ����� ���°� Ȧ �ȿ��������� ���� �Ұ�
            if (mole.MoleState == MoleState.UnderGround)
                return;

            //��ġ�� ��ġ ����
            transform.position = new Vector3(target.position.x, minY, target.position.z);

            //�δ����� �¾����� ����׶��� ���·� ����
            mole.ChangeState(MoleState.UnderGround);

            //ī�޶� ����
            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            //��ġ�� �ٽ� ���� �̵���Ű�� �ڷ�ƾ�Լ� ���
            StartCoroutine("MoveUp");
        }
    }

    private IEnumerator MoveUp()
    {
        //�̵������� ��
        movement3D.MoveTo(Vector3.up);

        while (true)
        {
            if (transform.position.y >= maxY)
            {
                movement3D.MoveTo(Vector3.zero);
                break;

            }

            yield return null;

        }


    }

}