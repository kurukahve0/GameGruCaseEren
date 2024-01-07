using System.Collections.Generic;
using Case_1;
using Case_2.Data;
using Case_2.Script.Level;
using UnityEngine;

namespace Case_2
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variable

        [SerializeField] private Transform levelsParent;
        
        public List<LevelController> activeLevels;
        public StackCreator ActiveStackCreator { get; private set; }

        private Vector3 levelFirstPosition = new Vector3(0, -.5f, 5f);

        public StackController LastStack => ActiveStackCreator.activeStacks[^1];
        public StackController NewStack => ActiveStackCreator.NewStack;
 

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
        
        
        void Start()
        {
           // SetActiveStack();
            //CreateLevel();
        }
        

        #endregion
    
        #region Func

        public void CreateLevel()
        {
            var level = Instantiate(GameManager.Instance.GameData.LevelPrefab,levelsParent);
            Vector3 levelPosition = levelFirstPosition;
            levelPosition.z += activeLevels.Count * 10f; //TODO değişecek
            level.transform.localPosition = levelPosition;
            activeLevels.Add(level);
            SetActiveStackCreator();
        }

        void SetActiveStackCreator()
        {
            ActiveStackCreator = activeLevels[^1].GetComponent<StackCreator>();
        }

        void GameSateListener(GameState currentState)
        {
            if (currentState==GameState.GameCreateState)
            {
                CreateLevel();
            }else if (currentState==GameState.GameRestartState)
            {
                activeLevels.ForEach(x=>Destroy(x.gameObject));
                activeLevels.Clear();
                CreateLevel();
            }
        }
    

        #endregion
    }
}