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

        private bool m_isAlive = true;
        private List<BodyPart> m_tail = new List<BodyPart>();
        private Vector3 m_prevHeadPosition;
        private PlayerController _controller;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            CreateBodyPart(transform.position + Vector3.down * 3);
            CreateBodyPart(transform.position + Vector3.down * 2);
            CreateBodyPart(transform.position + Vector3.down);
            m_prevHeadPosition = transform.position;
        }



        private void Update()
        {
            if (m_isAlive && m_prevHeadPosition != transform.position)
            {
                MoveBody();
                if (CheckIfDead()) Debug.LogWarning("Snake is dead");

                Vector3 apple_pos = m_apple.Position;
                if (!m_apple.IsEaten() && apple_pos == transform.position)
                {
                    m_apple.Eat();

                }
                else if (m_apple.IsEaten() && apple_pos != transform.position)
                {
                    m_apple.Spawn();
                    m_tail.Last().isDigesting = true;
                }
                m_prevHeadPosition = transform.position;
            }

        }

        private void CreateBodyPart(Vector3 _pos, bool _digest = false)
        {
            BodyPart bp = Instantiate(m_bodyPart, _pos, Quaternion.identity, m_body.transform).GetComponent<BodyPart>();
            bp.Init(_pos, _digest);
            m_tail.Add(bp);
        }

        private void MoveBody()
        {
            Vector3 bPos = transform.position - _controller.Direction;
            if (GameManager.Instance.CycleThroughWalls)
            {
                // Ensure BodyPart stays at the other side of the grid if Snake cycles through wall
                if (transform.position.x == GameManager.Instance.WorldOrigin.x && _controller.Direction == Vector3.right) bPos = new Vector3(transform.position.x + GameManager.Instance.Size -1, transform.position.y, transform.position.z);
                if (transform.position.y == GameManager.Instance.WorldOrigin.y && _controller.Direction == Vector3.up) bPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.Size - 1, transform.position.z);
                if (transform.position.x == GameManager.Instance.Size -1 && _controller.Direction == Vector3.left) bPos = new Vector3(GameManager.Instance.WorldOrigin.x, transform.position.y, transform.position.z);
                if (transform.position.y == GameManager.Instance.Size - 1 && _controller.Direction == Vector3.down) bPos = new Vector3(transform.position.x, GameManager.Instance.WorldOrigin.x, transform.position.z);
            }
            CreateBodyPart(bPos);
            if (m_tail[0].isDigesting)
            {
                m_tail[0].isDigesting = false;
            }
            else
            {
                Destroy(m_tail[0].gameObject);
                m_tail.RemoveAt(0);
            }
        }

        private bool CheckIfDead()
        {
            if (!GameManager.Instance.CycleThroughWalls)
            {
                if (transform.position.x >= GameManager.Instance.Size || transform.position.y >= GameManager.Instance.Size)
                { 
                    m_isAlive = false;
                    GameManager.Instance.GameState = GameStateEnum.DEAD;
                    return true;
                }
                if (transform.position.x < GameManager.Instance.WorldOrigin.x || transform.position.y < GameManager.Instance.WorldOrigin.y)
                {
                    m_isAlive = false;
                    GameManager.Instance.GameState = GameStateEnum.DEAD;
                    return true;
                }
            }
            foreach(BodyPart bp in m_tail)
            {
                if (bp.transform.position == transform.position)
                {
                    m_isAlive = false;
                    GameManager.Instance.GameState = GameStateEnum.DEAD;
                    return true;
                }
            }
            return false;
        }

        public Vector3[] OccupiedPlaces()
        {
            Vector3[] result = new Vector3[m_tail.Count + 1];
            for (int i = 0; i < m_tail.Count; i++)
            {
                result[i] = m_tail[i].transform.position;
            }
            result[m_tail.Count] = transform.position;
            if (result.Length == GameManager.Instance.Grid.Length)
            {
                GameManager.Instance.GameState = GameStateEnum.WON;
            }
            return result;
        }
    }
}

