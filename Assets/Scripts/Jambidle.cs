using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambidle : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    void Start()
    {
        if (dialog != null)
        {
            dialog.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialog.SetActive(true);
        }
    }
}
