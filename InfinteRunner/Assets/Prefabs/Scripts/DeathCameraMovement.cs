using UnityEngine;

public class DeathCameraMovement : MonoBehaviour
{
    public Transform deathCameraPosition;
    public Transform cameraPositionLeft;
    public Transform cameraPositionMiddle;
    public Transform cameraPositionRight;
    public GameObject deathScreen;
    int position = 1;

    // Update is called once per frame
    void LateUpdate()
    {
        if (ManagerScript.Instance.playerDead)
        {
            deathScreen.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.A) && !ManagerScript.Instance.playerDead && position > 0)
        {
            position--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && !ManagerScript.Instance.playerDead && position < 2)
        {
            position++;
        }

        if (position == 0 && !ManagerScript.Instance.playerDead)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositionLeft.position, 10 * Time.deltaTime);
        }
        else if (position == 1 && !ManagerScript.Instance.playerDead)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositionMiddle.position, 10 * Time.deltaTime);
        }
        else if (position == 2 && !ManagerScript.Instance.playerDead)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositionRight.position, 10 * Time.deltaTime);
        }
    }
}

