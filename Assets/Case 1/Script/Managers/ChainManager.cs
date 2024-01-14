using System.Collections.Generic;
using UnityEngine;

namespace Case_1
{
    public class ChainManager : Singleton<ChainManager>
    {
        #region Variable
        
        const int MATCHCOUNT = 3; // buradaki sayı değiştirilebilir, daha fazla bağlatıda kontrol yapılabilir
        private int chainCounter;
        private HashSet<SquareController> inChainSquare = new HashSet<SquareController>();

        #endregion

        #region MonoBehaviour

        void OnEnable()
        {
 
            EventManager.OnSelectSquare += CheckConnect;
        }

        void OnDisable()
        {
    
            EventManager.OnSelectSquare -= CheckConnect;
        }

        

        #endregion
        
        
        #region Connect

        void CheckConnect(SquareController squareController)
        {
            ResetChainCounter();
            inChainSquare.Clear();
            squareController.GetChain();
        }

        #endregion

        #region Chain

        public void AddChainCounter()
        {
            chainCounter++;

            if (chainCounter >= MATCHCOUNT)
            {
                EventManager.OnChangeMatchCount?.Invoke(chainCounter);
            }
        }

        void ResetChainCounter() => chainCounter = 0;

        public void ChainControl(SquareController squareController)
        {
            inChainSquare.Add(squareController);

            if (inChainSquare.Count >= MATCHCOUNT)
            {
                squareController.BreakChain();
            }
        }
        

        #endregion
    }
}