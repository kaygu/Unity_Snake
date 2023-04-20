using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class SnakeLife : MonoBehaviour
    {
        [SerializeField] private Apple m_apple;

        private bool m_isAlive = true;
        private Vector3 m_prevHeadPosition;
        private PlayerController m_controller;
        private void Awake()
        {
            m_controller = GetComponent<PlayerController>();
        }

        private void Start()
        {

            m_prevHeadPosition = transform.position;
        }


        private void Update()
        {
            if (m_isAlive && m_prevHeadPosition != transform.position)
            {
                if (CheckIfDead()) Debug.LogWarning("Snake is dead");

                Vector3 apple_pos = m_apple.Position;
                if (!m_apple.IsEaten() && apple_pos == transform.position)
                {
                    m_apple.Eat();
                    m_controller.IncreaseMoveSpeed();
                }
                else if (m_apple.IsEaten() && apple_pos != transform.position)
                {
                    m_apple.Spawn();
                    m_controller.DigestApple();
                }
                m_prevHeadPosition = transform.position;
            }

        }

        private bool CheckIfDead()
        {
            if (m_controller.IsWallCollision())
            {
                m_isAlive = false;
                GameManager.Instance.GameState = GameStateEnum.DEAD;
                return true;
            }
            if (m_controller.IsTailCollision())
            {
                m_isAlive = false;
                GameManager.Instance.GameState = GameStateEnum.DEAD;
                return true;
            }
            return false;
        }
    }
}

