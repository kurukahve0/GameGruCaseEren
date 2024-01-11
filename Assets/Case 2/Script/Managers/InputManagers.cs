using System.Collections;
using System.Collections.Generic;
using Case_1;
using UnityEngine;

namespace Case_2
{
    public class InputManagers : Singleton<InputManagers>
    {
        #region Variable

        private GameState currentGameState;
        
        #endregion

        #region MonoBehaviour

        private void OnEnable()
        {
            GameManager.OnGameStateChange += GameSateListener;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= GameSateListener;
     
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentGameState==GameState.GameCreateState 
                    || currentGameState==GameState.GameRestartState)
                {
                    GameManager.Instance.UpdateState(GameState.GameStartState);
                }
                else if (currentGameState == GameState.GameStartState)
                {
                    if( GameManager.Instance.IsStackCreateOpen)
                        LevelManager.Instance.CreateStack();
                    
                }else if (currentGameState == GameState.GameNexLevelState)
                {
                    GameManager.Instance.UpdateState(GameState.GameCreateState);
                    GameManager.Instance.UpdateState(GameState.GameStartState);
                }
            }
        }

        #endregion

        #region Func

        void GameSateListener(GameState satate)
        {
            currentGameState = satate;
        }
        

        #endregion
    }
}