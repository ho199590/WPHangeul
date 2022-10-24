using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawProductHandler : MonoBehaviour
{
    #region 변수
    ClawController clawController;
    ClawProductController product;

    Transform parent;

    public int PrefabNumber;
    #endregion

    private void Start()
    {
        clawController = FindObjectOfType<ClawController>();
        clawController.MagnetPutDown += PutDownObject;

        product = FindObjectOfType<ClawProductController>();

        parent = transform.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Stage")
        {
            if (GetComponent<Joint>())
            {
                if (GetComponent<Joint>().connectedBody == null && collision.transform.parent != parent)
                {
                    transform.SetParent(collision.transform);
                    GetComponent<Joint>().connectedBody = collision.rigidbody;
                    clawController.AddBodyPart(gameObject);
                    gameObject.layer = 0;

                    GetComponent<Joint>().autoConfigureConnectedAnchor = false;
                    //자식 갯수로 판별시 꾸미기 어려움이 있음
                    //GetComponent<Joint>().connectedAnchor = collision.transform.childCount < 3 ? Vector3.down * 0.5f : Vector3.down * 2.1f;                    
                    GetComponent<Joint>().connectedAnchor = !collision.transform.GetComponent<MagnetHandler>() ? Vector3.down * 0.5f : Vector3.down * 2.1f;
                    //자석 올리기 이벤트 호출
                    clawController.MagnetCollisionInvoke();
                }
                else if (GetComponent<Joint>().connectedBody != collision.rigidbody)
                {
                    clawController.MagnetCollisionInvoke();
                }
            }
            //GetComponent<Joint>(). = 50;
        }
        if (!GetComponent<Joint>())
        {
            if(GetComponent<Rigidbody>().velocity.magnitude < 1 && GetComponent<Rigidbody>().velocity.y >= 0)
            {
                if (collision.transform.GetComponent<ClawProductController>() != null)
                product.ResetDropProducts(transform, PrefabNumber);
            }
        }
    }

    // 잡혀있는 오브젝트 떨어트리기
    public void PutDownObject()
    {
        GetComponent<Collider>().isTrigger = true;
        if (transform.parent.GetComponent<Rigidbody>())
        {
            transform.SetParent(null);

            int r = Random.Range(10, 20);
            Vector3 RandomForce = new Vector3(Random.Range(-r, r), -2, Random.Range(-r, r));
            GetComponent<Rigidbody>().AddForce(RandomForce);
            

            GetComponent<Joint>().breakForce = 0;
            transform.parent = parent;
        }
    }


    // 오브젝트가 사라질 경우 이벤트 제외
    private void OnDestroy()
    {
        clawController.MagnetPutDown -= PutDownObject;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.transform.GetComponent<ClawProductController>() != null)
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
