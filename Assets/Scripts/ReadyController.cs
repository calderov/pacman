using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;

public class ReadyController : MonoBehaviour
{
    float timeout = 5f;
    void Start()
    {
        GameState.isIntroPlaying = true;
        SoundManager.PlaySound("intro");
    }

    void Update()
    {
        if (timeout <= 0)
        {
            GameState.isIntroPlaying = false;
            Destroy(gameObject);
        }
        timeout -= Time.deltaTime;
    }
}
