using System;
using Cinemachine;
using UnityEngine;

namespace Case_2
{
    public class FinalCameraController: MonoBehaviour
    {

        private bool isRotateOpen = false;
        private float rotateSpeed=50f;
        private Vector3 startRotation;
        
        [Header("Definitions")] 
        [SerializeField] CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform chibiTransform;
        
        private void OnEnable()
        {
            GameManager.OnGameStateChange += GameSateListener;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= GameSateListener;
        }

        private void Start()
        {
            startRotation = transform.eulerAngles;
        }

        private void Update()
        {
            if(!isRotateOpen)
                return;
            
            Rotate();
        }


        void GameSateListener(GameState currentState)
        {
            if (currentState==GameState.GameFinalActionState)
            {
                transform.eulerAngles = startRotation;
                isRotateOpen = true;
                virtualCamera.Priority = 11;
                transform.position = chibiTransform.transform.position;
            }
            else if(currentState==GameState.GameStartState)
            {
                virtualCamera.Priority = 9;
                isRotateOpen = false;
             //   
            }
        }

        void Rotate()
        {
            transform.Rotate(Vector3.up,Time.deltaTime*rotateSpeed);
        }
        
        
    }
}