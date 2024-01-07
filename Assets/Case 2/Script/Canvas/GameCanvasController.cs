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
            if (currentState==GameState.GameCreateState || currentState==GameState.GameRestartState )
            {
                tapToStartText.SetActive(true);
                tapToStartText.transform.localScale = Vector3.zero;
                tapToStartText.transform.DOScale(Vector3.one, .6f).SetEase(Ease.OutBack);
            }
            else
            {
                tapToStartText.transform.DOScale(Vector3.zero, .35f).SetEase(Ease.InBack);
            }
            
            if (currentState==GameState.GameOverState)
            {
                gameOveText.SetActive(true);
                gameOveText.transform.localScale = Vector3.zero;
                gameOveText.transform.DOScale(Vector3.one, .6f).SetEase(Ease.OutBack);
            }
            else
            {
                gameOveText.SetActive(false);
            }
  
            
           // tapToStartText.SetActive(currentState== GameState.GameCreateState);
          //  gameOveText.SetActive(currentState== GameState.GameOverState);
        }
    }
}