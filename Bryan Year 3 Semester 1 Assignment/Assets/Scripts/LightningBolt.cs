using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    private List<Vector3> _locations = new List<Vector3>();
    private float _timer = 0;
    private float _despawnTimer = 0.5f;
    private bool _despawn;
    private int _var = 0;

    public float startHeight;
    public float endHeight;
    private float _currentHeight;
    private Vector2 _pointPos; // X and Z pos of current point
    private float _maxPointDistance = 50;

    private float _minHeightDifference = 5;
    private float _maxHeightDifference = 15;

    public void GeneratePoints()
    {
        _var = 0;
        _timer = 0;
        _despawn = false;
        if (_locations != null) _locations.Clear();
        _currentHeight = startHeight;
        _pointPos = new Vector2(this.transform.position.x,this.transform.position.z);

        while(_currentHeight > endHeight)
        {
            _currentHeight -= Random.Range(_minHeightDifference,_maxHeightDifference);
            _locations.Add(new Vector3(_pointPos.x + Random.Range(-_maxPointDistance,_maxPointDistance), _currentHeight, _pointPos.y + Random.Range(-_maxPointDistance, _maxPointDistance)));
        }
        _despawn = true;
        Strike();
    }

    private void Strike()
    {
        foreach (Vector3 vec in _locations)
        {
            //Debug.Log(transform.name + " " + vec);
            //this.transform.position = vec;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_despawn == true && _timer >= _despawnTimer)
        {
            _timer = 0;
            _var++;
            Debug.Log(_var);
            this.transform.position = _locations[_var];
            //gameObject.SetActive(false);
        }
    }
}
