using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class SnakeLife : MonoBehaviour
    {
        [SerializeField] private GameObject m_body;
        [SerializeField] private GameObject m_bodyPart;
        [SerializeField] private Apple m_apple;

        private List<BodyPart> m_tail = new List<BodyPart>();
        private PlayerController _controller;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            CreateBodyPart(transform.position + Vector3.down * 3);
            CreateBodyPart(transform.position + Vector3.down * 2);
            CreateBodyPart(transform.position + Vector3.down);
        }



        private void Update()
        {
            MoveBody();
            CheckIfDead();
            Vector3 apple_pos = m_apple.Position;
            if (!m_apple.isEaten() && apple_pos == transform.position)
            {
                m_apple.Eat();
                CreateBodyPart(transform.position);
            } 
            else if (m_apple.isEaten() && apple_pos != transform.position)
            {
                m_apple.Spawn();
            }
        }

        private void CreateBodyPart(Vector3 _pos)
        {
            BodyPart bp = Instantiate(m_bodyPart, _pos, Quaternion.identity, m_body.transform).GetComponent<BodyPart>();
            bp.Init(_pos);
            m_tail.Add(bp);
        }

        private void MoveBody()
        {
            if (transform.position != _controller.Direction + m_tail.Last().Position)
            {
                if (m_tail.Last().Position != transform.position - _controller.Direction)
                {
                    CreateBodyPart(transform.position - _controller.Direction); // add bodt part
                    Destroy(m_tail[0].gameObject); // remove end of tail
                    m_tail.RemoveAt(0);
                }
            }
    }

        private bool CheckIfDead()
        {
            bool res = false;
            m_tail.ForEach(delegate (BodyPart bp)
            {
                if (bp.transform.position == transform.position)
                {
                    Debug.LogWarning("Snake is dead");
                    res = true;
                }
            });
            return res;
        }

        public Vector3[] OccupiedPlaces()
        {
            Vector3[] result = new Vector3[m_tail.Count + 1];
            for (int i = 0; i < m_tail.Count; i++)
            {
                result[i] = m_tail[i].transform.position;
            }
            result[m_tail.Count] = transform.position;
            return result;
        }
    }
}

