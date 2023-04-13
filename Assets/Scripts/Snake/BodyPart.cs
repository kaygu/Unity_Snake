using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class BodyPart : MonoBehaviour
    {
        private Vector3 m_pos;
        private void Awake()
        {

        }

        public void Init(Vector3 _pos)
        {
           Position = _pos;
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



    }

}
