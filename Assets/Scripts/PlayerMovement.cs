using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f , 20f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    [SerializeField] AudioClip bowSoundEfect;
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityAtStart;
    bool isAlive = true;
    MovingPlatform movingPlatform;
    
    Color myColor = new Color(1, 0.4f, 0.4f, 1);

   
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        movingPlatform = FindObjectOfType<MovingPlatform>();
        gravityAtStart = myRigidbody2D.gravityScale;
    }

   
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

   void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
            myAnimator.SetTrigger("IsShoting");
            Instantiate(arrow, bow.position, transform.rotation);
        AudioSource.PlayClipAtPoint(bowSoundEfect, Camera.main.transform.position);


    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        
    }
    
    void OnJump(InputValue value) 
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground" , "MovingPlatform")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed); 
        }
    }

    void Run()
    {
        
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning" ,  PlayerHasHorizontalSpeed);
         
    }
    
    void ClimbLadder()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("IsClimbing", false);
            myRigidbody2D.gravityScale = gravityAtStart;
            return;
        }
        
        Vector2 climbVelocity = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
        myRigidbody2D.velocity = climbVelocity;
        myRigidbody2D.gravityScale = 0f;
        bool IsClimbing = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", IsClimbing);

    }
    //roteste spritul in functie de directia playerului
    void FlipSprite()
    {
       
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }
    //daca playerul atinge inamici , apa sau tepii , nu se mai poate misca
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies" , "Hazards")))
        {
            
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody2D.velocity = deathKick;
            mySpriteRenderer.color= myColor;
            FindObjectOfType<GameSesion>().PlayerDeath();  
        }
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSesion>().PlayerDeath();


        }
    }
}
