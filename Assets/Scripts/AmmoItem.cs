using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
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
                player.AddAmmo(2);
                Destroy(gameObject);
            }
        }
    }
}
