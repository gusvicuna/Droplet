using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int maxHealth = 25;
    public int currentHealth = 0;


    // Events
    public UnityEvent OnNoHealth;




    // Start is called before the first frame update
    void Start()
    {
        OnNoHealth = new UnityEvent();

        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoseHealth(int health){

        currentHealth-=health;
        if(currentHealth <= 0){
            currentHealth = 0;
            OnNoHealth.Invoke();
        }
    }

}
