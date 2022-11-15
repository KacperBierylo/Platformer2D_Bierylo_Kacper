using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControllerLevel1 : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 8f;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    public Animator animator;
    private bool isWalking;
    private bool isFacingRight;
    private int score = 0;
    private float killOffset = 0.2f;
    private int lives = 3;
    private Vector2 startPosition;
    private int maxKeyNumber = 3;
    int keyNumber = 0;
    public AudioClip collectSound;
    public AudioClip golemDeathSound;
    public AudioClip playerDeathSound;
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.currentGameState == GameState.GS_GAME)
        {
            rigidBody.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isWalking = false;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (!isFacingRight) flip();
                // transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                MoveRight();
                isWalking = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (isFacingRight) flip();
                //transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                MoveLeft();
                isWalking = true;
            }
            //rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            else if (rigidBody.velocity.x != 0) rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);


            if (Input.GetKeyDown(KeyCode.Space))
                jump();
            else if (Input.GetKeyDown(KeyCode.F2))
                flyJump();
            animator.SetBool("isGrounded", isGrounded());
            animator.SetBool("isWalking", isWalking);

        }
        else
        {
            rigidBody.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //rigidBody.velocity = new Vector2(0, 0);
        }
    }
    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 0.8f, groundLayer.value);
    }

    void jump()
    {
        if (isGrounded()){
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("jumping");
        }
    }

    void flyJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hel3"))
        {
            source.PlayOneShot(collectSound, AudioListener.volume);
            GameManager.instance.AddHel(1);
            score += 1;
            Debug.Log($"Score: {score}");
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Exit")){
            if (keyNumber != maxKeyNumber && (SceneManager.GetActiveScene().name == "Level1")) Debug.Log($"You need: {maxKeyNumber - keyNumber} more keys to leave this level!");
            else
            {
                Debug.Log("Well done! You finished this level!");

            }
            if (GameManager.instance.keysCompleted || (SceneManager.GetActiveScene().name == "Level2"))
            {
                rigidBody.velocity = new Vector2(0, 0);
                GameManager.instance.LevelCompleted();
            }

        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.transform.position.y + killOffset < this.transform.position.y)
            {
                source.PlayOneShot(golemDeathSound, AudioListener.volume);
                score += 10;
                GameManager.instance.AddEnemyDefeated(1);
                Debug.Log($"Enemy Killed! Score:{score}");
            }
            else
            {

                if (lives > 1)
                {
                    GameManager.instance.addHitPoints(-1);
                    lives--;
                }
                else
                {
                    Debug.Log("Game over!");
                    this.transform.position = startPosition;
                }
            }


        }
        else if (other.CompareTag("Key"))
        {
            keyNumber++;
            GameManager.instance.AddKey(1);
            Debug.Log($"You found the key! Number of keys: {keyNumber}");
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("HitPoint"))
        {
            lives++;
            GameManager.instance.addHitPoints(1);
            other.gameObject.SetActive(false);
            Debug.Log($"You have: {lives} hit points!");
        }

        else if (other.CompareTag("FallLevel"))
        {
            source.PlayOneShot(playerDeathSound, AudioListener.volume);
            Debug.Log("�mier� przez spadek");
            GameManager.instance.GameOver();
        }
    }


    void MoveRight()
    {
        if (rigidBody.velocity.x < moveSpeed)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.right * 0.6f, ForceMode2D.Impulse);
        }
    }

    void MoveLeft()
    {
        if (rigidBody.velocity.x > -  moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.left * 0.6f, ForceMode2D.Impulse);
        }
    }

}
