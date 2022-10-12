using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �Ⱦ��� �Լ�
public class BalanceObjectController : MonoBehaviour
{
    #region �Լ�
    [SerializeField]
    private int weight;
    public bool Clone = true;

    [SerializeField]
    Transform SpawnPoint;

    private Rigidbody rig;    

    private Vector3 screenSpace;
    private Vector3 offset;
    #endregion


    #region �Լ�

    private void Start()
    {    
        rig = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {   
        rig.useGravity = false;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {

            // hit.point;
        }
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        
        if (Clone)
        {
            var sig = Instantiate(transform);
            sig.position = SpawnPoint.position;
            sig.GetComponent<BalanceObjectController>().Clone = false;
        }
    }

    private void OnMouseDrag()
    {   
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = new Vector3(curPosition.x, curPosition.y);        
    }

    private void OnMouseUp()
    {
        rig.useGravity = true;
    }

    #endregion

    #region �Ķ����
    public void SetWeight(int num){weight = num;}
    public int GetWeight(){return weight;}
    #endregion
}
