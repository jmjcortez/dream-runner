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
    public bool isFrozen;


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
        if (!isFrozen) {
            Run();
            JumpLogic();
            Animate();
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
            // if (isGrounded) {
            //     Jump();
            // }
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

    void Animate() {   
        animator.SetBool("isGrounded", isGrounded);
    }

}
