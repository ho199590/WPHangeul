using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//�ְ� ���� �̺�Ʈ Args;
public class CountParameter : EventArgs
{
    public GameObject gameObject;
    public int count1;
    public int transformIndex;
}

public class ForCount : MonoBehaviour
{
    public event EventHandler<CountParameter> Connect; //<�Ű�����> : CountParameter��� Ŭ������ �Ű������� �ϴ� �̺�Ʈ�ڵ鷯
    public CountParameter countParameter = new CountParameter(); //CountParameterŬ�����ȿ� �ִ� �������� ����ϰ���

    int count2;
    
    public int CountPro //��� �̼��� �ϼ��ߴ��� ī��Ʈ�ϴ� ������Ƽ
    {
        get => count2; //�о����:count���� �о�´�.
        set //����:����Ѵ�
        {
            count2 = value;
            countParameter.count1 = value;
            Connect?.Invoke(this, countParameter);
        }
    }
    
}

