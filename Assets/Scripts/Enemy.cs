using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemy;

    public float enemySpeed;

    private int health = 3;

    //Materials
    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    SpriteRenderer sr;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");

    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            sr.material = matWhite;
            if(health <= 0)
            {
                KillSelf();
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }    

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }

    void Move()
    {
        enemy.velocity = new Vector2(enemySpeed, enemy.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.tag == "Wall")
        {
            enemySpeed *= -1;
        }

    }
}
