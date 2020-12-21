﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioSource))]
public class Lightning : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary;
    public AudioSource AudioSourceObject;
    public float audioThreshold = 10;
    public float audioThresholdThunder = 0.035f;
    public float lightningLength = 400;

    private Gradient _gradient;
    private GradientColorKey[] _colorKey;
    private GradientAlphaKey[] _alphaKey;
    private Color[] _colourStorage = new Color[8];

    [Header("SpawnLimits")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    //audio variables
    private float[] _spectrum = new float[512];
    private float[] _frequencyBands = new float[8];

    private void Update()
    {
        AudioData();
    }

    void OnEnable()
    {
        Cursor.visible = false;
        poolDictionary = new Dictionary<int, Queue<GameObject>>();
        GenerateColour(8);

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i ++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        //StartCoroutine("LightningStrike");
    }

    #region Visuals

    private void Zap(int tag, Vector3 position, Quaternion rotation, int frequency)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "doesn't exist.");
            return;
        }
        GameObject poolie = poolDictionary[tag].Dequeue();

        poolie.transform.position = position;
        poolie.transform.rotation = rotation;
        poolie.SetActive(true);

        Material thunderTexture = poolie.GetComponent<Renderer>().material;
        thunderTexture.SetColor("Color_A52EA298", _colourStorage[frequency]);
        Debug.Log(thunderTexture.GetColor("Color_A52EA298"));

        poolie.GetComponent<LightningStrike>().Strike();

        poolDictionary[tag].Enqueue(poolie);
    }

    private void LightningStrike(int tag, Vector3 position, Quaternion rotation, int frequency)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "doesn't exist.");
            return;
        }
        GameObject poolie = poolDictionary[tag].Dequeue();
        //frequency needs to be a value between 0 and 1.

        poolie.transform.position = position;
        poolie.transform.rotation = rotation;

        //gradient schenanigans
        _gradient = new Gradient();

        _colorKey = new GradientColorKey[2];
        _colorKey[0].color = _colourStorage[frequency];
        _colorKey[0].time = 0.0f;
        _colorKey[1].color = _colourStorage[frequency];
        _colorKey[1].time = 1.0f;

        _alphaKey = new GradientAlphaKey[2];
        _alphaKey[0].alpha = 1.0f;
        _alphaKey[0].time = 0.0f;
        _alphaKey[1].alpha = 0.0f;
        _alphaKey[1].time = 1.0f;

        _gradient.SetKeys(_colorKey, _alphaKey);

        poolie.GetComponent<TrailRenderer>().colorGradient = _gradient;
        poolie.GetComponent<TrailRenderer>().Clear();
        poolie.SetActive(true);

        LightningBolt bolt = poolie.GetComponent<LightningBolt>();
        bolt.maxPointDistance = Random.Range(15,40);
        bolt.startHeight = position.y;
        bolt.endHeight = position.y - lightningLength;
        bolt.GeneratePoints();

        poolDictionary[tag].Enqueue(poolie);
    }

    private void GenerateColour(float bands)
    {
        float fraction = 1f / bands;
        for (int i = 0; i < bands; i++)
        {
            _colourStorage[i] = Color.HSVToRGB(fraction * i, 1, 1);
        }
    }

    #endregion

    #region AudioData
    public void AudioData()
    {
        AudioListener.GetSpectrumData(_spectrum, 0, FFTWindow.Blackman);
        int currentSample = 0;

        for(int i = 0; i < _frequencyBands.Length; i ++)
        {
            float averageFreq = 0;
            int sampleCount = (int)Mathf.Pow(2,i) * 2;

            for (int j = 0; j < sampleCount; j++)
            {
                averageFreq += _spectrum[currentSample] * (sampleCount + 1);
                currentSample++;
            }

            averageFreq /= currentSample;
            _frequencyBands[i] = averageFreq * 10;
        }
        for (int i = 0; i < _frequencyBands.Length; i++)
        {
            // 8 bands, each spawns a lightning bolt over a certain threshold.
            if (_frequencyBands[i] >= audioThreshold) LightningStrike(1, new Vector3(Random.Range(minX, maxX), 40, Random.Range(minY, maxY)), Quaternion.Euler(0, 0, 0), i);
            else if (_frequencyBands[i] >= audioThresholdThunder) Zap(0, new Vector3(Random.Range(minX, maxX), 5, Random.Range(minY, maxY)), Quaternion.Euler(0, 0, 0), i);
        }
    }
    #endregion
}
