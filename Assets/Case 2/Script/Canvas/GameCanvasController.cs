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
            switch (currentState)
            {
                case GameState.GameCreateState:
                case GameState.GameRestartState:
                    TextScaleAnimation(tapToStartText.transform, true);
                    TextScaleAnimation(gameOveText.transform, false);
                    TextScaleAnimation(winText.transform, false);
                    break;

                case GameState.GameStartState:
                    TextScaleAnimation(tapToStartText.transform, false);
                    break;

                case GameState.GameOverState:
                    TextScaleAnimation(gameOveText.transform, true);
                    break;

                case GameState.GameFinalActionState:
                    TextScaleAnimation(winText.transform, true);
                    break;

                case GameState.GameNexLevelState:
                    TextScaleAnimation(tapToStartText.transform, true);
                    break;
            }
            
        }


        void TextScaleAnimation(Transform textTransform, bool isUp)
        {
            
            if (isUp)
            {
                if ( !textTransform.gameObject.activeInHierarchy)
                {
                    textTransform.gameObject.SetActive(true);
                    textTransform.localScale = Vector3.zero;
                    textTransform.DOScale(Vector3.one, .6f).SetEase(Ease.OutBack);
                }
              
            }
            else
            {
                textTransform.DOScale(Vector3.zero, .6f).SetEase(Ease.InBack).OnComplete(() => textTransform.gameObject.SetActive(false));

            }
        }
    }
}