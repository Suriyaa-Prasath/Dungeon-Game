using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        public float climbSpeed;
        private float moveInput;
        private float climbInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        private bool isClimbing;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        public GameManager gameManager;

        AudioManager audioManager;

        // Keys for climbing the ladder
        public KeyCode climbUpKey = KeyCode.W;
        public KeyCode climbDownKey = KeyCode.S;

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //gameManager = GameObject.Find("GameManager Variant").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            moveInput = Input.GetAxis("Horizontal");
            climbInput = Input.GetAxis("Vertical");

            if (isClimbing)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, climbInput * climbSpeed);
                animator.SetInteger("playerState", 3); // Turn on climb animation
                rigidbody.gravityScale = 0; // Disable gravity while climbing
            }
            else
            {
                rigidbody.gravityScale = 1; // Enable gravity when not climbing

                if (Input.GetButton("Horizontal"))
                {
                    Vector3 direction = transform.right * moveInput;
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                    animator.SetInteger("playerState", 1); // Turn on run animation
                }
                else
                {
                    if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
                }

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    audioManager.PlaySFX(audioManager.jump);
                }

                if (!isGrounded) animator.SetInteger("playerState", 2); // Turn on jump animation

                if (facingRight == false && moveInput > 0)
                {
                    Flip();
                }
                else if (facingRight == true && moveInput < 0)
                {
                    Flip();
                }
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                gameManager.coinsCounter += 1;
                audioManager.PlaySFX(audioManager.coin);
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Ladder" && (Input.GetKey(climbUpKey) || Input.GetKey(climbDownKey)))
            {
                isClimbing = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); // Stop any vertical movement
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ladder" && (Input.GetKey(climbUpKey) || Input.GetKey(climbDownKey)))
            {
                isClimbing = true;
            }
            else if (other.gameObject.tag == "Ladder")
            {
                isClimbing = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ladder")
            {
                isClimbing = false;
            }
        }
    }
}
