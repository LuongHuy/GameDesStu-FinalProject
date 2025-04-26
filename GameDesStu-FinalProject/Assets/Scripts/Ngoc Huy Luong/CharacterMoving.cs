using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    public Transform checkGroundTransform;
    public LayerMask groundMask;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Vector3 currrentRotation = transform.eulerAngles;
        if( moveInput > 0)
        {
           currrentRotation.y = 0f;
        }
        else if ( moveInput < 0 )
        {
            currrentRotation.y = 180f;
        }
        transform.eulerAngles = currrentRotation;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
        }
        isGrounded = CheckGround();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    bool CheckGround()
    {
        return Physics2D.OverlapCircle(checkGroundTransform.position, 0.05f, groundMask);
        
            
       
    }
}