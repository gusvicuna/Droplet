using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action HealthIsZero;
    public event Action DropletIsImmune;
    public event Action DropletIsNotImmune;
    
    public delegate void HealthChanged(int amount);
    public HealthChanged healthIncremented;
    public HealthChanged healthDecremented;

    [SerializeField]
    private int maxHealth = 25;
    [SerializeField]
    private int minHealth = 0;
    private int currentHealth = 25;

    private bool isImmune = false;
    [SerializeField]
    private const int immuneCooldown = 3;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MinHealth => minHealth;
    public int MaxHealth => maxHealth;

    public void Decrement(int amount) {
        if(isImmune) return;

        StartCoroutine(ImmuneCooldown());
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        healthDecremented.Invoke(amount);
    }

    public void Increment(int amount) {
        currentHealth = Math.Min(maxHealth, currentHealth + amount);
        healthIncremented.Invoke(amount);
    }

    public void Die(){
        HealthIsZero?.Invoke();
    }

    private IEnumerator ImmuneCooldown(){
        isImmune = true;
        DropletIsImmune.Invoke();
        yield return new WaitForSeconds(immuneCooldown);
        isImmune = false;
        DropletIsNotImmune.Invoke();
    }

}
