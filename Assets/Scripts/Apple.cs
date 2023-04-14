using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] private GameObject m_applePrefab;
        private int m_gridSize;
        private GameObject m_apple;
        private bool m_isEaten = true;
        private GameManager m_gm;

        private void Start()
        {
            m_gm = GameManager.Instance;
            m_gridSize = m_gm.size;
            m_apple = Instantiate(m_applePrefab);
            m_apple.SetActive(false);
            Spawn();
        }
        public void Spawn()
        {
            if (m_isEaten)
            {
                Move();
                m_isEaten = false;
            }
            // Spawn apple & return position
            // Apple can be anywhere except in snake tail or head
        }

        public void Eat()
        {
            m_apple.SetActive(false);
            m_isEaten = true;
        }

        private Vector3 Move()
        {
            int x = Random.Range(-m_gridSize, m_gridSize);
            int y = Random.Range(-m_gridSize, m_gridSize);
            m_apple.transform.position = new Vector3(x, y, m_apple.transform.position.z);
            m_apple.SetActive(true);
            return m_apple.transform.position;
        }
    }

}
