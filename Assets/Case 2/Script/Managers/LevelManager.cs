using System.Collections.Generic;
using Case_1;
using Case_2.Data;
using UnityEngine;

namespace Case_2
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variable

        public bool IsStackCreateOpen { get; set; }
        
        public List<LevelController> activeLevels;
        public LevelController ActiveLevel { get; private set; }
        public StackController LastStack => ActiveLevel.activeStacks[^1];
        StackController NewStack => ActiveLevel.NewStack;

        private Vector3 levelFirstPosition = new Vector3(0, -.5f, 5f);
        private GameData GameData => GameManager.Instance.GameData;
        
        [Header("Definitions")] 
        [SerializeField] private Transform levelsParent;
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
        
        #endregion

        #region Func
        
        public void CreateStackOpen()
        {
            if(NewStack)
                return;
            
            IsStackCreateOpen=true;
            CreateNewStack();
        }

        public void CreateStack()
        {
            if(!IsStackCreateOpen)
                return;
            ActiveLevel.CreateStackPiece();
        }

        void CreateNewStack()
        {
            if (ActiveLevel.activeStacks.Count < 10)
            {
                ActiveLevel.CreateFullPieceStack();
            }
            else
            {
                GameManager.Instance.UpdateState(GameState.GameFinalState);
            }
        }

        void CreateLevel()
        {
            var level = Instantiate(GameData.LevelPrefab, levelsParent);
            Vector3 levelPosition = levelFirstPosition;
            levelPosition.z += activeLevels.Count * GameData.LevelLenght; //TODO değişecek
            level.transform.localPosition = levelPosition;
            activeLevels.Add(level);
            SetActiveLevel();
        }

        void SetActiveLevel()
        {
            ActiveLevel = activeLevels[^1];
        }

        void GameSateListener(GameState currentState)
        {
            if (currentState == GameState.GameCreateState)
            {
                CreateLevel();
                IsStackCreateOpen = true;
            }
            else if (currentState == GameState.GameRestartState)
            {
                ClearLevel();
            }
        }

        void ClearLevel()
        {
            foreach (var level in activeLevels)
            {
                level.ClearStacks();
                Destroy(level.gameObject);
            }
            activeLevels.Clear();
            CreateLevel();
        }

        #endregion
    }
}