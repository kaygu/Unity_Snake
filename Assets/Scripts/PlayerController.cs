using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_moveDelay;
        [SerializeField] private float m_moveDelayIncrement;
        private float m_timeElapsed;

        private Vector3 m_direction = Vector3.up;
        private List<Vector3> m_dirQueue = new List<Vector3>();
        private void Awake()
        {
            m_timeElapsed = m_moveDelay;
        }

        private void FixedUpdate()
        {
            if (m_timeElapsed <= 0)
            {
                if (m_dirQueue.Count > 0)
                {
                    m_direction = m_dirQueue[0];
                    m_dirQueue.RemoveAt(0);
                }
                transform.position += m_direction;
                m_timeElapsed = m_moveDelay;
            }
            m_timeElapsed -= Time.fixedDeltaTime;
        }

        public void Move(Vector3 _dir)
        {
            // Check that snake movement is legal
            if (_dir != m_direction)
            {
                int queueLen = m_dirQueue.Count;
                if (queueLen == 0 && _dir != -m_direction)
                {
                    m_dirQueue.Add(_dir);
                }
                else if (queueLen > 0)
                {
                    Vector3 lastQd = m_dirQueue.Last();
                    if (_dir != lastQd && _dir != -lastQd)
                    {
                        m_dirQueue.Add(_dir);
                    }
                }
            }
        }
    }
}

