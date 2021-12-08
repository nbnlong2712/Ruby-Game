using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Vector3 offset;

   public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(true);
        slider.value = health;
        slider.maxValue = maxHealth;
    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);    
    }
}
