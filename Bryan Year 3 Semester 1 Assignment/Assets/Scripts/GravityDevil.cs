using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDevil : MonoBehaviour
{
    private GravityInfo[] _affectedObjects;
    private double _gravitationalConstant = 6.67430 * Mathf.Pow(10, -6);
    private int i = 0;
    private GravityInfo _sun;
    private float _gravityScalar = 7.25f;

    void OnEnable()
    {
        _affectedObjects = FindObjectsOfType<GravityInfo>();
        for(int i = 0; i < _affectedObjects.Length; i++)
        {
            if (_affectedObjects[i].name == "Sun")
            {
                _sun = _affectedObjects[i];
            }
        }
        for (int i = 0; i < _affectedObjects.Length; i++)
        {
            if (_affectedObjects[i].name != "Sun")
            {
                _affectedObjects[i].initialVelocity = StableOrbit(_affectedObjects[i].transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (object obj in _affectedObjects)
        {
            for (int j = (i + 1); j < _affectedObjects.Length; j++)
            {
                GravityInfo var1 = _affectedObjects[i].GetComponent<GravityInfo>();
                GravityInfo var2 = _affectedObjects[j].GetComponent<GravityInfo>();

                var1.rb.velocity += Vector3.Normalize(_affectedObjects[j].transform.position - _affectedObjects[i].transform.position) * ((float)(_gravitationalConstant * var1.mass * var2.mass / Mathf.Pow(Vector3.Distance(_affectedObjects[j].transform.position, _affectedObjects[i].transform.position), 2)) / var1.mass);
                var2.rb.velocity += Vector3.Normalize(_affectedObjects[i].transform.position - _affectedObjects[j].transform.position) * ((float)(_gravitationalConstant * var1.mass * var2.mass / Mathf.Pow(Vector3.Distance(_affectedObjects[j].transform.position, _affectedObjects[i].transform.position), 2)) / var2.mass);
            }
            i++;
        }
        i = 0;
    }

    private Vector3 StableOrbit(Vector3 pos)
    {
        float velocity = Mathf.Sqrt((float)(_gravitationalConstant * _sun.mass) / Vector3.Distance(pos,_sun.transform.position));
        Vector3 normal = _sun.transform.position - pos;
        Vector3 angle = Vector3.zero;
        Vector3.OrthoNormalize(ref normal, ref angle);
        return angle * velocity * _gravityScalar;
    }
}