using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public static bool spawnChunk = false;

    //This trigger checks if the oldest chunk in the scene has collided with it and disables it
    //and adds score and increases the speed of the chunks
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            spawnChunk = true;
            ScoreManager.instance.score++;

            other.gameObject.SetActive(false);

            if (ChunkMovement.speedOfChunk < 12f)
            {
                ChunkMovement.speedOfChunk += 0.2f;
            }
            else if (ChunkMovement.speedOfChunk < 15f)
            {
                ChunkMovement.speedOfChunk += 0.1f;
            }
            else
            {
                //do not increase;
            }
        }
    }
}