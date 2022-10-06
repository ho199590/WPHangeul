using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//collision, trigger 충돌
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
                mRigidBody.AddForce(Vector3.right *Input.GetAxis("Horizontal")*10); //Rigidbody의 Use Gravity사용해줘야 제대로 적용
            }
            if (Input.GetButton("Vertical"))
            {
                //mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
                mRigidBody.AddForce(Vector3.forward *Input.GetAxis("Vertical")*10); //Rigidbody의 Use Gravity사용해줘야 제대로 적용
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
    //public Collider Rotation
    //{
    //    get => other;
    //}
    private void OnCollisionEnter(Collision collision) //Collider에 Is Trigger 체크안되어 있어야 충돌 //통과안됨(물리연산O)
    {
        if (collision != null)
        {
            print("온콜리전엔터" + collision.gameObject.name);
            //공 확대해주는 거
            //if (mAudioSource != null && HitSound != null && collision.relativeVelocity.y > .5f)
            //{
            //    mAudioSource.PlayOneShot(HitSound, collision.relativeVelocity.magnitude);
            //}
        }
    }
    public void OnTriggerEnter(Collider other) //Collider에 Is Trigger 체크되어있어야 충돌 //통과가능(물리연산X)
    {
        if (other != null)
        {
            print("온트리거엔터"+other);
            //Rotation = other;
            transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, Time.deltaTime*50);
            other.gameObject.SetActive(false);
            if (other.gameObject.name.Contains("Fin"))
            {
                print("도착!");
            }
        }
    }
}
   
