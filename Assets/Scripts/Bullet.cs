using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    Player player;
    Vector2 direction;
    [SerializeField] GameObject explosion;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
        direction = player.getDirection();
    }

    void Update()
    {
        Fire(direction);
    }

    void Fire(Vector2 direction)
    {
        rigidbody2D.AddForce(direction * 20f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(40f);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
