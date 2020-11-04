using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    private RaycastHit _hit;
    public GameObject obj;

    public void Strike()
    {
        this.transform.rotation = Quaternion.Euler(Random.Range(-30,30),0, Random.Range(-30, 30));
        Physics.Raycast(this.transform.position,-this.transform.up, out _hit, Mathf.Infinity);
        GetComponent<ParticleSystem>().Play();
        //Instantiate(obj,_hit.point, this.transform.rotation);
    }
}
