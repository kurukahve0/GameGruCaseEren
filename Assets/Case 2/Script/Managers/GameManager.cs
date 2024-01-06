using System;
using Case_1;
using UnityEngine;

namespace Case_2
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variable

        public bool IsGameStart => isGameStart;
        private bool isGameStart;
    

        #endregion
    
        #region MonoBehaviour

        void Start()
        {
            
        }

        void Update()
        {
        
        }

        private void OnEnable()
        {
            EventManager.OnMouseButton += MouseInput;
        }
        
        private void OnDisable()
        {
            EventManager.OnMouseButton -= MouseInput;
        }

        #endregion
    
        #region Func

        void MouseInput()
        {
            isGameStart = true;
        }

    

        #endregion
    }
}