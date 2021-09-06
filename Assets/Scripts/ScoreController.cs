using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private Text scoreText;

    void Start() 
    {
        scoreText = GetComponent<Text> ();
    }
        
    void FixedUpdate()
    {
        scoreText.text = GameState.score.ToString();
    }
}
