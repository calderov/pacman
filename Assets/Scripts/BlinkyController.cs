using System.Collections;
using System.Collections.Generic;
using NavigationNamespace;
using UnityEngine;

public class BlinkyController : GhostController
{
    private List<Node> visitedNodes = new List<Node>();
    private int maxNodes = 5;

    public override void Start()
    {
        base.Start();
        debugColor = Color.red;
    }

    public override void PickNextDirection()
    {
        if (visitedNodes.Count == maxNodes)
        {
            visitedNodes = new List<Node>();
        }

        nextDirection = isVulnerable ? 
                        NavigationUtils.WalkAway(currentNode, GetPlayerPosition()) :
                        NavigationUtils.MoveCloser(currentNode, GetPlayerNextNodePosition());
    }
}
