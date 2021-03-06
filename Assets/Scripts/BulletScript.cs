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
        rb2d.velocity = new Vector2(8, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Bullet named: " + name + ", is colliding with other: " + collision.name);
        if (collision.gameObject.tag == "Ground")
            Destroy(gameObject);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
