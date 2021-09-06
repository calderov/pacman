using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    public Node targetNode;
    public Node targetNextNode;
    public float offsetX;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    private void OnTriggerEnter2D(Collider2D colider)
    {
        float x = targetNode.transform.position.x;
        float y = targetNode.transform.position.y;

        switch (colider.gameObject.tag)
        {
            case "Player":
                colider.gameObject.transform.position = new Vector3(x + offsetX, y, 0);
                colider.gameObject.GetComponent<PlayerController>().currentNode = targetNode;
                colider.gameObject.GetComponent<PlayerController>().nextNode = targetNextNode;
                break;

            case "Ghost":
                colider.gameObject.transform.position = new Vector3(x + offsetX, y, 0);
                colider.gameObject.GetComponent<GhostController>().currentNode = targetNode;
                colider.gameObject.GetComponent<GhostController>().nextNode = targetNextNode;
                break;
        }
    }
}
