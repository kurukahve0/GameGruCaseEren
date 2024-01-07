using System;
using Cinemachine;
using UnityEngine;

namespace Case_2
{
    public class CameraController: MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform target;
        private Vector3 startPosition;
        private Vector3 startRotation;
        
        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.eulerAngles;
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChange += GameSateListener;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= GameSateListener;
        }
        
        
        void GameSateListener(GameState currentState)
        {
            if (currentState==GameState.GameRestartState)
            {
                //virtualCamera.enabled = false;
                // Kamera pozisyonunu değiştir
                transform.position = startPosition;
                //transform.eulerAngles = startRotation;
                //virtualCamera.enabled = true;
            }
        }
    }
}