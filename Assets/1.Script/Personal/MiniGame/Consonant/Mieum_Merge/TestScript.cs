using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    GameObject Particle;
    int i =0;
    private void OnMouseUp()
    {
/*        this.transform.parent = null;
        Destroy(objectHitPostion);*/
    }

    private void OnMouseDown()
    {
/*        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitRay))
        {
            objectHitPostion = new GameObject("HitPosition");
            objectHitPostion.transform.position = hitRay.point;
            this.transform.SetParent(objectHitPostion.transform);
        }*/
    }

    private void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = 0.7f;
        transform.position = objPos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.contacts[0].point);
        GameObject effect = Instantiate(Particle);
        effect.transform.position = collision.contacts[0].point;
        Destroy(effect, 1f);
    }    
}
