using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case_2
{
    
    public class InputManagers : MonoBehaviour
    {
        #region Variable

        


        #endregion

        #region MonoBehaviour


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.OnMouseButton?.Invoke();
            }
        }

        #endregion

        #region Func



        #endregion
    }
}