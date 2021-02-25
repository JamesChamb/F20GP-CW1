using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public LayerMask Ground;
    public CapsuleCollider col;
    public float jumpHeight = 100f;
    public float speed = 10f;
    public float rotateSpeed = 3f;
    public float Health = 20.0f;
    public bool takenHit = false;
    public bool dead = false;
    public Text gameOverText;
    public HealthBar hBar;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool doubleJump = true;
    private Vector3 moveInputs = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {

        isGrounded = Physics.Raycast(rb.position + new Vector3(0, 0.1f, 0), Vector3.down, 0.15f);
        //isGrounded = (rb.velocity.y > -0.05 && rb.velocity.y < 0.05 && );
        if (isGrounded)
        {
            doubleJump = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isDoubleJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }

        moveInputs = Vector3.zero;
        moveInputs.x = Input.GetAxis("Horizontal");
        moveInputs.z = Input.GetAxis("Vertical");

        if (rb.velocity.y < -0.9)
        {
            animator.SetBool("isFalling", true);
            //animator.SetBool("isJumping", false);
            animator.SetBool("isDoubleJumping", false);  
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            animator.SetBool("isDoubleJumping", true);
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            doubleJump = false;
        }

        if (takenHit)
        {
            hBar.barFill = Health / 10.0f;
            if (dead)
            {
                animator.SetBool("dead", true);
                gameOverText.text = "You are Dead";
                StartCoroutine(waiting(3.0f));
            }
            else
            {
                animator.SetBool("takenHit", true);
                takenHit = false;
            } 
        }
        else
        {
            animator.SetBool("takenHit", false);
        }
    }

    void FixedUpdate()
    {

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, Mathf.LerpAngle(transform.rotation.eulerAngles.y, Camera.main.transform.eulerAngles.y, Time.deltaTime) - transform.rotation.eulerAngles.y, 0) * rotateSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);

        animator.SetBool("isMoving", moveInputs.x != 0 || moveInputs.z != 0);

        if (Input.GetKey(KeyCode.LeftShift) && !(Input.GetKey(KeyCode.S)))
        {
            animator.SetFloat("MoveX", moveInputs.x);
            animator.SetFloat("MoveY", moveInputs.z);
            rb.MovePosition(transform.position + transform.TransformDirection(moveInputs) * (speed*2) * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetFloat("MoveX", moveInputs.x / 2);
            animator.SetFloat("MoveY", moveInputs.z / 2);
            rb.MovePosition(rb.position + transform.TransformDirection(moveInputs) * speed * Time.fixedDeltaTime);
        }
    }

    IEnumerator waiting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("SampleScene");
    }
}
