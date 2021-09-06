using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag.Equals("Player"))
        {
            SoundManager.PlaySound("pellet");

            GameState.score += 100;
            GameState.pelletCounter += 1;
            Destroy(gameObject);

            if (GameState.pelletCounter == 244)
            {
                GameState.isGameWon =  true;
            }
        }
    }
}
