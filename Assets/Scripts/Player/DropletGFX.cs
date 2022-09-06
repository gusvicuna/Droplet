using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletGFX : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotateDroplet(Vector2 direction){
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.Find("Graphics").rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public void Jump(){
        _animator.SetTrigger("jump");
    }

    public void Hover(){
        _animator.SetTrigger("hover");
    }

    public void Fall(){
        _animator.SetTrigger("fall");
    }

    public void SetIsOnGround(bool isOnGround){
        _animator.SetBool("isOnGround", isOnGround);
    }

    public void Move(float moveInput){
        if(moveInput<0){
            _animator.SetBool("isMoving", true);
            _spriteRenderer.flipX = true;
        }
        else if(moveInput>0){
            _animator.SetBool("isMoving", true);
            _spriteRenderer.flipX = false;
        }
        else{
            _animator.SetBool("isMoving", false);
        }
    }
}
