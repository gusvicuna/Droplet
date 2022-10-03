using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    private DropletGroundDetector groundDetector;
    private DropletGFX gfx;

    private DropletFollow dropletFollow;
    // Start is called before the first frame update
    void Start()
    {
        groundDetector = GetComponent<DropletGroundDetector>();
        gfx = GetComponent<DropletGFX>();
        dropletFollow = GetComponent<DropletFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundDetection();
    }

    private void GroundDetection()
    {
        gfx.RotateDroplet(groundDetector.NormalOfNearestGround * -1);
        gfx.SetIsOnGround(groundDetector.IsTouchingGround || groundDetector.IsTouchingCeiling);
    }



}
