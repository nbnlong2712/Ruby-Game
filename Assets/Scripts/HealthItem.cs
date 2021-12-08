using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    Player player;
    [SerializeField] AudioClip collectSound;
    void Start()
    {
        player = FindObjectOfType<Player>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(player != null)
            {
                player.PlayAudio(collectSound);
                player.AddHealth(10);
                Destroy(gameObject);
            }
        }
    }
}
