using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ʈ���� �޴޸��� ���� ������Ʈ
public class TreeMakerGiftHandler : MonoBehaviour
{
    #region ����
    
    #endregion
    #region ������Ƽ
    // 0 ���� , 1 ����, 2 ��ź
    public int giftProperty;
    public int GiftProperty
    {
        set
        {
            Operate = value switch
            {
                0 => GiftForAnswer,
                1 => GiftForWrong,
                2 => GiftForBomb,
                _ => null
            };
            giftProperty = value;
        }
    }
    public System.Action Operate;
    #endregion
    #region �Լ�
    // ���� => ȸ���ϸ� �޸��� + 1�� ���
    public void GiftForAnswer()
    {

    }
    // ���� => ���� 1�� �ϰ�
    public void GiftForWrong()
    {

    }
    // ��ź => 2�� �ϰ�
    public void GiftForBomb()
    {

    }
    #endregion

    #region �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TreeMakerGiftHandler>() != null) { return; }

    }
    #endregion
    
}
