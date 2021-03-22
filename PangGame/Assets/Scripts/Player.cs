using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheckCollider;
    public LayerMask groundLayer;
    public float jumpPower = 280;

    public Joystick joystick;


    public float speed = 150f;

    private const float groundCheckRaduis = 0.2f;
    private bool isGrounded = false;
    private float horizontalValue;
    private bool jump = false;
    private bool facingRight = true;
    private bool canWalk = true;

    public GameObject currentArrow,rocket;
    public AudioClip shootSound;
    private bool canShoot = false;

    void Awake()
    {
        InitializeVariables();
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalValue));
        CheckIfJumping();
    }
    public void PressJumping() // if the player press the jump button this function will call.
    {
        jump = true;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }
    public void PressShooting() // if the player press the shoot button
    {
        if (canShoot)
        {
            StartCoroutine(ShootTheRocket());
            canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        horizontalValue = joystick.Horizontal;
 
        GroundCheck();
        Move(horizontalValue, jump);
        jump = false;
    }

    void InitializeVariables()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    IEnumerator ShootTheRocket()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        canWalk = false;
        animator.Play("Shoot");

        Vector3 tempPlayerPosition = transform.position; // save the player position to shoot the rocket from him
        tempPlayerPosition.y -= 1.3f;
        tempPlayerPosition.x += 0.5f;
        Destroy(currentArrow);
        currentArrow = Instantiate(rocket, tempPlayerPosition, Quaternion.identity);

        yield return new WaitForSeconds(0.45f); // added a delay to interapt the player from moving for a a short amout of time after shooting
        animator.SetBool("Shoot", false);
        canWalk = true;

        yield return new WaitForSeconds(0.45f);
        canShoot = true;
    }

    void CheckIfJumping()
    {
        // If we press jump button enable jump
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            jump = true;
        }
        // Otherwise disable it
        else if (Input.GetButtonDown("Jump"))
            jump = false;

        // Set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    // This functions make the player move left and right
    void Move(float dir, bool jumpFlag)
    {
        if (canWalk)
        {
            if (isGrounded && jumpFlag)
            {
                jumpFlag = false;
                rb.AddForce(new Vector2(0f, jumpPower));
            }

            float xVal = dir * speed * Time.fixedDeltaTime;
            Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
            rb.velocity = targetVelocity;

            // Flip to left
            if (facingRight && dir < 0)
            {
                transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
                facingRight = false;
            }
            // Flip to Right
            else if (!facingRight && dir > 0)
            {
                transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
                facingRight = true;
            }
        }
        // if the player is grounded and press jump then jump
    }

    // Kill the player wait and restart the game at the same level.
    IEnumerator KillThePlayerAndRestartGame()
    {
        canWalk = false;
        canShoot = false;
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(1.5f);
        LevelManager.LevelCounter = 0;
        ScoreManagerScript.ResetScore();
        Application.LoadLevel(Application.loadedLevelName);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        string[] name = target.name.Split();
        if(name.Length > 2)
        {
            if (name[2] == "Ball" || name[2] == "Ball(Clone)")
            {

                StartCoroutine(KillThePlayerAndRestartGame()); // if the charecture is  killed by an object named Ball.
            }
        }
    }

    // checking all the time if the player is touching the ground for animation + for jump
    void GroundCheck()
    {
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRaduis, groundLayer);
        if (colliders.Length > 0) // we are grounded
            isGrounded = true;

        animator.SetBool("IsJumping", !isGrounded);
    }
}
