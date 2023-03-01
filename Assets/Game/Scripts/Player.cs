using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 500;


    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;

    private float horizontal;

    private string currentAnimName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       isGrounded = CheckGround();
        Debug.Log(isGrounded);
        //-1 -> 0 -> 1
       horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(jumpForce * Vector2.up);
        }
        if (!isGrounded && rb.velocity.y < 0) 
        {
            ChangeAnim("fall");    
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0,horizontal > 0 ? 0: 180,0));

            //transform.localScale = new Vector3(horizontal, 1, 1);
        } else if(isGrounded != false) 
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,1.1f,groundLayer);

        if (hit.collider != null) return true;
        else return false;
    }

    private void Attack()
    {

    }
    private void Throw()
    {

    }
    private void Run ()
    {

    }
    private void Jump()
    {

    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName!= animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
