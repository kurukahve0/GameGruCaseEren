using System;
using Case_2.Data;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Case_2
{
    public class StackController : MonoBehaviour , IPoolable
    {
        #region Variable

        public bool IsMovementOpen { get; set; } = false;
        public float XBoundsSize => meshRendererStack.bounds.size.x;
        public float ZBoundsSize => meshRendererStack.bounds.size.z;

        public Material Material
        {
            set => meshRendererStack.material = value;

            get => meshRendererStack.material;
        }
        
        private GameData GameData => GameManager.Instance.GameData;
        private float movementSpeed=>GameData.StackMovementSpeed;
        
        private bool movingRight;
        
        [Header("Definitions")] 
        [SerializeField] private MeshRenderer meshRendererStack;
        [SerializeField] private Rigidbody rigidbodyStack;
        
       
        
        
        
        #endregion
    
        #region MonoBehaviour

        private void Start()
        {
            
        }

        void Update()
        {
            UpdateObjectPosition();
        }
        

        #endregion
    
        #region Func

        void UpdateObjectPosition()
        {
            if(!IsMovementOpen)
                return;
            float movement = movementSpeed * Time.deltaTime;
            
            transform.Translate((movingRight?Vector3.right: Vector3.left) * movement);
            
            if (transform.position.x >=  GameData.CreateXPositions[1])
            {
                movingRight = false;
            }
            else if (transform.position.x <=  GameData.CreateXPositions[0])
            {
                movingRight = true;
            }
        }

        public void OpenPhysics(Vector3 torqueDirection)
        {
            rigidbodyStack.isKinematic = false;
            rigidbodyStack.AddTorque((torqueDirection)*2f,ForceMode.Impulse);
            IsMovementOpen = false;
            LeanPool.Despawn(gameObject,2f);
        }


        #endregion

        #region Pool

        public void OnSpawn()
        {
            rigidbodyStack.isKinematic = true;
            IsMovementOpen = false;
        }

        public void OnDespawn()
        {

        }

        #endregion
    }
}