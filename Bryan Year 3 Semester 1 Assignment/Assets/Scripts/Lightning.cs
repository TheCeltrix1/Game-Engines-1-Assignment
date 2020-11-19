using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        AudioData();
    }

    void Start()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

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
        StartCoroutine("LightningStrike");
    }

    #region Visuals

    IEnumerator LightningStrike()
    {
        while (true)
        {
            //Register sound to spawn thunder and lightning (reversed cause I named things wrong). Lightning strike for impactful notes and Zap for beats and repetitive notes.
            Zap(0, new Vector3(Random.Range(-150, 150), 5, Random.Range(100, 300)), Quaternion.Euler(0, 0, 0));
            LightningStrike(1, new Vector3(Random.Range(-150, 150), 40, Random.Range(100, 300)), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Zap(int tag, Vector3 position, Quaternion rotation)
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
        poolie.GetComponent<LightningStrike>().Strike();

        poolDictionary[tag].Enqueue(poolie);
    }

    private void LightningStrike(int tag, Vector3 position, Quaternion rotation)
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

        LightningBolt bolt = poolie.GetComponent<LightningBolt>();
        bolt.maxPointDistance = Random.Range(15,40);
        bolt.startHeight = position.y;
        bolt.endHeight = position.y -200;
        bolt.GeneratePoints();

        poolDictionary[tag].Enqueue(poolie);
    }
    #endregion

    #region AudioData
    public void AudioData()
    {
        float[] spectrum = new float[64];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
    }

    #endregion
}
