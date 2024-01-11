using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Case_2
{
    public class ChibiController : MonoBehaviour
    {
        #region Variable
        
        private float moveSpeed => GameManager.Instance.GameData.ChibiMovementSpeed; 
        private float lerpSpeed => GameManager.Instance.GameData.ChibiXLerpSpeed; 
        private float stackCreateTiggerDistance => GameManager.Instance.GameData.ChibiCreateStackTriggerDistance;
        
        private Vector3 startPosition;
        
        private bool isMovementOpen  = false;
        
        [Header("Definitions")] 
        [SerializeField] private Animator chibiAnimator;
        
        #endregion
    
        #region MonoBehaviour

        private void OnEnable()
        {
            GameManager.OnGameStateChange += GameSateListener;
            startPosition = transform.position;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= GameSateListener;
        }

        void Update()
        {
            if (!isMovementOpen)
                return;

            ChibiMove();
            ChibiGameControl();
        }

        #endregion
    
        #region Func
        
        void ChibiMove()
        {
            float targetX = LevelManager.Instance.LastStack.transform.position.x;
            float newY = transform.position.y;
            float lerpedX = Mathf.Lerp(transform.position.x, targetX, lerpSpeed * Time.deltaTime);
            
            Vector3 newPosition = new Vector3(lerpedX, newY, transform.position.z + moveSpeed * Time.deltaTime);
            
            transform.position = newPosition;

           
        }

        void ChibiGameControl()
        {
            float targetZ = LevelManager.Instance.LastStack.transform.position.z;
            float distance = targetZ - transform.position.z;
            float frontLimit = targetZ + LevelManager.Instance.LastStack.ZBoundsSize / 2;
            
            if ( distance<stackCreateTiggerDistance) // belli bir mesafen sonra stack yaratır
            {
                LevelManager.Instance.CreateStackOpen();
            }
            if (transform.position.z-frontLimit>0)
            {
                GameManager.Instance.UpdateState(GameState.GameOverState);
                ChibiFall();
            }
            
        }
        
        
        private void GameSateListener(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.GameCreateState:
                    chibiAnimator.SetTrigger("Idle");
                    break;
                case GameState.GameStartState:
                    isMovementOpen = true;
                    chibiAnimator.SetTrigger("Run");
                    break;
                case GameState.GameOverState:
                    isMovementOpen = false;
                    chibiAnimator.SetTrigger("Fall");
                    break;
                case GameState.GameFinalState:
                    isMovementOpen = false;
                    ChibiWin();
                    break;
                case GameState.GameRestartState:
                    transform.position = startPosition;
                    chibiAnimator.SetTrigger("Idle");
                    break;
            }
        }
        
        
        #endregion

        #region ChibiAction

        void ChibiFall()
        {
            DOTween.Sequence()
                .Append(transform.DOJump(transform.position + Vector3.forward*1.25f, .25f, 1, .45f))
                .Append(transform.DOMoveY(transform.position.y - 10f,1f).SetEase(Ease.InSine));
        }

        void ChibiWin()
        {
            DOTween.Sequence()
                .Append(
                    transform.DOMoveZ(LevelManager.Instance.ActiveLevels.FinishLineTransform.position.z, 1))
                .AppendCallback(() =>
                {
                    chibiAnimator.SetTrigger("Dance");
                    GameManager.Instance.UpdateState(GameState.GameFinalActionState);
                });
        }
        

        #endregion

    

      
    }
}