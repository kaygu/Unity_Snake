using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public enum GameStateEnum
    {
        PLAYING,
        DEAD,
        WON
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Vector2 WorldOrigin = new Vector2(0, 0);
        [Range(10, 100)]
        public int Size;
        public bool CycleThroughWalls = true;
        [Space]
        public float MoveDelay = .5f;
        public float MoveDelayIncrement = .01f;

        [HideInInspector]
        public Vector3[] Grid;

        [HideInInspector]
        public GameStateEnum GameState = GameStateEnum.PLAYING;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError("Could not instantiate GameManager Singleton");
                Destroy(this);
                return;
            }
            Instance = this;
            Debug.Log("GameManager Intance created");

            Grid = new Vector3[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i * Size + j] = new Vector3(i, j, 0);
                }
            }
        }
    }

}
