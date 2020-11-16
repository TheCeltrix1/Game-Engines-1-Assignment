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

    IEnumerator LightningStrike()
    {
        while (true)
        {
            Zap(0/*Random.Range(0, pools.Capacity)*/, new Vector3(Random.Range(-150, 150), 5, Random.Range(100, 300)), Quaternion.Euler(0, 0, 0));
            LightningStrike(1, new Vector3(0, 5, 200), Quaternion.Euler(0, 0, 0));
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
        poolie.GetComponent<LightningBolt>().GeneratePoints();

        poolDictionary[tag].Enqueue(poolie);
    }

    private void ObjectPool()
    {

    }
}
