using System.Collections;
using System.Collections.Generic;
using Case_1;
using UnityEngine;

namespace Case_2
{
    public class InputManagers : Singleton<InputManagers>
    {
        #region Variable

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
                if (GameManager.Instance.CurrentState==GameState.GameCreateState || GameManager.Instance.CurrentState==GameState.GameRestartState )
                {
                    GameManager.Instance.UpdateState(GameState.GameStartState);
                }
                else if (GameManager.Instance.CurrentState == GameState.GameStartState)
                {
                    if(GameManager.Instance.IsStackCreateOpen)
                        LevelManager.Instance.ActiveStackCreator.CreateStack();
                }
            }
        }

        #endregion

        #region Func

        void GameSateListener(GameState satate)
        {

        }
        

        #endregion
    }
}