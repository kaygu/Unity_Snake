using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class CameraController : MonoBehaviour
    {
        private void Start()
        {
            // Center camera
            transform.position = new Vector3(GameManager.Instance.Size / 2f - .5f, GameManager.Instance.Size / 2f - .5f, transform.position.z);
            Camera.main.orthographicSize = GameManager.Instance.Size / 2f;
            print("camera aspect: " + Camera.main.aspect);
            print("screen aspect: " + Screen.width / Screen.height);
        }
    }
}

