using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isGrounded;
    private float jumpPressedRemember, groundedRemember, jumpPressedRememberTime = 0.2f, groundedRememberTime = 0.2f;

    private Animator animator;

    public static Player instance;

    public float initialSpeed, jumpPower, maxSpeed;
    public LayerMask ground;
    public Transform groundCheckPoint;
    public Rigidbody2D playerRigidbody;
    public bool isFrozen, isRespawning, isInvulnerable = false, touchedLastFrame = false;

    public SpriteRenderer playerSpriteRenderer;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimateIdle();

        if (!isFrozen) {
            Run();
            JumpLogic();
            AnimateJump();
        }

        else {
            if (isRespawning && AdManager.instance.isRewardVidPlayed) {

                if (Input.GetMouseButtonDown(0)) {

                    Vector2 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                        
                    RespawnOnPosition(new Vector2(clickedPosition.x, 4));

                    AdManager.instance.isRewardVidPlayed = false;
                    isRespawning = false;
                    
                    LevelManager.instance.ResumeGame();
                }
            }

        }
    }

    void Run() {
        playerRigidbody.velocity = new Vector2(initialSpeed, playerRigidbody.velocity.y);
    }

    void JumpLogic() {    

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, ground);

        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;

        if (isGrounded) {
            groundedRemember = groundedRememberTime;
        }

        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetButtonDown("Jump")) {
            jumpPressedRemember = jumpPressedRememberTime;

            if (!isGrounded && LevelManager.instance.doubleJumpsAvailable > 0) {
                LevelManager.instance.doubleJumpsAvailable -= 1;
                LevelManager.instance.UpdateDoubleJumpText();
                jumpPressedRemember = 0;
                groundedRemember = 0;
                Jump();
            }
        }

        if (jumpPressedRemember > 0 && groundedRemember > 0) {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            Jump();
        }
        #endif

        if (touchedLastFrame && Input.touchCount == 0) {
            touchedLastFrame = false;
        }

        else if (Input.touchCount > 0 && !touchedLastFrame) {
            jumpPressedRemember = jumpPressedRememberTime;

            if (!isGrounded && LevelManager.instance.doubleJumpsAvailable > 0) {
                LevelManager.instance.doubleJumpsAvailable -= 1;
                LevelManager.instance.UpdateDoubleJumpText();
                jumpPressedRemember = 0;
                groundedRemember = 0;
                Jump();
            }

            if (jumpPressedRemember > 0 && groundedRemember > 0) {
                jumpPressedRemember = 0;
                groundedRemember = 0;
                Jump();
            }

            touchedLastFrame = true;
        }

    }

    void Jump() {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);
        AudioManager.instance.PlaySFX(3);
    }

    void AnimateJump() {   
        animator.SetBool("isGrounded", isGrounded);
    }

    void AnimateIdle() {
        animator.SetBool("isFrozen", isFrozen);
    }

    void RespawnOnPosition(Vector3 respawnPoint) {

        StartCoroutine(MakeInvulnerable(8));
        
        playerSpriteRenderer.enabled = true;
        playerRigidbody.velocity = Vector2.zero;

        transform.position = respawnPoint;

        CameraController.instance.isFollowingTarget = true;

        StartCoroutine(LevelManager.instance.PreStartCountdown());

    }

    IEnumerator MakeInvulnerable(int waitTime) {
        isInvulnerable = true;
        playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g, playerSpriteRenderer.color.b, 0.5f);

        yield return new WaitForSeconds(waitTime);

        playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g, playerSpriteRenderer.color.b, 1f);
        isInvulnerable = false;
    }

}
