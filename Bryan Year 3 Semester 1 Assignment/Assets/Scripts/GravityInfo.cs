using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInfo : MonoBehaviour
{
    public float mass;
    public Vector3 initialVelocity;
    public Rigidbody rb;

    private void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
        //rb.mass = mass;
        rb.velocity = initialVelocity;
    }
}
