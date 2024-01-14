using System.Collections.Generic;
using UnityEngine;

namespace Case_1
{
    public class SquareController : MonoBehaviour
    {
        #region Variable

        public bool Selected
        {
            get => selected;
            set
            {
                if (value)
                    NeighbourControl();

                xObject.SetActive(value);
                selected = value;
            }
        }

        public int[] Location { get; set; } = new int[2];
        // neighbour control  
        public List<SquareController> neighbourSquare = new List<SquareController>();
        

        private bool selected = false;
        private bool isGetChainOpen = true;


        [Header("Definitions")] [SerializeField]
        private GameObject xObject;


       
       


        #endregion

        #region MonoBehaviour

        private void OnEnable()
        {
            EventManager.OnRefreshNeighbour += GetChainOpen;
        }

        private void OnDisable()
        {
            EventManager.OnRefreshNeighbour -= GetChainOpen;
        }

        #endregion

        #region Neigbour

        void NeighbourControl()
        {
            //neighbourSquare.Clear();

            AddNeighbour(GridController.Instance.GetSquare(Location[0], Location[1] + 1));
            AddNeighbour(GridController.Instance.GetSquare(Location[0], Location[1] - 1));
            AddNeighbour(GridController.Instance.GetSquare(Location[0] + 1, Location[1]));
            AddNeighbour(GridController.Instance.GetSquare(Location[0] - 1, Location[1]));
        }

        public void AddNeighbour(SquareController neighbourSquareController)
        {
            if (!neighbourSquareController)
                return;
            if (neighbourSquare.Contains(neighbourSquareController))
                return;

            neighbourSquare.Add(neighbourSquareController);
            neighbourSquareController.AddNeighbour(this);
        }

        #endregion

        #region Chain

        public void GetChain()
        {
            if (!isGetChainOpen)
                return;
            isGetChainOpen = false;
            ChainManager.Instance.ChainControl(this);
            List<SquareController>
                holdNeighbour = new List<SquareController>(neighbourSquare); // Clearda sorun olmasın diye
            //neighbourSquare.ForEach(x=>x.GetChain());

            foreach (var squareChain in holdNeighbour)
            {
                squareChain.GetChain();
            }
        }

        public void BreakChain()
        {
            if (!selected)
                return;

            Selected = false;
            ChainManager.Instance.AddChainCounter();
            foreach (var square in neighbourSquare)
            {
                square.BreakChain();
            }

            neighbourSquare.Clear();
        }

        private void GetChainOpen() => isGetChainOpen = true;


        #endregion




    }

}