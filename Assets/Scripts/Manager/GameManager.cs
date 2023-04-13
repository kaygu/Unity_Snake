using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_instance;

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
        }

        private void Update()
        {

        }

        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    Debug.LogError("GameManager is NULL");
                }
                return m_instance;
            }
            private set
            {
                m_instance = value;
            }
        }
    }

}
