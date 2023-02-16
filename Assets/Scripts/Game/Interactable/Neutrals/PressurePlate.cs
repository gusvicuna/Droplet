using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public ISwitchable client;
    [SerializeField]
    private int massToActivate = 10;
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Toggle(int currentMass){
        if(!client.IsActive && currentMass>= massToActivate){
            animator.SetTrigger("Activate");
            client.Activate();
        }
    }
}
