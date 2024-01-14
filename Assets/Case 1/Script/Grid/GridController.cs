using UnityEngine;


namespace Case_1
{
    public class GridController : Singleton<GridController>
    {
        #region Variable

        [Header("Definitions")] 
        [SerializeField] private SquareController squarePrefabs;
        [SerializeField] private Transform squareParent;


        [Header("Square")]
        private SquareController[,] activeSquares;
        private float squareSize = .42f;
        private int currentSize;

        

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            CreateGrid(3);
        }

        void OnEnable()
        {
            EventManager.OnChangeSizeValue += CreateGrid;
        }

        void OnDisable()
        {
            EventManager.OnChangeSizeValue -= CreateGrid;
        }

        #endregion

        #region Grid

        void ClearGrid()
        {
            if (activeSquares == null || activeSquares.Length == 0)
                return;

            for (int i = 0; i < activeSquares.GetLength(0); i++)
            {
                for (int j = 0; j < activeSquares.GetLength(1); j++)
                {
                    Destroy(activeSquares[i, j].gameObject);
                }
            }
        }

        void CreateGrid(int size)
        {
            currentSize = size;
            ClearGrid();
            SetPosition();
            activeSquares = new SquareController[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CreateSquare(y, x);
                }
            }
        }


        void CreateSquare(int x, int y)
        {
            var square = Instantiate(squarePrefabs, squareParent);

            float xPos = x * squareSize;
            float yPos = y * squareSize;

            xPos -= (currentSize - 1) * squareSize * 0.5f;
            yPos -= (currentSize - 1) * squareSize * 0.5f;
            square.transform.localPosition = new Vector3(xPos, yPos, 0);
            square.Location[0] = x;
            square.Location[1] = y;
            square.gameObject.name = "Square" + x + " " + y;
            activeSquares[y, x] = square;
        }

        void SetPosition() // Kamera için
        {
            Vector3 gridPosition = transform.position;
            gridPosition.y = .75f + (currentSize - 3) * .24f;
            transform.position = gridPosition;
        }

        #endregion


        #region Square

        public SquareController GetSquare(int x, int y)
        {
            if (x >= currentSize || x < 0)
                return null;
            if (y >= currentSize || y < 0)
                return null;
            
            if (!activeSquares[y, x].Selected)
                return null;

            return activeSquares[y, x];
        }
        

        #endregion

    }
}