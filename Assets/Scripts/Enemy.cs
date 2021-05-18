using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int level;
    private bool Level2;

    Object bulletRef;
    Object bulletRef2;
    private float fireRate = .3f; // Bullets/second
    private float timeToNextShot; // How much longer we have to wait.

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        level = SceneManager.GetActiveScene().buildIndex;
        bulletRef = Resources.Load("firebullet");
        bulletRef2 = Resources.Load("firebullet2");


    }

    void Update()
    {
        Move();
        Shoot();
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
        Vector3 enemyScale = transform.localScale;

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            enemySpeed *= -1;

            enemyScale.x *= -1;
            transform.localScale = enemyScale;
            //Destroy(gameObject);
        }

    }

    void Shoot()
    {
        if (level == 3)
        {
            timeToNextShot -= Time.deltaTime;
            if (timeToNextShot <=0)
            {
                timeToNextShot = 1 / fireRate;
                //DeweyAnimator.SetBool("IsShooting", true);
                GameObject bullet = (GameObject)Instantiate(bulletRef);
                bullet.transform.position = new Vector3(transform.position.x + .4f, transform.position.y - .2f, -1);

            }
            //else
            //{
            //DeweyAnimator.SetBool("IsShooting", false);
            //}


        }
        if (level == 5)
        {
            timeToNextShot -= Time.deltaTime;
            if (timeToNextShot <= 0)
            {
                timeToNextShot = 1 / fireRate;
                //DeweyAnimator.SetBool("IsShooting", true);
                GameObject bullet2 = (GameObject)Instantiate(bulletRef2);
                if (enemy.transform.position.y > 0 && enemy.transform.position.x < 35) {
                    bullet2.transform.position = new Vector3(transform.position.x + .4f, transform.position.y - .2f, 1);
                }
                if (enemy.transform.position.y > 0 && enemy.transform.position.x > 35)
                {
                    bullet2.transform.position = new Vector3(transform.position.x + .4f, transform.position.y - .2f, -1);
                }

            }
            //else
            //{
            //DeweyAnimator.SetBool("IsShooting", false);
            //}


        }
    }
}
