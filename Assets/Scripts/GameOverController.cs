using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private Renderer spriteRenderer;
    private float timeUntilShow = 1f;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameState.isGameOver)
        {
            timeUntilShow = Mathf.Max(0, timeUntilShow - Time.deltaTime);
        }

        spriteRenderer.enabled = GameState.isGameOver && timeUntilShow == 0;
    }
}
