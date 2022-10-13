using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script ���� : �δ����� �ൿ�� �����ϴ� ��ũ��Ʈ
// ���Ͽ� ��� , ���� ��� , ���� -> ���� �̵� , ���� -> ���� �̵�
public enum MoleState {  UnderGround = 0, OnGround , MoveUp, MoveDown}
public class MoleFSM : MonoBehaviour
{
    [SerializeField]
    private float waitTimeOnGround;         //���鿡 �ö�ͼ� �������� ���� ��ٸ��� �ð�
    [SerializeField]
    private float limitMinY;                //������ �� �ִ� �ּ� y ��ġ
    [SerializeField]
    private float limitMaxY;                //�ö�� �� �ִ� �ִ� Y ��ġ

    private Movement3D movement3D;          //��/�Ʒ� �̵��� ���� Movement3D

    //�δ����� ���� ���� (Set�� MoleFSM Ŭ���� ���ο�����)
    public MoleState MoleState { private set; get; }  //property  

    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();

        ChangeState(MoleState.UnderGround);//ó�� �δ����� ���¸� ���� UnderGround(���ӿ� ���)
    }
    public void ChangeState(MoleState newState)
    {
        //������ ������ ToString() �޼ҵ带 �̿��� ���ڿ��� ��ȯ�ϸ�
        //"UnderGround"�� ���� ������ ��� �̸� ��ȯ

        //������ ������̴� ���� ���� 
        StopCoroutine(MoleState.ToString());
        //���� ���� 
        MoleState = newState;
        //���ο� ���� ���
        StartCoroutine(MoleState.ToString());
    }
    /*<summary>
      �δ����� �ٴڿ��� ����ϴ� ���·� ���ʿ� �ٴ� ��ġ�� �δ��� ��ġ ����
     </summary>*/
    private IEnumerator UnderGround()
    {
        //�̵������� : (0,0,0) [����]
        movement3D.MoveTo(Vector3.zero);
        //�δ����� y��ġ�� Ȧ ������ �����ִ� limitMaxY ��ġ�� ����
        transform.position = new Vector3(transform.position.x ,limitMinY, transform.position.z);

        yield return null;
    }
    ///<summary>
    ///�δ����� Ȧ ������ �����ִ� ���·� waitTimeOnGround���� ���
    ///</summary>
    private IEnumerator OnGround()
    {
 
        //�̵������� : (0,0,0) [����]
        movement3D.MoveTo(Vector3.zero);
        //�δ����� y��ġ�� Ȧ ������ �����ִ� limitMaxY ��ġ�� ����
        transform.position = new Vector3(transform.position.x , limitMaxY, transform.position.z);

        //waitTImeOnGround �ð� ���� ���
        yield return new WaitForSeconds(waitTimeOnGround);

        //�δ����� ���¸� MoveDown���� ����
        ChangeState(MoleState.MoveDown);
        print("OnGround");
    }
    ///<summary>
    ///�δ����� Ȧ ������ ������ ���� (maxYPoseOnGround ��ġ���� ���� �̵�)
    ///</summary>
    private IEnumerator MoveUp()
    {
        //�̵����� : (0,1,0) [��]
        movement3D.MoveTo(Vector3.up);

        while(true)
        {
            //�δ����� y��ġ�� limitMaxY�� �����ϸ� ���� ����
            if(transform.position.y >= limitMaxY)
            {
                //OnGround ���·� ����
                ChangeState(MoleState.OnGround);
            }
            yield return null;
        }
    }
    ///<summary>
    ///�δ����� Ȧ�� ���� ���� (minYPosUnderGround ��ġ���� �Ʒ��� �̵�)
    /// </summary>
   private IEnumerator MoveDown()
    {
        //�̵����� : (0,-1,0) [�Ʒ�]
        movement3D.MoveTo(Vector3.down);
        while(true)
        {
            //�δ����� y��ġ�� limitMinY�� �����ϸ� �ݺ��� ����
            if(transform.position.y <= limitMinY)
            {
                //UnderGround ���·� ����
                ChangeState(MoleState.UnderGround);
            }
            yield return null;
        }
    }
}
