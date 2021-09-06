using System.Collections;
using System.Collections.Generic;
using NavigationNamespace;
using UnityEngine;

public class ClydeController : GhostController
{
    public override void Start()
    {
        base.Start();
        debugColor = Color.yellow;
    }

    public override void PickNextDirection()
    {
        nextDirection = isVulnerable ? 
                        NavigationUtils.WalkAway(currentNode, GetPlayerPosition()) :
                        NavigationUtils.RandomDirection(currentNode, currentDirection);

        if (nextDirection == NavigationUtils.InvertDirection(currentDirection))
        {
            SwapDirection();           
        }
    }
}
