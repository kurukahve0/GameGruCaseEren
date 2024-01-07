using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case_2
{
    public class ChibiController : MonoBehaviour
    {
        #region Variable

        public bool IsMovementOpen {
            get;
            set;
        }
        public float moveSpeed => GameManager.Instance.GameData.ChibiMovementSpeed; // Objelerin sabit hızı
        private float lerpSpeed => GameManager.Instance.GameData.ChibiXLerpSpeed; // Lerp hızı (0.0f ile 1.0f arasında)
        private float stackCreateTiggerDistance => GameManager.Instance.GameData.ChibiCreateStackTriggerDistance;
    

        #endregion
    
        #region MonoBehaviour

        void Start()
        {
        
        }

        void Update()
        {
            if (!IsMovementOpen)
                return;

            ChibiMove();
            ChibiCreateControl();
        }

        #endregion
    
        #region Func

        public void MovementOpening()
        {
            
        }

        void ChibiMove()
        {
            float targetX = LevelManager.Instance.LastStackTransform.position.x;
            float newY = transform.position.y;
            float lerpedX = Mathf.Lerp(transform.position.x, targetX, lerpSpeed * Time.deltaTime);
            
            Vector3 newPosition = new Vector3(lerpedX, newY, transform.position.z + moveSpeed * Time.deltaTime);
            
            transform.position = newPosition;

           
        }

        void ChibiCreateControl()
        {
            float targetZ = LevelManager.Instance.LastStackTransform.position.z;
            if (!LevelManager.Instance.NewStack && targetZ-transform.position.z<stackCreateTiggerDistance)
            {
                GameManager.Instance.CreateStackOpen();
            }
            
        }
    

        #endregion
    }
}