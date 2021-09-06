using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameNamespace;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 0.2f;
    public float powerUpSpeed = 0.2f;
    public Node currentNode;
    public Node nextNode;
    private Vector2 currentDirection = Vector2.zero;
    private Vector2 nextDirection = Vector2.zero;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool didVictorySoundPlay = false;
    private bool didDeathSoundPlay = false;
    private float timeToDestroyAfterDeath = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameState.isIntroPlaying)
        {
            return;
        }

        ProcessInputs();
        GameState.powerUpRemainingTime = Mathf.Max(0f, GameState.powerUpRemainingTime - Time.deltaTime);

        if (GameState.isGameOver)
        {
            LoseGame();
        }

        if (GameState.isGameWon)
        {
            WinGame();
        }
    }

    void FixedUpdate() 
    {
        Move();
    }

    void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            nextDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            nextDirection = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            nextDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            nextDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (nextDirection == InvertDirection(currentDirection))
        {
            SwapDirection();           
        }
    }

    void SetAnimatorParams(Vector2 direction)
    {
        animator.SetFloat("DirX", direction.x);
        animator.SetFloat("DirY", direction.y);
        animator.SetBool("IsDeath", GameState.isGameOver);
    }

    void Move()
    {
        // Stop moving if game is over or player won
        if (GameState.isGameOver || GameState.isGameWon)
        {
            return;
        }

        if (transform.position == nextNode.transform.position)
        {
            currentNode = nextNode;
            if (GetNodeInDirection(nextDirection) != null) {
                currentDirection = nextDirection;
            }
        }

        Node tempNode = GetNodeInDirection(currentDirection);
        if (tempNode)
        {
            SetAnimatorParams(currentDirection);

            nextNode = tempNode;
            Vector2 destination = nextNode.transform.position;
            Vector2 delta = Vector2.MoveTowards(transform.position, destination, GameState.powerUpRemainingTime > 0 ? powerUpSpeed : normalSpeed);
            rigidBody.MovePosition(delta);
        }

        DrawArrow(currentNode.transform.position, nextNode.transform.position);
    }

    void SwapDirection()
    {
        Vector2 tempDirection = currentDirection;
        currentDirection = nextDirection;
        nextDirection = tempDirection;

        Node tempNode = currentNode;
        currentNode = nextNode;
        nextNode = tempNode;
    }

    Node GetNodeInDirection(Vector2 direction)
    {
        int nodeIndex =  Array.IndexOf(currentNode.validDirections, direction);
        if (nodeIndex != -1)
        {
            return currentNode.neighbors[nodeIndex];
        }
        return null;
    }

    Vector2 InvertDirection(Vector2 direction)
    {
        return new Vector2(direction.x * -1, direction.y * -1);
    }

    void WinGame()
    {
        if (!didVictorySoundPlay) {
            GameState.isGameWon = true;
            didVictorySoundPlay = true;
            SoundManager.PlaySound("victory");
        }
    }

    void LoseGame()
    {
        if (timeToDestroyAfterDeath > 0)
        {
            timeToDestroyAfterDeath = timeToDestroyAfterDeath - Time.deltaTime;

            SetAnimatorParams(currentDirection);
        
            if (!didDeathSoundPlay)
            {
                SoundManager.PlaySound("death");
                didDeathSoundPlay = true;
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    void DrawArrow(Vector2 from, Vector2 to)
    {
        Vector2 direction = (to - from).normalized;
        if (direction == Vector2.up)
        {
            Debug.DrawLine(from, to, Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y - 0.5f), Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y - 0.5f), Color.green, 1f);
        }

        if (direction == Vector2.down)
        {
            Debug.DrawLine(from, to, Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y + 0.5f), Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y + 0.5f), Color.green, 1f);
        }

        if (direction == Vector2.left)
        {
            Debug.DrawLine(from, to, Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y + 0.5f), Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y - 0.5f), Color.green, 1f);
        }

        if (direction == Vector2.right)
        {
            Debug.DrawLine(from, to, Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y + 0.5f), Color.green, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y - 0.5f), Color.green, 1f);
        }
    }
}
