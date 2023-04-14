using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Range(10, 100)]
        public int size = 10;
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
        }

    }

}
