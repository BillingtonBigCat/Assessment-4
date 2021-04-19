using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D player;

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

    void Awake()
    {
        gameObject.transform.position = RespawnPoint.transform.position;
    }

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        additionalJumps = defaultAdditionalJumps;
    }

    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        player.velocity = new Vector2(moveBy, player.velocity.y);
    }

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




    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            additionalJumps = defaultAdditionalJumps;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        if (other.gameObject.tag == "OutOfBounds")
        {
            gameObject.transform.position = RespawnPoint.transform.position;
        }

        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            RespawnPoint = other.gameObject;
            
        }
    }
}