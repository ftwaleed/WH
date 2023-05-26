using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.EventSystems;


public class playermovement : MonoBehaviour
{
    private float horizontal;

    private float vertical;

    public   float speed = 8f;
    private bool isFacingRight = true;
    public Animator animator;

    private bool isLadder;

 
 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource engageWithSomthing;


    private bool moveLeft;
    private bool moveRight;

    private bool moveUp;
    private bool moveDown;
    public Button actionButton;

    void Start()
    {
        
        actionButton.onClick.AddListener(EngageWithSomething);    
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveLeft=false;
        moveRight=false;
        moveUp=false;
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }
    
    public void PointerDownLeft(){
        moveLeft=true;
    }

    public void PointerUpLeft(){
        moveLeft=false;
    }

    public void PointerDownRight(){
        moveRight=true;
    }

    public void PointerUpRight(){
        moveRight=false;
    }

    public void PointerDownClimb(){
        moveUp=true;
    }

    public void PointerUpClimb(){
        moveUp=false;
    }

    public void PointerclimbDown(){
        moveDown=true;
    }

    public void Pointerclimbup(){
        moveDown=false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        MovePlayer();

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if(isLadder){
            animator.SetFloat("Speed", Mathf.Abs(vertical));
        }else{

        }
        Flip();
    }

   private void MovePlayer()
{
    if (isLadder)
    {
        if (moveUp)
        {
            vertical = speed;
            moveDown = false; // Disable moveDown when moveUp is pressed
        }
        else if (moveDown)
        {
            vertical = -speed;
            moveUp = false; // Disable moveUp when moveDown is pressed
        }
        else
        {
            // No vertical input, player stays at current position on the ladder
            vertical = 0;
        }
    }
    else
    {
        vertical = 0;
    }

    if (moveLeft)
    {
        horizontal = -speed;
    }
    else if (moveRight)
    {
        horizontal = speed;
    }
    else
    {
        horizontal = 0;
    }
}


    private void FixedUpdate()
{
    rb.velocity = new Vector2(horizontal, rb.velocity.y);

    if (isLadder)
    {
        // Apply vertical movement on the ladder
        if (moveUp)
        {
            // Move player up when the up button is pressed
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (moveDown)
        {
            // Move player down when the down button is pressed
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            // No vertical input, player stays at current position on the ladder
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        // Disable gravity while on the ladder
        rb.gravityScale = 0f;
    }
    else
    {
        // Enable gravity when not on the ladder
        rb.gravityScale = 1f;
    }
}


    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }

    private void EngageWithSomething()
    {
        engageWithSomthing.Play();
    }
}
