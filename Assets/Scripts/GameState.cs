using System.Collections;
using System.Collections.Generic;

namespace GameNamespace {
    public class GameState
    {
        public static int score = 0;
        public static int pelletCounter = 0; // When this reaches 244 win the game
        public static float powerUpRemainingTime = 0f;
        public static bool isIntroPlaying = false;
        public static bool isGameWon = false;
        public static bool isGameOver = false;
        public static List<GhostController> ghosts = new List<GhostController>();
    }

}
