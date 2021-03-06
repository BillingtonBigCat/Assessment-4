using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D player;

    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;

    public float speed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    bool isGrounded = false;
    public LayerMask groundLayer;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    public int defaultAdditionalJumps = 1;
    int additionalJumps;

    public GameObject RespawnPoint;

   
    //bool isChanged = false;

    public Animator DeweyAnimator;
    private string state = "Normal";
    private int level;

    private bool Level2;
    private bool Level3;
    private bool Level4;
    private bool Level5;

    Object bulletRef;


    void Awake()
    {
        gameObject.transform.position = RespawnPoint.transform.position;
    }

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
        additionalJumps = defaultAdditionalJumps;
        bulletRef = Resources.Load("Bullet");
        level = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
        Ice();
        Shoot();
        Mist();
    }



    // Control Movement of Player
    void Move()
    {
        if (state == "Normal")
        {
            Vector3 playerScale = transform.localScale;
            float x = Input.GetAxisRaw("Horizontal");
            if (Input.GetKey(KeyCode.D))
            {
                playerSprite.flipX = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerSprite.flipX = true;
            }
            float moveBy = x * speed;
            player.velocity = new Vector2(moveBy, player.velocity.y);

            if (x > 0.0f)
            {
                DeweyAnimator.SetBool("Moving", true);
                DeweyAnimator.SetBool("Idle", false);
            }
            else
            {
                DeweyAnimator.SetBool("Idle", true);
                DeweyAnimator.SetBool("Moving", false);
            }
        }

        if (state == "Ice")
        {
            player.velocity = new Vector2(0, -15.0f);
        }

        if (state == "Mist")
        {
            player.velocity = new Vector2(0, 0);
        }
    }

    // Control Player Jumps
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            additionalJumps--;
        }
        if (player.velocity.y < 0)
        {
            player.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (player.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            player.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Ice()
    {
        if (level > 1)
        {
            if (state != "Mist")
                if (Input.GetKey(KeyCode.S))
                {
                    DeweyAnimator.SetBool("Ice", true);
                    DeweyAnimator.SetBool("Idle", false);
                    DeweyAnimator.SetBool("Moving", false);
                    state = "Ice";

                }
                else
                {
                    DeweyAnimator.SetBool("Ice", false);
                    state = "Normal";
                }
            }
        }
    

    void Shoot()
    {
        if (level > 2)
        {
             if (Input.GetMouseButtonDown(0))
            {
            DeweyAnimator.SetBool("IsShooting", true);
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.transform.position = new Vector3(transform.position.x + .4f, transform.position.y + .1f, -1);

            }
             else
            {
            DeweyAnimator.SetBool("IsShooting", false);
            }
        }
    }

    void Mist()
    {
        if (level > 3)
        {
            if (state != "Ice")
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    DeweyAnimator.SetBool("Mist", true);
                    DeweyAnimator.SetBool("Idle", false);
                    DeweyAnimator.SetBool("Moving", false);
                    DeweyAnimator.SetBool("Ice", false);
                    state = "Mist";
                    if (playerCollider.enabled == true)
                    {
                        playerCollider.enabled = !playerCollider.enabled;
                    }
                    player.gravityScale = 0;
                }
                else
                {
                    DeweyAnimator.SetBool("Mist", false);
                    state = "Normal";
                    if (playerCollider.enabled == false)
                    {
                        playerCollider.enabled = !playerCollider.enabled;
                    };
                    player.gravityScale = 1;
                }
            }
        }
    }


    // Check if something is below the player
    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            additionalJumps = defaultAdditionalJumps;
        }
    }

    // Handles Respawning when Enemy is hit or fell off platform
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "DangerousTerrain") && state != "Mist" )
        {
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        if (other.gameObject.tag == "OutOfBounds")
        {
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        // Loads next scene when you pass the level 
        if (other.gameObject.tag == "Goal")
        {
            if (level < 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
                SceneManager.LoadScene(0);
        }

        if (other.gameObject.tag == "Breakable" && state == "Ice")
        {
            Destroy(other.gameObject);
        }
    }

    // Setting the Respawn Point to the Checkpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            RespawnPoint = other.gameObject;
            
        }

        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            //health--;
            //sr.material = matWhite;
            //if (health <= 0)
            //{
                gameObject.transform.position = RespawnPoint.transform.position;
            //}
            //else
            //{
            //    Invoke("ResetMaterial", .1f);
            //}
        }

    }
    
}