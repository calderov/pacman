using System.Collections;
using System.Collections.Generic;
using NavigationNamespace;
using UnityEngine;

public class PinkyController : GhostController
{
    public override void Start()
    {
        base.Start();
        debugColor = Color.magenta;
    }

    public override void PickNextDirection()
    {
        nextDirection = isVulnerable ? 
                        NavigationUtils.WalkAway(currentNode, GetPlayerPosition()) :
                        NavigationUtils.MoveCloser(currentNode, GetPlayerCurrentNodePosition());

        if (nextDirection == NavigationUtils.InvertDirection(currentDirection))
        {
            SwapDirection();           
        }
    }
}
