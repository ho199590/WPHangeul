using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    Transform Box;
    [SerializeField]
    GameObject GG;
    
    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.transform.GetComponent<Joint>() == null)
        {
            if (collision.transform.GetComponent<MagnetHandler>())
            {
                return;
            }

            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
            GameObject go = Instantiate(GG, collision.transform.position, collision.transform.rotation, Box) as GameObject;
            
            go.GetComponent<Collider>().enabled = true;
            go.GetComponent<Rigidbody>().isKinematic = false;
            go.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Destroy(collision.gameObject);
        }
    }
}
