using System;
using DG.Tweening;
using Palmmedia.ReportGenerator.Core.Parser;
using UnityEngine;

namespace Case_2
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject tapToStartText;
        [SerializeField] private GameObject gameOveText;
        [SerializeField] private GameObject winText;
        
        private Tween scaleTween;

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
            if (currentState == GameState.GameCreateState || currentState == GameState.GameRestartState)
            {
                TextScaleAnimation(tapToStartText.transform, true);
                TextScaleAnimation(gameOveText.transform, false);
                TextScaleAnimation(winText.transform, false);
            }
            else if (currentState == GameState.GameStartState)
            {
                TextScaleAnimation(tapToStartText.transform, false);
            }
            else if (currentState == GameState.GameOverState)
            {
                TextScaleAnimation(gameOveText.transform, true);
            }else if (currentState == GameState.GameFinalActionState)
            {
                TextScaleAnimation(winText.transform, true);
            }else if (currentState == GameState.GameNexLevelState)
            {
                TextScaleAnimation(tapToStartText.transform, true);
            }

          
   


            // tapToStartText.SetActive(currentState== GameState.GameCreateState);
            //  gameOveText.SetActive(currentState== GameState.GameOverState);
        }


        void TextScaleAnimation(Transform textTransform, bool isUp)
        {
            
            if (isUp)
            {
                if ( !textTransform.gameObject.activeInHierarchy)
                {
                    textTransform.gameObject.SetActive(true);
                    textTransform.localScale = Vector3.zero;
                    scaleTween =  textTransform.DOScale(Vector3.one, .6f).SetEase(Ease.OutBack);
                }
              
            }
            else
            {
                //textTransform.gameObject.SetActive(false);
                textTransform.DOScale(Vector3.zero, .6f).SetEase(Ease.InBack).OnComplete(() => textTransform.gameObject.SetActive(false));

            }
        }
    }
}