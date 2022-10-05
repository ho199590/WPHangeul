using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//collision, trigger �浹
public class TriggerCollisionHandle : MonoBehaviour
{
    private Rigidbody mRigidBody;
    private AudioSource mAudioSource;
    public AudioClip JumpSound;
    public AudioClip HitSound;
    public Camera ViewCamera;
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (mRigidBody != null)
        {
            if (Input.GetButton("Horizontal"))
            {
                //mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
                mRigidBody.AddForce(Vector3.right *Input.GetAxis("Horizontal")*10); //Rigidbody�� Use Gravity�������� ����� ����
            }
            if (Input.GetButton("Vertical"))
            {
                //mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
                mRigidBody.AddForce(Vector3.forward *Input.GetAxis("Vertical")*10); //Rigidbody�� Use Gravity�������� ����� ����
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (mAudioSource != null && JumpSound != null)
                {
                    mAudioSource.PlayOneShot(JumpSound);
                }
                mRigidBody.AddForce(Vector3.up*200);
            }
        }
        if (ViewCamera != null)
        {
            //Vector3 direction = (Vector3.up*2+ Vector3.back*4);
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position, Color.red);
            if (Physics.Linecast(transform.position, transform.position, out hit))
            {
                ViewCamera.transform.position = hit.point;
            }
            else
            {
                ViewCamera.transform.position = transform.position;
            }
            ViewCamera.transform.LookAt(transform.position  + transform.forward);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            print("���ݸ�������" + collision);
            //�� Ȯ�����ִ� ��
            //if (mAudioSource != null && HitSound != null && collision.relativeVelocity.y > .5f)
            //{
            //    mAudioSource.PlayOneShot(HitSound, collision.relativeVelocity.magnitude);
            //}
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            print("��Ʈ���ſ���"+other);
            transform.rotation = Quaternion.Slerp(transform.rotation, other.transform.rotation, Time.deltaTime*50);
            other.gameObject.SetActive(false);
            if (other.gameObject.name.Contains("Fin"))
            {
                print("����!");
            }
        }
    }
}
