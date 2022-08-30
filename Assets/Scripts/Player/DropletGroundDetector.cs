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
    public float DistanceToNearestGround{
        get => _distanceToNearestGround; 
    }
    public bool IsTouchingGround {
        get =>_isTouchingGround;
    }
    public bool IsTouchingCeiling {
        get => _isTouchingCeiling;
    }


    [SerializeField]
    private int _raysCount = 8;
    [SerializeField]
    private float _maxDistanceToGround = 0.7f;

    private Vector2 _normalOfNearestGround;
    private Vector2 _normalOfNearestCeiling;
    private float _distanceToNearestGround;
    private bool _isTouchingGround = false;
    private bool _isTouchingCeiling = false;
    public LayerMask groundLayerMask;

    
    private CircleCollider2D circleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();
    }

    private void DetectGround(){
        _normalOfNearestGround = new Vector2(0,1).normalized;
        float distance_to_nearest_ground = Mathf.Infinity;
        bool didHit = false;
        bool didHitCeiling = false;
        for (int i = 0; i < _raysCount; i++)
        {
            Color ray_color = Color.green;
            float angle = i * 360/_raysCount;
            Vector2 directionOfRay = Quaternion.AngleAxis(angle, Vector3.back) * Vector2.down;

            if(210 > angle && angle > 90){
                RaycastHit2D raycastHit = Physics2D.Raycast(circleCollider2D.bounds.center, directionOfRay, _maxDistanceToGround, groundLayerMask);
                if(raycastHit.collider != null){
                    didHitCeiling = true;
                    ray_color = Color.yellow;
                    float distance_to_ground = raycastHit.distance;
                    if(distance_to_ground < distance_to_nearest_ground){
                        distance_to_nearest_ground = distance_to_ground;
                        _normalOfNearestCeiling = raycastHit.normal;
                    }
                }
            }
            else{
                RaycastHit2D raycastHit = Physics2D.Raycast(circleCollider2D.bounds.center, directionOfRay, _maxDistanceToGround, groundLayerMask);
                if(raycastHit.collider != null){
                    didHit = true;
                    ray_color = Color.yellow;
                    float distance_to_ground = raycastHit.distance;
                    if(distance_to_ground < distance_to_nearest_ground){
                        distance_to_nearest_ground = distance_to_ground;
                        _normalOfNearestGround = raycastHit.normal;
                    }
                }
            }

            Debug.DrawRay(transform.position, Quaternion.AngleAxis(i * 360/_raysCount,Vector3.back) * Vector2.down * _maxDistanceToGround,ray_color);
        }

        _isTouchingGround = didHit;
        _isTouchingCeiling = didHitCeiling;

        _distanceToNearestGround = distance_to_nearest_ground;
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < _raysCount; i++){
            
        }
        
    }
}