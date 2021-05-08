using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(6, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}