using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridgeController : MonoBehaviour
{
    public float xMin = 1.5f;
    public float xMax = 1.5f;
    public float moveSpeed = 1f;
    private Rigidbody2D rigidBody;
    private bool isMovingRight;
    private float startPositionX;
    // Start is called before the first frame update
    private void Awake()
    {
        startPositionX = this.transform.position.x;
        //this.transform.position = new Vector2(Random.Range(startPositionX - XMin, startPositionX + xMax), this.transform.position.y);
        Debug.Log($"startPositionXMost: {startPositionX}");
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
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
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - xMin)
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
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
        if (rigidBody.velocity.x > -moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.left * 0.6f, ForceMode2D.Impulse);
        }
    }
}
