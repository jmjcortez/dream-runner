using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isGrounded;
    private float jumpPressedRemember, groundedRemember, jumpPressedRememberTime = 0.2f, groundedRememberTime = 0.2f;

    private Animator animator;

    public static Player instance;

    public float initialSpeed, jumpPower;
    public LayerMask ground;
    public Transform groundCheckPoint;
    public Rigidbody2D playerRigidbody;
    public bool isFrozen, isRespawning;

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
                                        
                    StartCoroutine(RespawnOnPosition(new Vector2(clickedPosition.x, 4)));
                
                    AdManager.instance.isRewardVidPlayed = false;
                    isRespawning = false;
                    
                    LevelManager.instance.ResumeGame();
                }
            }

        }
    }

    void Run() {
        //papalitan pa to pag may accelerate na
        playerRigidbody.velocity = new Vector2(initialSpeed, playerRigidbody.velocity.y);
    }

    void JumpLogic() {    

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, ground);

        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;

        if (isGrounded) {
            groundedRemember = groundedRememberTime;
        }

        if (Input.GetButtonDown("Jump")) {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        if (jumpPressedRemember > 0 && groundedRemember > 0) {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            Jump();
        }
    }

    void Jump() {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);
    }

    void AnimateJump() {   
        animator.SetBool("isGrounded", isGrounded);
    }

    void AnimateIdle() {
        animator.SetBool("isFrozen", isFrozen);
    }

    IEnumerator RespawnOnPosition(Vector3 respawnPoint) {
        
        playerSpriteRenderer.enabled = true;
        playerRigidbody.velocity = Vector2.zero;

        transform.position = respawnPoint;

        CameraController.instance.isFollowingTarget = true;

        LevelManager.instance.PauseGame();

        yield return new WaitForSeconds(1);
        Debug.Log('3');
        yield return new WaitForSeconds(1);
        Debug.Log('2');
        yield return new WaitForSeconds(1);
        Debug.Log('1');
        yield return new WaitForSeconds(1);
        Debug.Log("GO");
        
        LevelManager.instance.ResumeGame();

        isFrozen = false;
    }

}
