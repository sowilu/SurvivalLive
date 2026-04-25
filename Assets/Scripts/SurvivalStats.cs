using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger")] 
    public float maxHunger = 100;
    public float hunger;
    public float hungerDrainRate = 1;
    public Transform hungerVisual;
    
    [Header("Thirst")]
    public float maxThirst = 100;
    public float thirst;
    public float thirstDrainRate = 1.5f;
    public Transform thirstVisual;
    
    [Header("Health")]
    public float maxHealth = 100;
    public float health;
    public float damageWhenStarving = 5;
    public float damageWhenDehydrated = 8;
    public Transform healthVisual;

    void Start()
    {
        hunger = maxHunger;
        thirst = maxThirst;
        health = maxHealth;
    }
    
    void Update()
    {
        hunger -= Time.deltaTime * hungerDrainRate;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        
        thirst -= Time.deltaTime * thirstDrainRate;
        thirst = Mathf.Clamp(thirst, 0, maxThirst);

        if (thirst <= 0)
        {
            health -= damageWhenDehydrated * Time.deltaTime;
        }

        if (hunger <= 0)
        {
            health -= damageWhenStarving * Time.deltaTime;
        }
        
        health = Mathf.Clamp(health, 0, maxHealth);
        
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        hungerVisual.localScale = new Vector3(hunger/maxHunger, 1, 1);
        thirstVisual.localScale = new Vector3(thirst/maxThirst, 1, 1);
        healthVisual.localScale = new Vector3(health/maxHealth, 1, 1);
    }

    public void Eat(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        UpdateVisuals();
    }

    public void Drink(float amount)
    {
        thirst += amount;
        thirst = Mathf.Clamp(thirst, 0, maxThirst);
        UpdateVisuals();
    }
}
