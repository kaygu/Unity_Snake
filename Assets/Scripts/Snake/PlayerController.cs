using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject m_body;
        [SerializeField] private GameObject m_bodyPart;

        private float m_timeElapsed;
        private float m_moveDelay;
        private float m_moveDelayIncrement;

        private Vector3 m_direction = Vector3.up;
        private List<Vector3> m_dirQueue = new List<Vector3>();

        private Vector3 m_prevPosition;
        private List<BodyPart> m_tail = new List<BodyPart>();
        private void Awake()
        {
            m_moveDelay = GameManager.Instance.MoveDelay;
            m_moveDelayIncrement = GameManager.Instance.MoveDelayIncrement;

            m_timeElapsed = m_moveDelay;
        }

        private void Start()
        {
            // Move head to center
            transform.position = new Vector3(GameManager.Instance.Size / 2, GameManager.Instance.Size / 2, transform.position.z);
            // Spawn tail
            CreateBodyPart(transform.position + Vector3.down * 3);
            CreateBodyPart(transform.position + Vector3.down * 2);
            CreateBodyPart(transform.position + Vector3.down);
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.GameState == GameStateEnum.PLAYING)
            {
                if (m_timeElapsed <= 0)
                {
                    if (m_dirQueue.Count > 0)
                    {
                        m_direction = m_dirQueue[0];
                        m_dirQueue.RemoveAt(0);
                    }
                    transform.position += m_direction;
                    CycleWalls();
                    m_timeElapsed = m_moveDelay;
                    MoveBody();
                }
                m_timeElapsed -= Time.fixedDeltaTime;
            }
        }

        public void Move(Vector3 _dir) // Recieve user input
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

        private void CycleWalls()
        {
            if (GameManager.Instance.CycleThroughWalls)
            {
                // x
                if (transform.position.x >= GameManager.Instance.Size) transform.position = new Vector3(GameManager.Instance.WorldOrigin.x, transform.position.y, transform.position.z);
                if (transform.position.x < GameManager.Instance.WorldOrigin.x) transform.position = new Vector3(GameManager.Instance.Size - 1, transform.position.y, transform.position.z);
                // y
                if (transform.position.y >= GameManager.Instance.Size) transform.position = new Vector3(transform.position.x, GameManager.Instance.WorldOrigin.y, transform.position.z);
                if (transform.position.y < GameManager.Instance.WorldOrigin.y) transform.position = new Vector3(transform.position.x, GameManager.Instance.Size - 1, transform.position.z);
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
            Vector3 bPos = transform.position - m_direction;
            if (GameManager.Instance.CycleThroughWalls)
            {
                // Ensure BodyPart stays at the other side of the grid if Snake cycles through wall
                if (transform.position.x == GameManager.Instance.WorldOrigin.x && m_direction == Vector3.right) bPos = new Vector3(transform.position.x + GameManager.Instance.Size - 1, transform.position.y, transform.position.z);
                if (transform.position.y == GameManager.Instance.WorldOrigin.y && m_direction == Vector3.up) bPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.Size - 1, transform.position.z);
                if (transform.position.x == GameManager.Instance.Size - 1 && m_direction == Vector3.left) bPos = new Vector3(GameManager.Instance.WorldOrigin.x, transform.position.y, transform.position.z);
                if (transform.position.y == GameManager.Instance.Size - 1 && m_direction == Vector3.down) bPos = new Vector3(transform.position.x, GameManager.Instance.WorldOrigin.x, transform.position.z);
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
        public Vector3 Direction
        {
            get
            {
                return m_direction;
            }
            private set
            {
                m_direction = value;
            }
        }

        public void IncreaseMoveSpeed()
        {
            if (m_moveDelay <= .1f)
            {
                print("We shmooving");
            }
            else if (m_moveDelay <= .2f)
            {
                m_moveDelay -= m_moveDelayIncrement / 2;
            }
            else
            {
                m_moveDelay -= m_moveDelayIncrement;
            }
        }

        public void DigestApple()
        {
            m_tail.Last().isDigesting = true;
        }

        public bool IsTailCollision()
        {
            foreach (BodyPart bp in m_tail)
            {
                if (bp.transform.position == transform.position)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsWallCollision()
        {
            if (!GameManager.Instance.CycleThroughWalls)
            {
                if (transform.position.x >= GameManager.Instance.Size || transform.position.y >= GameManager.Instance.Size)
                {
                    GameManager.Instance.GameState = GameStateEnum.DEAD;
                    return true;
                }
                if (transform.position.x < GameManager.Instance.WorldOrigin.x || transform.position.y < GameManager.Instance.WorldOrigin.y)
                {
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

