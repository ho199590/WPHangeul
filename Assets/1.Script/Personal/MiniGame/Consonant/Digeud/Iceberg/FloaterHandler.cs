using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterHandler : MonoBehaviour
{
    #region 변수
    WaveManager waveManager;

    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    public float accel = 1;
    //public float waterHeight = 0f;
    #endregion

    #region 함수

    private void Awake()
    {   
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = waveManager.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight * accel) {
            float displacementMultiplier = Mathf.Clamp01((waveHeight * accel - transform.position.y) / depthBeforeSubmerged ) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f) ,transform.position ,ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
    #endregion
}
