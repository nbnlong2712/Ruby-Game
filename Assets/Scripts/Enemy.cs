using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    Player player;
    Animator animator;
    Vector2 direction;
    [SerializeField] float health = 100;
    CapsuleCollider2D capsuleCollider2D;
    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 1.5f;
    ParticleSystem smokeEffect;
    [SerializeField] HealthEnemy healthEnemy;
    [SerializeField] float stopDieTime = 1.2f;

    void Start()
    {
        smokeEffect = GetComponentInChildren<ParticleSystem>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DirectionEnemy();
        healthEnemy.SetHealth(health, 100);
    }
    
    void DirectionEnemy()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if  ((Mathf.Abs(player.transform.position.y - transform.position.y) > 3)&&(player.transform.position.y > transform.position.y))
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", -1);
            }
            else if ((Mathf.Abs(player.transform.position.y - transform.position.y) > 3) && (player.transform.position.y < transform.position.y))
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", 1);
            }
            else if ((Mathf.Abs(player.transform.position.y - transform.position.y) < 3) && (player.transform.position.x > transform.position.x))
            {
                animator.SetFloat("Move X", 1);
                animator.SetFloat("Move Y", 0);
            }
            else if ((Mathf.Abs(player.transform.position.y - transform.position.y) < 3) && (player.transform.position.x < transform.position.x))
            {
                animator.SetFloat("Move X", -1);
                animator.SetFloat("Move Y", 0);
            }
        }
    }

    public void TakeDamage(float value)
    {
        healthEnemy.SetHealth(health, 100);
        health -= value;
        if(health <= 0)
        {
            healthEnemy.SetHealth(0, 100);
            Die();
        }
    }

    void Die()
    {
        speed = 0;
        animator.SetBool("isFix", true);
        if(smokeEffect != null)
            smokeEffect.Stop();
        Invoke("DestroyEnemy", stopDieTime);
    }

    void DestroyEnemy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}