using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody _rb;
    private float r;
    private double _gravitationalConstant = 6.67430 * Mathf.Pow(10, -11);
    private Gravity[] objects;

    public Vector3 velocity;
    public float mass = 30;
    public Vector3 gravityAffect;

    void Start()
    {
        this.gameObject.tag = "Gravity";
        if (!this.transform.GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _rb.useGravity = false;
        r = this.transform.GetComponent<Renderer>().bounds.size.x /2;
        _rb.velocity = velocity;
    }

    void Update()
    {
        _rb.velocity += gravityAffect;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Gravity")
        {
            GravityStuff(other.GetComponent<Gravity>().mass,other.transform.position);
        }
    }

    private void GravityStuff(float m2, Vector3 location)
    {
        gravityAffect = Vector3.Normalize(location - this.transform.position) * (float)(_gravitationalConstant * mass * m2 / Mathf.Pow(r,2));
    }
}
