using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    Rigidbody2D rock;

    public float enemySpeed;
    private float originalPosition;

    void Start()
    {
        rock = GetComponent<Rigidbody2D>();
        originalPosition = gameObject.transform.position.y;
    }

    void Update()
    {
        Move();
    }


    void Move ()
    {
        rock.velocity = new Vector2(rock.velocity.x, -enemySpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "StandardTerrain")
        {
            enemySpeed *= -1;
        }

    }

}