using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] private GameObject m_apple;
        [SerializeField] private SnakeLife m_snake;
        private GameManager m_gm;

        private bool m_isEaten = true;
        private Vector3 m_pos = new Vector3(0, 0, 0);
        

        private void Start()
        {
            m_gm = GameManager.Instance;
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
        }

        public void Eat()
        {
            m_apple.SetActive(false);
            m_isEaten = true;
        }

        private void Move()
        {
            Vector3[] freeSpace = m_gm.Grid.Except(m_snake.OccupiedPlaces()).ToArray(); //m_gm.Grid.Where(x => !m_snake.OccupiedPlaces().Contains(x)).ToArray();  //
            int x = Random.Range(0, freeSpace.Length);
            m_apple.transform.position = freeSpace[x];
            m_apple.SetActive(true);
            Position = m_apple.transform.position;
        }

        public Vector3 Position
        {
            get
            {
                return m_pos;
            }
            private set
            {
                m_pos = value;
            }
        }

        public bool IsEaten()
        {
            return m_isEaten;
        }
    }

}
