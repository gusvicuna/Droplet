using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletGFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotateDroplet(Vector2 direction){
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.Find("Graphics").rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
