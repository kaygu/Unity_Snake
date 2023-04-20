using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class BodyPart : MonoBehaviour
    {
        private SpriteRenderer m_sr;
        private Color m_defaultColor;
        private Vector3 m_pos;
        private bool m_isDigesting;

        private void Awake()
        {
            m_sr = GetComponent<SpriteRenderer>();
            m_defaultColor = m_sr.color;
        }
        public void Init(Vector3 _pos, bool _digest)
        {
           Position = _pos;
           isDigesting = _digest;
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

        public bool isDigesting
        {
            get
            {
                return m_isDigesting;
            }
            set
            {
                if (value) m_sr.color = Color.green;
                else m_sr.color = m_defaultColor;

                m_isDigesting = value;
            }
        }
    }
}
