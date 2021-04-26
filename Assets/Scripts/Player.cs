using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float iceMultiplier = 500f;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    public int defaultAdditionalJumps = 1;
    int additionalJumps;

<<<<<<< Updated upstream
=======
    public GameObject RespawnPoint;

    public Sprite changeState;
    //bool isChanged = false;

    public Animator DeweyAnimator;
    private string state;


    void Awake()
    {
        gameObject.transform.position = RespawnPoint.transform.position;
    }

>>>>>>> Stashed changes
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        additionalJumps = defaultAdditionalJumps;
<<<<<<< Updated upstream
=======
        state = "Normal";
        //ChangeState();
>>>>>>> Stashed changes
    }

    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
<<<<<<< Updated upstream
=======
        Ice();
        //ChangeState();
>>>>>>> Stashed changes
    }


    void Move()
    {
<<<<<<< Updated upstream
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
=======
        if (state == "Normal")
        {
            float x = Input.GetAxisRaw("Horizontal");
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
>>>>>>> Stashed changes
    }

    void Jump()
    {
        if (state == "Normal")
        {
<<<<<<< Updated upstream
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }

    void BetterJump()
=======
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
    }

    void Ice()
    {
        if (Input.GetKey(KeyCode.S)){
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

    // Check if something is below the player
    void CheckIfGrounded()
>>>>>>> Stashed changes
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
<<<<<<< Updated upstream
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
=======
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        if (other.gameObject.tag == "OutOfBounds")
        {
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        // Loads next scene when you pass the level 
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.gameObject.tag == "Breakable" && state == "Ice")
        {
            Destroy(other.gameObject);
>>>>>>> Stashed changes
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

}