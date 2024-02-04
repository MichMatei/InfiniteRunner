using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;
    ManagerScript managerScript;

    //List used to load the 2 youngest chunks and make sure that the newest one is 30 units back from the older one
    //so that there is no gap between the chunks
    //It preloads the 2 oldest chunks in the scene at the start.
    public List<GameObject> spawnedObjects;
    public GameObject endStarterChunk0;
    public GameObject endStarterChunk1;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        managerScript = ManagerScript.Instance;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        spawnedObjects.Add(endStarterChunk0);
        spawnedObjects.Add(endStarterChunk1);
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Error");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        //Used for fixing the gap between the chunks
        if (spawnedObjects.Count == 2)
        {
            spawnedObjects.Remove(spawnedObjects[0]);
        }

        //Used for fixing the gap between the chunks
        if (spawnedObjects.Count < 3)
        {
            spawnedObjects.Add(objectToSpawn);
        }

        //Used for fixing the gap between the chunks and to decide if the spawned chunk should be rotated up or down for the transition chunk
        if (spawnedObjects.Count == 2)
        {
            if (managerScript.lastZone == 1 && managerScript.zonePublic == 1)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 30;
            }
            else if ((managerScript.zonePublic == 0 && managerScript.lastZone == 0) || (managerScript.zonePublic == 0 && managerScript.zoneLenghtPublic > 1))
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 30;
            }
            else if (managerScript.zonePublic == 0 && managerScript.zoneLenghtPublic == 0 && managerScript.lastZone == 1)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 29.33f + Vector3.up * 3.84f;
            }
            else if (managerScript.zonePublic == 0 && managerScript.zoneLenghtPublic == 1 && managerScript.lastZone == 1)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 29.33f + Vector3.up * 3.84f;
            }
            else if (managerScript.zonePublic == 1 && managerScript.zoneLenghtPublic == 0 && managerScript.lastZone == 0)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 29.33f + Vector3.down * 3.84f;
            }
            else if (managerScript.zonePublic == 1 && managerScript.zoneLenghtPublic == 1 && managerScript.lastZone == 0)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 29.33f + Vector3.down * 3.84f;
            }
            else if (managerScript.zonePublic == 1 && managerScript.zoneLenghtPublic > 1)
            {
                spawnedObjects[1].transform.position = spawnedObjects[0].transform.position + Vector3.forward * 30f;
            }
        }

        ChunkSpawner.zoneLength++;
        managerScript.zoneLenghtPublic++;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
