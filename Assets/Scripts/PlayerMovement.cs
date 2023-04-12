using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerController m_playerController;
        private float m_xMovement;
        private float m_yMovement;

        private void Awake()
        {
            m_playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            m_xMovement = Input.GetAxisRaw("Horizontal");
            m_yMovement = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if (m_xMovement > m_yMovement && m_xMovement > 0)
            {
                m_playerController.Move(Vector3.right);
            }
            else if (m_xMovement < m_yMovement && m_xMovement < 0)
            {
                m_playerController.Move(Vector3.left);
            }
            else if (m_yMovement > m_xMovement && m_yMovement > 0)
            {
                m_playerController.Move(Vector3.up);
            }
            else if (m_yMovement < m_xMovement && m_yMovement < 0)
            {
                m_playerController.Move(Vector3.down);
            }
            else
            {
                // No input
            }
        }
    }
}
