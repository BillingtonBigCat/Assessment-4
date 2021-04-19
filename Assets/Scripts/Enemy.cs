using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemy;

    public float enemySpeed;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
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
