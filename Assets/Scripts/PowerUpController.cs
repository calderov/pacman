using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag.Equals("Player"))
        {
            GameState.score += 250;
            GameState.pelletCounter += 1;
            GameState.powerUpRemainingTime += 5;
            Destroy(gameObject);

            SoundManager.PlaySound("ghostPanic");

            foreach(GhostController ghost in GameState.ghosts)
            {
                ghost.MakeVulnerable();
            }
        }
    }
}
