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
        
        [SerializeField] private Animator chibiAnimator;
        
        
        public bool isMovementOpen = false;
        private float moveSpeed => GameManager.Instance.GameData.ChibiMovementSpeed; 
        private float lerpSpeed => GameManager.Instance.GameData.ChibiXLerpSpeed; 
        private float stackCreateTiggerDistance => GameManager.Instance.GameData.ChibiCreateStackTriggerDistance;
    
        // Start
        private Vector3 startPosition;
        #endregion
    
        #region MonoBehaviour

        private void OnEnable()
        {
            GameManager.OnGameStateChange += GameSateListener;
        }

        private void Start()
        {
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
            
            if (!LevelManager.Instance.NewStack && distance<stackCreateTiggerDistance)
            {
                Debug.Log("createee");
                GameManager.Instance.CreateStackOpen();
            }
            else if (transform.position.z-frontLimit>0)
            {
                GameManager.Instance.UpdateState(GameState.GameOverState);
                ChibiFall();
            }
            
        }
        
        private void GameSateListener(GameState gameState)
        {
            if (gameState==GameState.GameCreateState)
            {
               // transform.position = GetStartPosition();
                chibiAnimator.SetTrigger("Idle");
            }else if (gameState==GameState.GameStartState)
            {
                isMovementOpen = true;
                chibiAnimator.SetTrigger("Run");
            }else if (gameState==GameState.GameOverState)
            {
                isMovementOpen = false;
                chibiAnimator.SetTrigger("Fall");
            }else if (gameState==GameState.GameFinalState)
            {
                isMovementOpen = false;
                ChibiWin();
            }else if (gameState==GameState.GameRestartState)
            {
                transform.position = startPosition;
                chibiAnimator.SetTrigger("Idle");
            }
        }

        private Vector3 GetStartPosition()
        {
            Vector3 newStartPosition=startPosition;
            float levelFactor = GameManager.Instance.GameData.LevelLenght * GameManager.Instance.Level;
            newStartPosition.z += levelFactor;
            return newStartPosition;
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