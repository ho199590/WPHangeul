using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_AnimalChoice : MonoBehaviour
{
    //Random_Animal ����ȭ 
    [SerializeField]
    Random_Animal Animal;
    //���° �������� number���ڷ� üũ
    public int number;
    //�θ��� �θ� Random_Animal ��ũ��Ʈ�� ���� �迭 ��ȣ�� ���� �ڱ� �ڽ� ���� ������Ʈ Ȱ��ȭ
    private void Start()
    {
        Animal = transform.parent.GetComponentInParent<Random_Animal>();
        transform.GetChild(Animal.shuffledArray[transform.parent.GetSiblingIndex()]).gameObject.SetActive(true);
        number = Animal.shuffledArray[transform.parent.GetSiblingIndex()];
    }
}
