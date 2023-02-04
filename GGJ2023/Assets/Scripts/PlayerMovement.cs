using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D plrRB;
    bool isGrounded = false, canFly;
    [SerializeField] float moveSpeed, jumpForce, flySpeed, platformSwitchDelay, reSpawnDelay, delayForRocks;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkGroundRadius;
    [SerializeField] Transform isGroundedChecker;
    [SerializeField] GameObject jumpOne, jumpZero, victoryCanvas;

    //for re-spawn
    public static Vector3 currentcheckPoint = Vector3.zero;

    private void Awake()
    {
        plrRB = GetComponent<Rigidbody2D>();
        currentcheckPoint = this.transform.position;
        canFly = false;
    }

    private void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();

        if(canFly)
        {
            plrRB.AddForce(Vector2.up * flySpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if(isGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * moveSpeed;
            plrRB.velocity = new Vector2(moveBy, plrRB.velocity.y);
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * (moveSpeed / 4);
            plrRB.velocity = new Vector2(moveBy, plrRB.velocity.y);
        }
        
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            plrRB.velocity = new Vector2(plrRB.velocity.x, jumpForce);

            StartCoroutine(ChangePlatform(platformSwitchDelay));

            StartCoroutine(DropRocks(delayForRocks));
        }
        
        CheckIfGrounded();
    }

    IEnumerator ChangePlatform(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (jumpZero.activeInHierarchy == true)
        {
            jumpZero.SetActive(false);
            jumpOne.SetActive(true);
        }
        else
        {
            jumpZero.SetActive(true);
            jumpOne.SetActive(false);
        }
    }

    private void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if(colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.collider.tag == "Obstacle")
        {
            StartCoroutine(ReSpawnPlr(reSpawnDelay));
        }
        if(collision.collider.tag == "MovingPlatform")
        {
            var emptyObject = new GameObject();
            emptyObject.transform.parent = collision.gameObject.transform;
            this.transform.parent = emptyObject.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentcheckPoint = collision.transform.position;
            //currentcheckPoint = currentcheckPoint + new Vector3(0, 2, 0);
        }
        if (collision.CompareTag("Obstacle"))
        {
            StartCoroutine(ReSpawnPlr(reSpawnDelay));
        }
        if (collision.CompareTag("EndLine"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Time.timeScale = 0;
            victoryCanvas.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Upstream"))
        {
            canFly = true;
        }
    }

    IEnumerator ReSpawnPlr(float delay)
    {
        yield return new WaitForSeconds(delay);

        this.transform.position = currentcheckPoint;
    }

    IEnumerator DropRocks(float delay)
    {
        yield return new WaitForSeconds(delay);

        var spawner = FindObjectOfType<ObjectSpawner>();
        spawner.SpawnObjects();
    }
}
