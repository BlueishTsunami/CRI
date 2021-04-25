using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;
    public GameObject player;
    public float angle;
    public Camera cam;
    public Vector2 movement;
    public Vector2 lookDir;
    private Animator animator;
    public bool facingRight = true;
    Vector2 mousePos;
    
    // Update is called once per frame
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (facingRight && (angle < 90 || angle > -90))
        {
            FlipOrientation();
        }

        if (!facingRight && (angle > 90 || angle < -90))
        {
            FlipOrientation();
        }

        lookDir = mousePos - rb2d.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // rb2d.rotation = angle;

    }

    void FlipOrientation()                                 //Function to flip character when facing left
    {

        facingRight = !facingRight;                        // When called, flips value of facingRight Boolean
        Vector2 spriteTransform = transform.localScale;    // sets vector spriteTransform equal to current transform settings of gamesprite
        spriteTransform.x *= -1;                           // Multiplies the x axis of tranform by -1, flipping the image used as the art
        transform.localScale = spriteTransform;            // sets the transform value of your local object to the newly flipped value 
    }
}
