using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DropletHealth : MonoBehaviour
{
    public int maxHealth = 25;
    public int currentHealth = 25;


    // Events
    public UnityEvent OnNoHealth;




    // Start is called before the first frame update
    void Start()
    {
        OnNoHealth = new UnityEvent();

        currentHealth = maxHealth;

    }

    public void LoseHealth(int health){

        currentHealth-=health;
        if (currentHealth > 0) return;
        currentHealth = 0;
        OnNoHealth.Invoke();
    }

    public void GainHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
