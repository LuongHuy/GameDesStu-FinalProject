using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerMovementW3 : MonoBehaviour
{
    [Header("Import component")]
    // import component
    [SerializeField] private Rigidbody2D rd;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStatus playerStatus;

    [Header("Check step on Ground")]
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundMask;

    [Header("Check step on enemy")]
    [SerializeField] BoxCollider2D enemyCheckCollider;
    [SerializeField] LayerMask enemyMask;

    // movement parameter
    [Header("Horizontal movement")]
    [SerializeField] float maxVelocity = 7f;
    [SerializeField] float time = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] float friction = 0.6f;

    [Header("Jump Parameter")]
    // jump parameter
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float coyoteMax = 0.1f;
    [SerializeField] float jumpBufferMax = 0.1f;
    [SerializeField] float jumpTimeMin = 0.1f;
    [SerializeField] float jumpTimeMax = 0.5f;
    // special, for controlling falling speed
    [SerializeField] float gravityFallingScale = 2f;

    [Header("Shooting parameter")]
    [SerializeField] float shootingCd = 2f;
    [SerializeField] BulletStatus bulletPrefab;
    bool bullet1Ready = true;
    bool bullet2Ready = true;

    [Header("Dash Parameter")]
    // Dash movement
    [SerializeField] float dashVelocity = 30f;
    [SerializeField] float minimumDashTime = 0.2f;
    public float dashTime = 0.5f;

    [Header("Sound")]
    // For sound
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip jumpingSound;
    [SerializeField] AudioClip dashingSound;
    [SerializeField] AudioClip shootingSound;


    // private parameter
    float curr_velocity;
    [HideInInspector] public bool facingRight = true;

    // constant
    float JUMPFORCE;
    float DASHFORCE;
    float GRAVITYSCALE;

    // state_machine
    MoveState currMoveState = null;

    // Start is called before the first frame update
    void Start()
    {
        // Start at idle state
        TransitTo(new Idle());

        // Can not fully replace the gravity, because it is universal in Unity, though it is really slow.
        GRAVITYSCALE = (4 * jumpHeight / (jumpTimeMax * jumpTimeMax)) / Mathf.Abs(Physics2D.gravity.y);
        UpdateGravityScale(1);

        // To beter control the time of the jump: (1) g_scale*g = g_actual. (2) g_actual = 4*h/t^2
        JUMPFORCE = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * GRAVITYSCALE) * -2) * rd.mass;
    }

    void Update()
    {
        // Keep watch over stage change every frame
        currMoveState.StateChange();
        currMoveState.Action();
    }

    // Update the movement every FixedUpdate
    private void FixedUpdate()
    {
        currMoveState.Move();
    }

    // Go to state
    public void TransitTo(MoveState state)
    {
        // if the new state does not exist
        if (state == null)
        {
            return;
        }
        // Execute on exit
        if (currMoveState != null)
        {
            currMoveState.OnExit();
            //Debug.Log("previous state: " +currMoveState.ToString());
        }
        // Switch state
        state.SetPlayerMovement(this);
        currMoveState = state;
        // execute on enter
        currMoveState.OnEnter();
        //Debug.Log("next stage: "+ currMoveState.ToString());
    }

    public void HorizontalMove(Vector2 moveInput)
    {
        //float horizontalForce = moveInput.x * maxVelocity / time * rd.mass;
        // scale movement force by different between max velocity and current velocity, but if greater then keep current velocity
        float speedDif = maxVelocity - Mathf.Abs(rd.velocity.x);
        speedDif = speedDif > 0 ? speedDif : 0 ;

        float horizontalForce = speedDif / time * rd.mass;

        rd.AddForce(new Vector2(horizontalForce * moveInput.x, rd.velocity.y), ForceMode2D.Force);

        // If the player do not press direction button, or move against the current direction, then add friction
        if (Mathf.Abs(moveInput.x) <=0.01f || moveInput.x * rd.velocity.x < 0)
        {
            // this is for friction on air and ground. Unrealistic, but let the player easier to control jump.
            rd.velocity = new Vector2(rd.velocity.x * friction, rd.velocity.y);
        }

        // Direction of the sprite
        if (moveInput.x > 0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x < 0)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Jump()
    {
        rd.AddForce(Vector2.up * JUMPFORCE, ForceMode2D.Impulse);
    }
    // jump but half force
    public void Bounch()
    {
        rd.AddForce(Vector2.up * JUMPFORCE/2, ForceMode2D.Impulse);
    }

    public void Dash(Vector2 moveInput)
    {
        //before dash, reset the player speed. This allows dash to overcome momemtum
        // remove velocity.x, but keep velocity.y if greater than 0, so that the player can dash up easier
        float y = rd.velocity.y>0? rd.velocity.y :0;
        rd.velocity = new Vector2(0,y);
        //rd.velocity = Vector2.zero;

        // Activate dash
        // default position right
        Vector2 direction = moveInput==Vector2.zero ? Vector2.right : moveInput.normalized;
        // apply force
        rd.AddForce(direction * dashVelocity, ForceMode2D.Impulse);
    }

    private void ShootBullet()
    {
        SoundManager.Instance.playVFX(shootingSound, transform);

        Vector3 direction = facingRight ? Vector3.right : Vector3.left;
        Vector2 location = transform.position + new Vector3( 0.75f*(facingRight?1:-1),0);

        BulletStatus bullet = Instantiate(bulletPrefab, location, Quaternion.identity);

        bullet.SetDestination(transform.position + direction);
        bullet.SetBoss(playerStatus);
        bullet.Activate();
        GameMasterW3.Instance.AddBullet(bullet);
    }

    IEnumerator ShootOnce()
    {
        if (bullet1Ready)
        {
            ShootBullet();
            bullet1Ready = false;
            yield return new WaitForSeconds(shootingCd);
            bullet1Ready = true;
        }
        else if (bullet2Ready) 
        {
            ShootBullet();
            bullet2Ready = false;
            yield return new WaitForSeconds(shootingCd);
            bullet2Ready = true;
        }
        else
        {
            //Debug.Log("Can not shoot");
            GameMasterW3.Instance.SpawnPopup("Gun is cooling off", transform.position, transform);
        }
    }

    public void Shoot()
    {
        StartCoroutine(ShootOnce());
    }

    public void resetVelocity()
    {
        Vector2 resetVelocity;
        // bound velocity x from -maxVelocity to maxVelocity
        resetVelocity.x = rd.velocity.x;
        // if velocity y is greater than 0, reset it to 0.
        resetVelocity.y = Mathf.Min(rd.velocity.y, 0);
        rd.velocity = resetVelocity;
    }

    public void UpdateGravityScale(float mode)
    {
        if (mode == 1)
        {
            rd.gravityScale = GRAVITYSCALE;
        }else if (mode == 2)
        {
            rd.gravityScale = GRAVITYSCALE * gravityFallingScale;
        }else if(mode == 0)
        {
            rd.gravityScale = 0;
        }
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    public void PlaySoundEff(string soundName)
    {
        if (soundName == "Jump")
        {
            SoundManager.Instance.playVFX(jumpingSound, transform);
        }
        else if (soundName == "Dash")
        {
            SoundManager.Instance.playVFX(dashingSound, transform);
        }
    }

    public void LoopingSoundEff()
    {
        SoundManager.Instance.playVFXLoop(walkingSound, transform);
    }
    public void StopLoopingSound()
    {
        SoundManager.Instance.StopVFXLoop();
    }

    // return true if is on the ground
    public bool CheckIsGround()
    {
        bool isGround = Physics2D.OverlapAreaAll(groundCheckCollider.bounds.min, groundCheckCollider.bounds.max, groundMask).Length > 0;
        return isGround;
    }

    // return true if is on the enemy
    public bool CheckStepOnEnemy()
    {
        bool isGround = Physics2D.OverlapAreaAll(enemyCheckCollider.bounds.min, enemyCheckCollider.bounds.max, enemyMask).Length > 0;
        return isGround;
    }

    public void ActivateTrail()
    {
        tr.emitting = true;
    }
    public void DeactivateTrail()
    {
        tr.emitting = false;
    }

    public bool CheckFalling()
    {
        return rd.velocity.y<0;
    }

    public bool CheckTimeJump(float time)
    {
        return time>jumpTimeMin;
    }

    public bool checkCoyote(float time)
    {
        return time < coyoteMax;
    }

    public bool CheckJumpBuffer(float time)
    {
        return time < jumpBufferMax;
    }
    public bool CheckDashTime(float time)
    {
        return time >= dashTime;
    }
    public bool CheckMinDash(float time)
    {
        return time < minimumDashTime;
    }
}
