using UnityEngine;

namespace Case_1
{
    public class InputManager : MonoBehaviour
    {
        #region Veriable

        [SerializeField] private LayerMask layerMask;
        

        #endregion
        #region MonoBehaviour

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RayToSquare();
            }
        }

        #endregion

        #region Func

        void RayToSquare()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , Vector2.zero
                ,1
                ,layerMask);
            if (hit.collider)
            {
                var square = hit.collider.GetComponent<SquareController>();
                square.Selected = true;
                EventManager.OnRefreshNeighbour?.Invoke();
                EventManager.OnSelectSquare?.Invoke(square);
               
            }
        }
        
        

        #endregion
        
        
        
    }
}