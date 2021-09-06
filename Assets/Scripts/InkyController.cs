using System.Collections;
using System.Collections.Generic;
using NavigationNamespace;
using UnityEngine;

public class InkyController : GhostController
{
    public override void Start()
    {
        base.Start();
        debugColor = Color.cyan;
    }

    public override void PickNextDirection()
    {
        nextDirection = isVulnerable ? 
                        NavigationUtils.WalkAway(currentNode, GetPlayerPosition()) :
                        NavigationUtils.MoveCloser(currentNode, GetPlayerPosition());

        if (nextDirection == NavigationUtils.InvertDirection(currentDirection))
        {
            SwapDirection();           
        }
    }
}
