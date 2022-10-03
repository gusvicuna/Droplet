using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bloom(bool isDropletContaminated){
        
        GetComponent<Collider2D>().enabled = false;
        if(isDropletContaminated){
            animator.SetTrigger("Wither");
        }
        else{
            animator.SetTrigger("Bloom");
        }
    }
}
