using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    [SerializeField] Transform gun;
    Animator animator;
    Vector2 moveInput;
    [SerializeField] GameObject bullet;
    Vector2 direction;
    [SerializeField] float health = 100;
    BoxCollider2D boxCollider2D;
    [SerializeField] GameObject explosion;
    [SerializeField] bool showImage;
    [SerializeField] int ammo = 10;
    [SerializeField] TextMeshProUGUI ammoText;
    LevelManager levelManager;
    AudioSource audioSource;
    [SerializeField] AudioClip takeDameSound;
    [SerializeField] AudioClip fireSound;

    bool isExit;
    [SerializeField] float speed = 3f;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ammoText.text = ammo + "/10";
        boxCollider2D = GetComponent<BoxCollider2D>();
        isExit = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Run();
        IdlePlayer();
    }

    public Vector2 getDirection()
    {
        return direction;
    }

    void OnMove(InputValue inputValue)
    {
        if (!isExit)
        {
            moveInput = inputValue.Get<Vector2>();
            if (boxCollider2D != null)
            {
                if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
                {
                    TakeDamage(5f);
                    animator.SetBool("isHit", true);
                }
                else
                {
                    animator.SetBool("isHit", false);
                }
            }
        }
    }

    public void AddHealth(float value)
    {
        if (health < 100f)
            health += value;
        else if (health > 100f)
            health = 100f;
        HealthBar.instance.setValue(health / 100);
    }

    public void ResetHealth()
    {
        this.health = 100f;
        HealthBar.instance.setValue(health / 100);
    }

    public void ResetAmmo()
    {
        ammo = 10;
        ammoText.text = ammo + "/10";
    }

    public void AddAmmo(int value)
    {
        ammo += value;
        if (ammo > 10)
            ammo = 10;
        ammoText.text = ammo + "/10";
    }

    void TakeDamage(float value)
    {
        PlayAudio(takeDameSound);
        this.health -= value;
        HealthBar.instance.setValue(health / 100);
        if (health <= 0)
        {
            animator.SetBool("isHit", true);
            speed = 0;
            Invoke("Die", 1);
            if (levelManager != null)
            {
                levelManager.LoadGameOver();
            }
        }
    }

    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Run()
    {
        Vector2 playerMove = new Vector2(moveInput.x * speed, moveInput.y * speed);
        transform.rotation = Quaternion.identity;
        rigidbody2D.velocity = playerMove;
    }

    void IdlePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("Move X", moveInput.x);
            animator.SetFloat("Move Y", moveInput.y);
            direction = moveInput;
        }
        animator.SetFloat("MoveVelocity", moveInput.magnitude);
    }

    void OnFire()
    {
        if (ammo > 0)
        {
            ammo--;
            ammoText.text = ammo + "/10";
            Instantiate(bullet, gun.position, Quaternion.identity);
            PlayAudio(fireSound);
        }
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
