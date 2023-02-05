using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D plrRB;
    bool isGrounded = false, canFly, onPlatform;
    [SerializeField] float moveSpeed, moveSpeedPlatform, jumpForce, flySpeed, platformSwitchDelay, reSpawnDelay, delayForRocks;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkGroundRadius;
    [SerializeField] Transform isGroundedChecker;
    [SerializeField] GameObject jumpTwo, jumpOne, jumpZero;
    [SerializeField] GameObject victoryCanvas;

    [SerializeField] Animator plrAnimator;

    SfxManager sfx;
    GameManager gameManager;
    ObjectSpawner spawner;

    //for re-spawn
    public static Vector3 currentcheckPoint = Vector3.zero;

    private void Awake()
    {
        plrRB = GetComponent<Rigidbody2D>();
        currentcheckPoint = this.transform.position;
        canFly = false;
        onPlatform = false;
        plrAnimator = GetComponentInChildren<Animator>();
        sfx = FindObjectOfType<SfxManager>();
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<ObjectSpawner>();
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

        if(this.transform.position.y <= -25)
        {
            StartCoroutine(ReSpawnPlr(reSpawnDelay));
        }
    }

    private void Move()
    {
        if(isGrounded && onPlatform == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * moveSpeed;
            plrRB.velocity = new Vector2(moveBy, plrRB.velocity.y);
            
           
        }
        else if (isGrounded && onPlatform)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * moveSpeedPlatform;
            plrRB.velocity = new Vector2(moveBy, plrRB.velocity.y);
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * (moveSpeed / 2);
            plrRB.velocity = new Vector2(moveBy, plrRB.velocity.y);
            plrAnimator.SetBool("IsRunning", false);

        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            plrAnimator.SetBool("IsRunning", true);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            plrAnimator.SetBool("IsRunning", true);
        }
        

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            plrAnimator.SetTrigger("IsJumping");
            sfx.jumpAudio();
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
            jumpTwo.SetActive(false);
        }
        else if (jumpOne.activeInHierarchy == true)
        {
            jumpZero.SetActive(false);
            jumpOne.SetActive(false);
            jumpTwo.SetActive(true);
        }
        else
        {
            jumpZero.SetActive(true);
            jumpOne.SetActive(false);
            jumpTwo.SetActive(false);
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
            sfx.hitAudio();
            gameManager.plrLives--;
            StartCoroutine(ReSpawnPlr(reSpawnDelay));
        }
        if(collision.collider.tag == "MovingPlatform")
        {
            var emptyObject = new GameObject();
            emptyObject.transform.parent = collision.gameObject.transform;
            this.transform.parent = emptyObject.transform;

            onPlatform = true;
            
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "MovingPlatform")
        {
            this.transform.parent = null;
            onPlatform = false;
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
            sfx.hitAudio();
            gameManager.plrLives--;
            StartCoroutine(ReSpawnPlr(reSpawnDelay));
        }
        if (collision.CompareTag("EndLine"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Time.timeScale = 0;
            victoryCanvas.SetActive(true);
        }
        if (collision.CompareTag("Health"))
        {
            if(gameManager.plrLives < gameManager.maxHealth)
            {
                gameManager.plrLives++;
                gameManager.ChangeRootImage();
            }
            
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Upstream"))
        {
            canFly = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Upstream"))
        {
            canFly = false;
        }
    }

    IEnumerator ReSpawnPlr(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameManager.ChangeRootImage();
        this.transform.position = currentcheckPoint;
        spawner.SpawnHealthObject();
    }

    IEnumerator DropRocks(float delay)
    {
        yield return new WaitForSeconds(delay);

        var spawner = FindObjectOfType<ObjectSpawner>();
        spawner.SpawnObjects();
    }
}
