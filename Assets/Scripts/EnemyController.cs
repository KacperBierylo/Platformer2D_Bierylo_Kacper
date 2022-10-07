using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float XMin = 1.5f;
    public float xMax = 1.5f;
    public float moveSpeed = 1f;
    public Animator animator;
    private Rigidbody2D rigidBody;
    private bool isFacingRight;
    private bool isMovingRight;
    private float startPositionX;
    private float killOffset = 0.2f;
    // Start is called before the first frame update
    private void Awake()
    {
        startPositionX = this.transform.position.x;
        //this.transform.position = new Vector2(Random.Range(startPositionX - XMin, startPositionX + xMax), this.transform.position.y);
        //Debug.Log($"startPositionX: {startPositionX}");
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        isFacingRight = true;
        isMovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            if (this.transform.position.x < startPositionX + xMax)
            {
                MoveRight();
            }
            else
            {
                isMovingRight = false;
                MoveLeft();
                flip();
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - XMin)
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
                flip();
            }
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
        if (rigidBody.velocity.x > - moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.left * 0.6f, ForceMode2D.Impulse);
        }
    }


    void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.7f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.position.y > this.gameObject.transform.position.y + killOffset)//killOffset
            {
                Debug.Log("Enemy dead!");
                animator.SetBool("isDead", true);
                //this.gameObject.SetActive(false);
                StartCoroutine(KillOnAnimationEnd());
            }
            else
            {
                Debug.Log("Killed player!");
            }
        }

    }

}
