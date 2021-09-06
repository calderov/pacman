using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavigationNamespace {
    public class NavigationUtils
    {
        private static Vector2[] directions = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};

        public static Vector2 WalkAway(Node currentNode, Vector2 position)
        {
            Vector2 bestDirection = currentNode.validDirections[0];
            float maxDistance = Vector2.Distance(currentNode.neighbors[0].transform.position, position);

            for (int i = 0; i < currentNode.neighbors.Length; i++)
            {
                float distance = Vector2.Distance(currentNode.neighbors[i].transform.position, position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    bestDirection = currentNode.validDirections[i];
                }
            }

            return bestDirection;
        }

        public static Vector2 MoveCloser(Node currentNode, Vector2 position)
        {
            Vector2 bestDirection = currentNode.validDirections[0];
            float minDistance = Vector2.Distance(currentNode.neighbors[0].transform.position, position);

            for (int i = 0; i < currentNode.neighbors.Length; i++)
            {
                float distance = Vector2.Distance(currentNode.neighbors[i].transform.position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestDirection = currentNode.validDirections[i];
                }
            }

            return bestDirection;
        }

        public static Vector2 RandomDirection(Node currentNode, Vector2 currentDirection)
        {
            int index = Random.Range(0, 4);
            Vector2 newDirection = directions[index];
            return newDirection == InvertDirection(currentDirection) ? currentDirection : newDirection;
        }

        public static Vector2 InvertDirection(Vector2 direction)
        {
            return new Vector2(direction.x * -1, direction.y * -1);
        }
    }
}
