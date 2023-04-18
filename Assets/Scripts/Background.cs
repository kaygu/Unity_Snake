using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Background : MonoBehaviour
    {
        private SpriteRenderer m_sr;
        private void Start()
        {
            m_sr = GetComponent<SpriteRenderer>();
            m_sr.enabled = true;
            transform.position = new Vector3(GameManager.Instance.Size / 2f - .5f, GameManager.Instance.Size / 2f - .5f);
            transform.localScale = new Vector3(GameManager.Instance.Size, GameManager.Instance.Size);
        }
    }
}
