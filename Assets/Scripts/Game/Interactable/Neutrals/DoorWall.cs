using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWall : MonoBehaviour, ISwitchable
{
    private bool isActive;

    private Animator animator;

    [SerializeField]
    private PressurePlate pressurePlate;
    public bool IsActive => isActive;
    
    // Start is called before the first frame update
    private void Awake() {
        animator = GetComponent<Animator>();
        pressurePlate.client = this;
    }

    public void Activate(){
        animator.SetTrigger("Activate");
    }

    public void Deactivate(){
        animator.SetTrigger("Deactivate");
    }


}
