using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Touch_Fruit: MonoBehaviour
{
    [SerializeField]
    GameObject Fruit;
    private int ClickNum;
    private float force = 500.0f;
    private void Start()
    {
 
    }
    private void OnMouseDown()
    {
        print("Ŭ��");
        ClickNum++;
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Force);
        rb.useGravity = true;
        if(ClickNum ==5)
        {
            print("������");
            Fruit.GetComponent<Rigidbody>().isKinematic = false;
            ClickNum = 0;
        }
    }
}
