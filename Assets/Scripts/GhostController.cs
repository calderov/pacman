using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameNamespace;

public class GhostController : MonoBehaviour
{
    public float speed = 0.1f;
    public float vulnerableSpeed = 0.08f;
    public bool isVulnerable;
    public Node currentNode;
    public Node nextNode;
    public Node respawnNode;
    public PlayerController player;
    public float initialActivationTime = 0; // Time to activate the current ghost at boot
    public float reactivationTime = 0; // Time to reactivate a ghost after it has been eaten
    protected float remainingTimeUntilActivation = 0; // Remaining time until the ghost is activated
    protected Vector2 currentDirection = Vector2.zero;
    protected Vector2 nextDirection = Vector2.zero;
    protected Animator animator;
    protected Rigidbody2D rigidBody;
    protected bool interGhostColliding;
    protected Color debugColor = Color.red;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        interGhostColliding = false;
        remainingTimeUntilActivation = initialActivationTime;

        // Register the current ghost in the list of ghosts known to other objects in the game
        GameState.ghosts.Add(this);
    }

    protected void FixedUpdate() 
    {
        if (GameState.isIntroPlaying)
        {
            return;
        }

        PickNextDirection();
        Move();
        SetAnimatorParams();
        UpdateGhostState();
    }

    public virtual void PickNextDirection()
    {
        return;
    }

    protected void UpdateGhostState()
    {
        if (remainingTimeUntilActivation > 0)
        {
            remainingTimeUntilActivation = Mathf.Max(0, remainingTimeUntilActivation - Time.deltaTime);
        }

        if (remainingTimeUntilActivation == 0 && isVulnerable && currentNode == respawnNode)
        {
            isVulnerable = false;
        }

        if (GameState.powerUpRemainingTime == 0)
        {
            isVulnerable = false;
        }
    }

    protected void SetAnimatorParams()
    {
        animator.SetFloat("DirX", currentDirection.x);
        animator.SetFloat("DirY", currentDirection.y);
        animator.SetBool("IsVulnerable", isVulnerable);
    }

    protected void Move()
    {
        if (!ShouldMove())
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
            nextNode = tempNode;
            Vector2 destination = nextNode.transform.position;
            Vector2 delta = Vector2.MoveTowards(transform.position, destination, Speed());
            rigidBody.MovePosition(delta);
        }

        DrawArrow(currentNode.transform.position, nextNode.transform.position);
    }

    protected float Speed()
    {
        return isVulnerable ? vulnerableSpeed : speed;
    }

    protected bool ShouldMove()
    {
        // Stop moving if game is over or player won
        if (GameState.isGameOver || GameState.isGameWon)
        {
            return false;
        }

        return remainingTimeUntilActivation == 0;
    }

    protected void SwapDirection()
    {
        Vector2 tempDirection = currentDirection;
        currentDirection = nextDirection;
        nextDirection = tempDirection;

        Node tempNode = currentNode;
        currentNode = nextNode;
        nextNode = tempNode;
    }

    protected Node GetNodeInDirection(Vector2 direction)
    {
        int nodeIndex =  Array.IndexOf(currentNode.validDirections, direction);
        if (nodeIndex != -1)
        {
            return currentNode.neighbors[nodeIndex];
        }
        return null;
    }

    protected Vector2 InvertDirection(Vector2 direction)
    {
        return new Vector2(direction.x * -1, direction.y * -1);
    }

    public void MakeVulnerable()
    {
        isVulnerable = true;
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            if (isVulnerable)
            {
                gameObject.transform.position = respawnNode.transform.position;
                currentNode = respawnNode;
                nextNode = respawnNode;
                remainingTimeUntilActivation = reactivationTime;
                
                SoundManager.PlaySound("eatGhost");

                GameState.score += 800;
            }
            else
            {
                GameState.isGameOver = true;
            }
        }
    }

    protected Vector3 GetPlayerPosition()
    {
        if (GameState.isGameOver)
        {
            return Vector3.zero;
        }

        return player.transform.position;
    }

    protected Vector3 GetPlayerCurrentNodePosition()
    {
        if (GameState.isGameOver)
        {
            return Vector3.zero;
        }
        return player.currentNode.transform.position;
    }

    protected Vector3 GetPlayerNextNodePosition()
    {
        if (GameState.isGameOver)
        {
            return Vector3.zero;
        }
        return player.nextNode.transform.position;
    }

    protected void DrawArrow(Vector2 from, Vector2 to)
    {
        Vector2 direction = (to - from).normalized;
        if (direction == Vector2.up)
        {
            Debug.DrawLine(from, to, debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y - 0.5f), debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y - 0.5f), debugColor, 1f);
        }

        if (direction == Vector2.down)
        {
            Debug.DrawLine(from, to, debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y + 0.5f), debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y + 0.5f), debugColor, 1f);
        }

        if (direction == Vector2.left)
        {
            Debug.DrawLine(from, to, debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y + 0.5f), debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x + 0.5f, to.y - 0.5f), debugColor, 1f);
        }

        if (direction == Vector2.right)
        {
            Debug.DrawLine(from, to, debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y + 0.5f), debugColor, 1f);
            Debug.DrawLine(to, new Vector2(to.x - 0.5f, to.y - 0.5f), debugColor, 1f);
        }
    }
}
