using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int barrierMaxHealth = 100;
    public int damageTaken = 0;

    float healthPercentage = 1;
    bool barrierIsDestroyed = false;
    SpriteRenderer[] sprites;

    private void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        damageTaken++;

        if (damageTaken >= barrierMaxHealth)
        {
            DestroyBarrier();
        }

        CalculateHealthPercentage();
    }

    private void DestroyBarrier()
    {
        barrierIsDestroyed = true;
    }

    public bool IsDestroyed()
    {
        return barrierIsDestroyed;
    }

    private void CalculateHealthPercentage()
    {
        healthPercentage = (float)(barrierMaxHealth - damageTaken) / barrierMaxHealth;
        if (healthPercentage < 0) healthPercentage = 0;
        if (healthPercentage > 1) healthPercentage = 1;
    }

    public float GetHealthPercentage()
    {
        return healthPercentage;
    }
}
