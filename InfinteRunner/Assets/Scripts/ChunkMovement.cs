using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    public static float speedOfChunk = 10f;

    void FixedUpdate()
    {
        ChunkMove();
    }

    public void ChunkMove()
    {
        transform.position += (Vector3.back * Time.deltaTime) * speedOfChunk;

        if (ManagerScript.Instance.goDown == 1)
        {
            transform.position += (Vector3.up * Time.deltaTime * 0.27f) * speedOfChunk;
        }
        else if (ManagerScript.Instance.goDown == -1)
        {
            transform.position += (Vector3.down * Time.deltaTime * 0.27f) * speedOfChunk;
        }

        if (ManagerScript.Instance.playerDead)
        {
            GetComponent<ChunkMovement>().enabled = false;
        }
    }
}
