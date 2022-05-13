using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;
    public ParticleSystem m_particle;
    public ParticleSystem m_dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;

    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        Physics2D.gravity *= gravityModifier;

        gameController = FindObjectOfType<GameController>(gameController);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }
    //nhay khi nhan space
    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameController.IsGameOver())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            m_dirtParticle.Stop();
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
            m_dirtParticle.Stop();
        }
    }

}
