using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletEnviromentInput : MonoBehaviour
{
    public DropletController playerController;

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
