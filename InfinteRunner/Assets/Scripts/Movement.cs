using System.Collections;
using UnityEngine;
using DG.Tweening;


public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerBody;
    Vector3 targetPosition;
    Vector3 targetPositionDown;
    public Quaternion targetRotation;
    public AudioClip ticketSound;
    public AudioClip oilSound;

    private IEnumerator jumpingCoroutine;
    private IEnumerator movementCoroutine;
    private IEnumerator slindingCoroutine;
    
    bool canJump = false;

    int position = 1;
    int jumpHeight = 2;
    public float smoothnessSpeed;

    public Transform leftLane;
    public Transform middleLane;
    public Transform rightLane;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = middleLane.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            //Adds score & removes tickets on collision
            other.gameObject.SetActive(false);
            ManagerScript.Instance.playerScore++;
            ScoreManager.instance.score++;
            ScoreManager.instance.ticket++;
            AudioSource.PlayClipAtPoint(ticketSound, transform.position);
        }
        else if (other.gameObject.layer == 8)
        {
            //Increases oil
            other.gameObject.SetActive(false);
            ManagerScript.Instance.slider.value += 0.5f;
            AudioSource.PlayClipAtPoint(oilSound, transform.position);
        }
        else if (other.gameObject.layer == 10)
        {
            //variable for going down increases to either 1 if it was 0 before, or 0 if it was -1 before
            ManagerScript.Instance.goDown++;
            //StopAllCoroutines();
        }
        else if (other.gameObject.layer == 11)
        {
            //variable for going down decreases to either -1 if it was 0 before, or 0 if it was 1 before
            ManagerScript.Instance.goDown--;
            //StopAllCoroutines();
        }
        else
        {
            GetComponent<Movement>().enabled = false;
            ManagerScript.Instance.playerDead = true;
        }

        //rotation for the player when it goes up or down the transition slope/chunk
        if (ManagerScript.Instance.goDown > 0)
        {
            transform.rotation = Quaternion.Euler(15, 0, 0);
            canJump = false;
        }
        else if (ManagerScript.Instance.goDown < 0)
        {
            transform.rotation = Quaternion.Euler(-15, 0, 0);
            canJump = false;
        }
        else if (ManagerScript.Instance.goDown == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            canJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Vector3.Distance(targetPosition, transform.position) < 0.2f) && transform.position.y < 2f && ManagerScript.Instance.goDown == 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            StopAllCoroutines();

            if (Input.GetKeyDown(KeyCode.A) && position > 0)
            {
                position--;
            }
            else if (Input.GetKeyDown(KeyCode.D) && position < 2)
            {
                position++;
            }

            movementCoroutine = MovementCouroutine();
            StartCoroutine(movementCoroutine);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            transform.position = targetPosition;

            targetPosition = transform.position;
            targetPositionDown = transform.position;
            
            targetPosition += Vector3.up * jumpHeight;

            StopAllCoroutines();

            jumpingCoroutine = JumpingCouroutine();
            StartCoroutine(jumpingCoroutine);
        }
        //-17 and +17 has to do with the transition chunks if the player slides just before it creats a bug, with 17+- as bounds, we fix the bug
        if (Input.GetKeyDown(KeyCode.S) && playerBody.transform.rotation.x > -17f && playerBody.transform.rotation.x < 17f && canJump)
        {
            targetRotation = Quaternion.Euler(-70,180,0);
            StopAllCoroutines();
            slindingCoroutine = SlidingCoroutine();
            StartCoroutine(slindingCoroutine);
        }
    }
    
    IEnumerator MovementCouroutine()
    {
        if (position == 0)
        {
            targetPosition = leftLane.transform.position;
        }
        else if (position == 1)
        {
            targetPosition = middleLane.transform.position;
        }
        else if (position == 2)
        {
            targetPosition = rightLane.transform.position;
        }

        while (transform.position != targetPosition)
        {
            targetRotation = Quaternion.Euler(0, 180, 0);
            playerBody.transform.DORotateQuaternion(targetRotation, 10 * Time.fixedDeltaTime).SetEase(Ease.Linear);
            controller.radius = 0.5f;
            controller.height = 1f;

            transform.DOMove(targetPosition, 5f * Time.fixedDeltaTime).SetEase(Ease.Linear);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator JumpingCouroutine()
    {
        bool reachedTop = false;
        while (true)
        {
            if (!reachedTop)
            {
                if (Vector3.Distance(targetPosition, transform.position) > 0.2f)
                {
                    targetRotation = Quaternion.Euler(0, 180, 0);
                    playerBody.transform.DORotateQuaternion(targetRotation, 10 * Time.fixedDeltaTime).SetEase(Ease.Linear);
                    controller.radius = 0.5f;
                    controller.height = 1f;

                    transform.DOMove(targetPosition, 10f * Time.fixedDeltaTime).SetEase(Ease.Linear);
                    yield return null;
                }
                else
                {
                    reachedTop = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.25f);

                if (Vector3.Distance(transform.position, targetPositionDown) > 0.05f)
                {
                    transform.DOMove(targetPositionDown, 7f * Time.fixedDeltaTime).SetEase(Ease.Linear);
                    yield return null;
                }
                else
                {
                    break;
                }
            }
        }
        //resetting for next time
        transform.position = targetPositionDown;
        targetPosition = transform.position;
        yield return null;
    }

    IEnumerator SlidingCoroutine()
    {
        Debug.Log("entered sliding");
        bool reachedSlide = false;
        while (true)
        {
            if (!reachedSlide)
            {
                if (playerBody.transform.rotation != Quaternion.Euler(-70, 180, 0))
                {
                    playerBody.transform.DORotateQuaternion(targetRotation, 10 * Time.fixedDeltaTime).SetEase(Ease.Linear);
                    controller.radius = 0.1f;
                    controller.height = 0.1f;
                    yield return null;
                }
                else
                {
                    reachedSlide = true;
                }
            }
            else
            {
                if (playerBody.transform.rotation != Quaternion.Euler(0, 180, 0))
                {
                    targetRotation = Quaternion.Euler(0, 180, 0);
                    playerBody.transform.DORotateQuaternion(targetRotation, 10 * Time.fixedDeltaTime).SetEase(Ease.Linear);
                    controller.radius = 0.5f;
                    controller.height = 1f;
                    yield return null;
                }
                else
                {
                    break;
                }
            }
        }
    }
}