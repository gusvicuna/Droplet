using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletGroundDetector : MonoBehaviour
{
    public Vector2 NormalOfNearestGround{
        get => _normalOfNearestGround; 
    }
    public Vector2 NormalOfNearestCeiling{
        get => _normalOfNearestCeiling; 
    }
    public bool IsTouchingGround {
        get =>_isTouchingGround;
    }
    public bool IsTouchingCeiling {
        get => _isTouchingCeiling;
    }

    private Vector2 _normalOfNearestGround;
    private Vector2 _normalOfNearestCeiling;
    private bool _isTouchingGround = false;
    private bool _isTouchingCeiling = false;

    private void OnCollisionStay2D(Collision2D other) {
        switch (other.gameObject.tag){ 
            case "Floor":
                _isTouchingGround = true;
                _normalOfNearestGround = other.contacts[0].normal;
                break;
            case "Ceiling":
                _isTouchingCeiling = true;
                _normalOfNearestCeiling = other.contacts[0].normal;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        switch (other.gameObject.tag){
            case "Floor":
                _isTouchingGround = false;
                _normalOfNearestGround = Vector2.up;
                break;
            case "Ceiling":
                _isTouchingCeiling = false;
                _normalOfNearestCeiling = Vector2.up;
                break;
        }
    }
}