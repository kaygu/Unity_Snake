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

        [Range(10, 100)]
        public int Size = 10;
        public Vector3[] Grid;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError("Could not instantiate GameManager Singleton");
                Destroy(this);
                return;
            }
            Instance = this;
            Debug.Log("Intance created");

            Grid = new Vector3[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i * 10 + j] = new Vector3(i, j, 0);
                }
            }
        }
    }

}
