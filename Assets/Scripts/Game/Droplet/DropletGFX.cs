using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletGFX : MonoBehaviour
{
    private Animator _bodyAnimator;
    private SpriteRenderer _bodySpriteRenderer;
    [SerializeField]
    private Animator _vaporAnimator;
    [SerializeField]
    private Animator _liquidAnimator;
    [SerializeField]
    private Animator _solidAnimator;
    [SerializeField]
    private SpriteRenderer _vaporSpriteRenderer;
    [SerializeField]
    private SpriteRenderer _liquidSrpiteRenderer;
    [SerializeField]
    private SpriteRenderer _solidSpriteRenderer;
    [SerializeField]
    private Animator _eyesAnimator;
    [SerializeField]
    private SpriteRenderer _eyesSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _bodySpriteRenderer = _liquidSrpiteRenderer;
        _bodyAnimator = _liquidAnimator;
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
        _bodyAnimator.SetTrigger("jump");
    }

    public void SetIsMoving(bool hasNoVelocity){
        if(hasNoVelocity){
            _bodyAnimator.SetBool("isMovingVerticaly", false);
            _eyesAnimator.SetTrigger("Normal");
        }
        else{
            _bodyAnimator.SetBool("isMovingVerticaly", true);
            _eyesAnimator.SetTrigger("Angry");
        }
    }

    public void Fall(){
        _bodyAnimator.SetTrigger("fall");
    }

    public void SetIsOnGround(bool isOnGround){
        _bodyAnimator.SetBool("isOnGround", isOnGround);
    }

    public void Move(float moveInput){
        if(moveInput<0){
            _bodyAnimator.SetBool("isMovingHorizontally", true);
            _bodySpriteRenderer.flipX = true;
            _eyesSpriteRenderer.flipX = true;
        }
        else if(moveInput>0){
            _bodyAnimator.SetBool("isMovingHorizontally", true);
            _bodySpriteRenderer.flipX = false;
            _eyesSpriteRenderer.flipX = false;
        }
        else{
            _bodyAnimator.SetBool("isMovingHorizontally", false);
        }
    }

    public void OnContaminated(){
        _bodySpriteRenderer.color = Color.gray;
    }

    public void Vaporize(){
        _bodyAnimator = _vaporAnimator;
        _bodySpriteRenderer = _vaporSpriteRenderer;
    }

    public void Liquify(){
        _bodyAnimator = _liquidAnimator;
        _bodySpriteRenderer = _liquidSrpiteRenderer;
    }

    public void Solidify(){
        _bodyAnimator = _solidAnimator;
        _bodySpriteRenderer = _solidSpriteRenderer;
    }

    public void SetImmune(bool isImmune){
        _bodyAnimator.SetBool("isImmuneToDamage", isImmune);
    }
}
