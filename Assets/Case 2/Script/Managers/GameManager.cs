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

        [Header("Definitions")] [SerializeField]
        private GameCanvasController gameCanvas;

        [SerializeField] private ChibiController chibiController;
        public GameData GameData;

        public bool IsGameStart => isGameStart;
        private bool isGameStart;

        public int Level => level;
        private int level;
       

        public bool IsStackCreateOpen { get; set; } = false;

        public GameState CurrentState { get; private set; }

        //Action
        public static event Action<GameState> OnGameStateChange;

        #endregion

        #region MonoBehaviour

        void Start()
        {
            UpdateState(GameState.GameCreateState);
        }

        #endregion


        #region State

        public void UpdateState(GameState state)
        {
            CurrentState = state;

            switch (state)
            {
                case GameState.GameCreateState:
                    GameCreateState();
                    break;
                case GameState.GameStartState:
                    GameStartState();
                    break;
                case GameState.GameOverState:
                    GameOverState();
                    break;
                case GameState.GameFinalState:
                    GameWinState();
                    break;
                case GameState.GameFinalActionState:
                    break;
                    
            }

            OnGameStateChange?.Invoke(state);
        }


        void GameCreateState()
        {
            // LevelManager.Instance.CreateLevel();
            //   UpdateState(GameState.GameStartState);
        }

        void GameStartState()
        {
            IsStackCreateOpen = true;
            isGameStart = true;
            // LevelManager.Instance.ActiveStackCreator.CreateNewStack();
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

        #region Stack

        public void CreateStackOpen()
        {
            LevelManager.Instance.CreateNewStack();
        }

        #endregion
    }

    
}