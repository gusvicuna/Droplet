using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnviromentInput : MonoBehaviour
{
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Floor")
        {
            playerController.isOnFloor = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.transform.tag == "Floor")
        {
            playerController.isOnFloor = false;
        }
    }


}
