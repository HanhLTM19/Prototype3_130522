using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;
    public ParticleSystem m_particle;
    public ParticleSystem m_dirtParticle;

    private AudioSource playSound;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    private bool isDoubleJump = false;
    public float doubleJumpForce;

    public bool isDoubleSpeed = false;

    
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playSound = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;

        gameController = FindObjectOfType<GameController>(gameController);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        if (Input.GetKey(KeyCode.Z))
        {
            isDoubleSpeed = true;
            playerAnimator.SetFloat("Speed_Multiplier", 2.0f);
        } else if (isDoubleSpeed)
        {
            isDoubleSpeed = false;
            playerAnimator.SetFloat("Speed_Multiplier", 1.0f);
        }
    }
    
    //nhay khi nhan space
    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameController.IsGameOver())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            playSound.PlayOneShot(jumpSound, 1.0f);
            m_dirtParticle.Stop();
            isDoubleJump = false;
        } else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !isDoubleJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.Play("Running_Jump", 3, 0f);
            playSound.PlayOneShot(jumpSound, 1.0f);
            isDoubleJump = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            m_dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacles"))
        {
            //Destroy(gameObject);
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathTypeInt", 1);
            gameController.SetIsGameOver(true);
            m_particle.Play();
            playSound.PlayOneShot(crashSound, 1.0f);
            m_dirtParticle.Stop();
        }
    }

}
