using UnityEngine;
// ���ò� ��Ʈ�ѷ�
public class FishermanController : MonoBehaviour
{
    #region ����
    // ���𰡸� ��Ҵ�
    public bool grabbed = false;
    // �⺻ ����
    Ray moRay;
    //������ ��Ұ�, ���� ��� ���ΰ�?
    public Transform catchTransform;
    public Transform ground;
    // ���� ���� ���̾�� ���� ���̾�
    public LayerMask catchLayer;
    public LayerMask groundLayer;
    // ������� ���̿� ���� ����
    RaycastHit hit;
    RaycastHit groundHit;
    // ���콺 ��Ŀ ���� ������
    public Transform mousePosMarker;
    RaycastHit mousePosHit;
    public float offsetY = 0;
    public float mouseposOffsetFromGround = 0;
    public Vector3 mousePos;

    public Collider water;

    #endregion
    #region
    private void Start()
    {
        hit = new RaycastHit();
        groundHit = new RaycastHit();
        mousePosHit = new RaycastHit();
    }

    private void Update()
    {
        moRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            
            CatchObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }

        grabbed = catchTransform != null;
        mousePosMarker.gameObject.SetActive(grabbed);

        if (grabbed)
        {
            TraceMousePostion();

        }
    }
    // ������Ʈ ���
    void CatchObject()
    {
        if(Physics.Raycast(moRay, out hit, Mathf.Infinity, catchLayer))
        {
            catchTransform = hit.transform;
            catchTransform.GetComponent<Rigidbody>().isKinematic = true;
            //catchTransform.GetComponent<BuoyancyHandler>().enabled = false;
            FindGround();
            water.enabled = true;

        }
    }
    // ���� ã��
    void FindGround()
    {
         if(Physics.Raycast(catchTransform.position, Vector3.down, out groundHit, Mathf.Infinity, groundLayer))
        {
            ground = groundHit.transform; 
        }
    }

    void TraceMousePostion()
    {
        if(Physics.Raycast(moRay, out mousePosHit, Mathf.Infinity, groundLayer))
        {
            mousePos = mousePosHit.point;
            catchTransform.position = new Vector3(mousePos.x, mousePos.y + offsetY, mousePos.z);            
            mousePosMarker.position = new Vector3(mousePos.x, mousePos.y + mouseposOffsetFromGround, mousePos.z);
        }
    }
    // ������Ʈ ����Ʈ����
    void DropObject()
    {
        if (catchTransform != null)
        {
            catchTransform.GetComponent<Rigidbody>().isKinematic = false;
            

        }
        catchTransform = null;
        ground = null;
        water.enabled = false;
    }
    #endregion

    #region
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 wordMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 wordMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(wordMousePosNear, wordMousePosFar - screenMousePosNear, out hit);

        return hit;
    }
    #endregion

}
