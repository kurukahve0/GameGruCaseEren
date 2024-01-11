using System;
using Case_1;
using Case_2.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Case_2
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variable

        //Action
        public static event Action<GameState> OnGameStateChange;
        public GameData GameData=> gameData;
        public int Level => level;

        public GameState CurrentState { get; private set; }
        
        private int level;

        [Header("Definitions")] 
        [SerializeField] private GameData gameData;

        #endregion

        #region MonoBehaviour

        void Start()
        {
            UpdateState(GameState.GameCreateState);
        }

        #endregion


        #region State

        public void UpdateState(GameState state) // Oyun akış kontrolü
        {
            CurrentState = state;

            switch (state)
            {
                case GameState.GameOverState:
                    GameOverState();
                    break;
                case GameState.GameFinalState:
                    GameWinState();
                    break;
                    
            }

            OnGameStateChange?.Invoke(state);
        }
        



        void GameOverState()
        {
            DOTween.Sequence()
                .AppendInterval(4f)
                .AppendCallback(() => UpdateState(GameState.GameRestartState));
        }

        void GameWinState()
        {
            level++;
            DOTween.Sequence()
                .AppendInterval(5f)
                .AppendCallback(() => UpdateState(GameState.GameNexLevelState));
        }

        #endregion
        
    }

    
}