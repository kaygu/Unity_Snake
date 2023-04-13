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
    }
}

