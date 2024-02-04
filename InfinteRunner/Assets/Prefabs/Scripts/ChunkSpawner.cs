using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    ManagerScript managerScript;
    int lastChunk = 100;
    public static int zoneLength = 0;
    int randomPickZone = 0;
    int zoneCounter = 0;

    List<string> chunkList = new List<string>()
    {
        "Chunk1",
        "Chunk2",
        "Chunk3",
        "Chunk4",
        "Chunk5",
        "Chunk6",
        "Chunk7",
        "Chunk8",
        "Chunk9",
        "Chunk10",
        "Chunk11",
        "Chunk12",
        "Chunk13",
        "Chunk14",
        "Chunk15",
        "Chunk16"
    };

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        managerScript = ManagerScript.Instance;
        managerScript.spawnTransition = false;
        managerScript.zonePublic = 0;
    }


    void Update()
    {

        if (ColliderScript.spawnChunk == true)
        {   
            //When 4 chunks have been spawned, it rerolls for a new zone, if the same zone has been rolled 2 times in a row, then the 3rd will be another zone
            //100%
            if (zoneLength == 4)
            {
                zoneLength = 0;
                managerScript.zoneLenghtPublic = 0;
                managerScript.lastZone = managerScript.zonePublic;

                if (zoneCounter == 2)
                {
                    zoneCounter = 0;
                    randomPickZone = 0;
                }
                else if (zoneCounter == -2)
                {
                    zoneCounter = 0;
                    randomPickZone = 1;
                }
                else
                {
                    randomPickZone = Random.Range(0, 2);
                }
                
                if (randomPickZone == 1)
                {
                    zoneCounter++;

                    if (managerScript.lastZone == 1)
                    {
                        managerScript.spawnTransition = false;
                        managerScript.zonePublic = 1;
                    }
                    else
                    {
                        managerScript.spawnTransition = true;
                        managerScript.zonePublic = 1;
                    }
                }
                else if (randomPickZone == 0)
                {
                    zoneCounter--;

                    if (managerScript.lastZone == 1)
                    {
                        managerScript.spawnTransition = true;
                        managerScript.zonePublic = 0;
                    }
                    else
                    {
                        managerScript.spawnTransition = false;
                        managerScript.zonePublic = 0;
                    }
                }
            }

            int randomNumber = Random.Range(0, 7);

            if (randomNumber == lastChunk)
            {
                if(lastChunk == 0)
                {
                    randomNumber++;
                }
                else if (lastChunk == 6)
                {
                    randomNumber--;
                }
                else
                {
                    randomNumber = Random.Range(1, 6);
                }
            }

            //if the zone is 0, which is the House, and it doesn't need to spawn a transition chunk, it decides which House chunk it will spawn
            if (randomPickZone == 0 && !managerScript.spawnTransition)
            {
                if (randomNumber == 0)
                {
                    objectPooler.SpawnFromPool(chunkList[0], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 1)
                {
                    objectPooler.SpawnFromPool(chunkList[1], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 2)
                {
                    objectPooler.SpawnFromPool(chunkList[2], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 3)
                {
                    objectPooler.SpawnFromPool(chunkList[3], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 4)
                {
                    objectPooler.SpawnFromPool(chunkList[4], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 5)
                {
                    objectPooler.SpawnFromPool(chunkList[5], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 6)
                {
                    objectPooler.SpawnFromPool(chunkList[6], transform.position, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
            }
            //if the zone is 1, which is the Basement, and it doesn't need to spawn a transition chunk, it decides which Basement chunk it will spawn
            else if (randomPickZone == 1 && !managerScript.spawnTransition)
            {
                if (randomNumber == 0)
                {
                    objectPooler.SpawnFromPool(chunkList[7], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 1)
                {
                    objectPooler.SpawnFromPool(chunkList[8], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 2)
                {
                    objectPooler.SpawnFromPool(chunkList[9], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 3)
                {
                    objectPooler.SpawnFromPool(chunkList[10], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 4)
                {
                    objectPooler.SpawnFromPool(chunkList[11], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 5)
                {
                    objectPooler.SpawnFromPool(chunkList[12], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
                else if (randomNumber == 6)
                {
                    objectPooler.SpawnFromPool(chunkList[13], transform.position + Vector3.down * 7.739323f, Quaternion.identity);
                    ColliderScript.spawnChunk = false;
                }
            }

            if (managerScript.spawnTransition && managerScript.lastZone == 0)
            {
                objectPooler.SpawnFromPool(chunkList[14], transform.position, Quaternion.identity * Quaternion.Euler(15, 0, 0));
                ColliderScript.spawnChunk = false;
                managerScript.spawnTransition = false;
            }
            else if (managerScript.spawnTransition && managerScript.lastZone == 1)
            {
                objectPooler.SpawnFromPool(chunkList[15], transform.position, Quaternion.identity * Quaternion.Euler(15, 180, 0));
                ColliderScript.spawnChunk = false;
                managerScript.spawnTransition = false;
            }

            lastChunk = randomNumber;
        }
    }
}